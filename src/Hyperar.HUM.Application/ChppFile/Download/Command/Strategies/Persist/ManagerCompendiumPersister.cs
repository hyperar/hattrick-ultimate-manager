namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp;
    using Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium;
    using Microsoft.EntityFrameworkCore;

    public class ManagerCompendiumPersister : PersisterBase, IFilePersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.ManagerAvatarLayer> managerAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public ManagerCompendiumPersister(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IRepository<Domain.ManagerAvatarLayer> managerAvatarLayerRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.managerAvatarLayerRepository = managerAvatarLayerRepository;
            this.managerRepository = managerRepository;
            this.userProfileRepository = userProfileRepository;
        }

        public async Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var managerCompendium = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(managerCompendium);

            var userProfile = await this.userProfileRepository.GetByIdAsync(xmlFileDownloadTask.UserProfileId);

            ArgumentNullException.ThrowIfNull(userProfile);

            var manager = await this.PersistManagerNodeAsync(
                managerCompendium.Manager,
                userProfile);

            if (managerCompendium.Manager.Avatar is not null)
            {
                await this.PersistAvatarBackgroundAsync(
                    managerCompendium.Manager.Avatar.BackgroundImage,
                    manager,
                    cancellationToken);

                var index = 0;

                foreach (var curLayer in managerCompendium.Manager.Avatar.Layers ?? Array.Empty<Layer>())
                {
                    await this.PersistAvatarLayerAsync(
                        curLayer,
                        index + 1,
                        manager,
                        cancellationToken);

                    index++;
                }

                var layersToDelete = await this.managerAvatarLayerRepository.Query(x => x.ManagerHattrickId == manager.HattrickId
                                                                                     && x.Index > index)
                    .Select(x => x.Id)
                    .ToArrayAsync(cancellationToken);

                if (layersToDelete.Length > 0)
                {
                    await this.managerAvatarLayerRepository.DeleteRangeAsync(layersToDelete);
                }
            }
        }

        private async Task PersistAvatarBackgroundAsync(
            string backgroundImageUrl,
            Domain.Manager manager,
            CancellationToken cancellationToken)
        {
            var backgroundLayer = await this.managerAvatarLayerRepository.Query(x => x.ManagerHattrickId == manager.HattrickId
                                                                                       && x.Type == GetAvatarLayerType(backgroundImageUrl)
                                                                                       && x.Index == 0)
                .SingleOrDefaultAsync(cancellationToken);

            if (backgroundLayer is null)
            {
                await this.managerAvatarLayerRepository.InsertAsync(
                    new Domain.ManagerAvatarLayer
                    {
                        ImageUrl = backgroundImageUrl,
                        Index = 0,
                        XCoordinate = 0,
                        YCoordinate = 0,
                        Type = GetAvatarLayerType(backgroundImageUrl),
                        Manager = manager
                    });
            }
        }

        private async Task PersistAvatarLayerAsync(
            Layer layerNode,
            int index,
            Domain.Manager manager,
            CancellationToken cancellationToken)
        {
            var layer = await this.managerAvatarLayerRepository.Query(x => x.ManagerHattrickId == manager.HattrickId
                                                                        && x.Index == index)
                .OrderBy(x => x.Index)
                .SingleOrDefaultAsync(cancellationToken);

            if (layer == null)
            {
                await this.managerAvatarLayerRepository.InsertAsync(
                    new Domain.ManagerAvatarLayer
                    {
                        ImageUrl = layerNode.Image,
                        Index = index,
                        XCoordinate = layerNode.X,
                        YCoordinate = layerNode.Y,
                        Type = GetAvatarLayerType(layerNode.Image),
                        Manager = manager
                    });
            }
            else
            {
                layer.ImageUrl = layerNode.Image;
                layer.Index = index;
                layer.XCoordinate = layerNode.X;
                layer.YCoordinate = layerNode.Y;
                layer.Type = GetAvatarLayerType(layerNode.Image);

                await this.managerAvatarLayerRepository.UpdateAsync(layer);
            }
        }

        private async Task<Domain.Manager> PersistManagerNodeAsync(
            Manager managerNode,
            Domain.UserProfile userProfile)
        {
            var manager = await this.managerRepository.GetByIdAsync(managerNode.UserId);

            if (manager is null)
            {
                var country = await this.countryRepository.GetByIdAsync(
                    managerNode.Country.Id);

                ArgumentNullException.ThrowIfNull(country);

                manager = new Domain.Manager
                {
                    HattrickId = managerNode.UserId,
                    UserName = managerNode.LoginName,
                    SupporterTier = GetSupporterTier(managerNode.SupporterTier),
                    CurrencyName = managerNode.Currency.CurrencyName,
                    CurrencyRate = managerNode.Currency.CurrencyRate,
                    Country = country,
                    UserProfile = userProfile
                };

                await this.managerRepository.InsertAsync(manager);
            }
            else
            {
                manager.CurrencyName = managerNode.Currency.CurrencyName;
                manager.CurrencyRate = managerNode.Currency.CurrencyRate;

                await this.managerRepository.UpdateAsync(manager);
            }

            await this.databaseContext.SaveAsync();

            return manager;
        }
    }
}