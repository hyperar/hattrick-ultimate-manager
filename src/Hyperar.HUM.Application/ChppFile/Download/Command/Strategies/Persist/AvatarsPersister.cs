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
    using Hyperar.HUM.Shared.Models.Chpp.Avatars;
    using Microsoft.EntityFrameworkCore;

    public class AvatarsPersister : PersisterBase, IFilePersisterStrategy
    {
        private readonly IRepository<Domain.SeniorPlayerAvatarLayer> seniorPlayerAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public AvatarsPersister(
            IRepository<Domain.SeniorPlayerAvatarLayer> seniorPlayerAvatarLayerRepository,
            IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.seniorPlayerAvatarLayerRepository = seniorPlayerAvatarLayerRepository;
            this.seniorPlayerRepository = seniorPlayerRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var avatars = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(avatars);

            var seniorTeam = await this.seniorTeamRepository.GetByIdAsync(avatars.Team.TeamId);

            ArgumentNullException.ThrowIfNull(seniorTeam);

            foreach (var curPlayer in avatars.Team.Players.Where(x => x.PlayerId != seniorTeam.TrainerHattrickId))
            {
                var seniorPlayer = await this.seniorPlayerRepository.GetByIdAsync(curPlayer.PlayerId);

                ArgumentNullException.ThrowIfNull(seniorPlayer);

                await this.PersistAvatarBackgroundAsync(
                    curPlayer.Avatar.BackgroundImage,
                    seniorPlayer,
                    cancellationToken);

                var index = 0;

                foreach (var curLayer in curPlayer.Avatar.Layers ?? Array.Empty<Layer>())
                {
                    await this.PersistAvatarLayerAsync(
                        curLayer,
                        index + 1,
                        seniorPlayer,
                        cancellationToken);

                    index++;
                }

                var layersToDelete = await this.seniorPlayerAvatarLayerRepository.Query(x => x.SeniorPlayerHattrickId == seniorPlayer.HattrickId
                                                                                          && x.Index > index)
                    .Select(x => x.Id)
                    .ToArrayAsync(cancellationToken);

                if (layersToDelete.Length > 0)
                {
                    await this.seniorPlayerAvatarLayerRepository.DeleteRangeAsync(layersToDelete);
                }
            }
        }

        private async Task PersistAvatarBackgroundAsync(
            string backgroundImageUrl,
            Domain.SeniorPlayer seniorPlayer,
            CancellationToken cancellationToken)
        {
            var backgroundLayer = await this.seniorPlayerAvatarLayerRepository.Query(x => x.SeniorPlayerHattrickId == seniorPlayer.HattrickId
                                                                                       && x.Type == GetAvatarLayerType(backgroundImageUrl)
                                                                                       && x.Index == 0)
                .SingleOrDefaultAsync(cancellationToken);

            if (backgroundLayer is null)
            {
                await this.seniorPlayerAvatarLayerRepository.InsertAsync(
                    new Domain.SeniorPlayerAvatarLayer
                    {
                        ImageUrl = backgroundImageUrl,
                        Index = 0,
                        XCoordinate = 0,
                        YCoordinate = 0,
                        Type = GetAvatarLayerType(backgroundImageUrl),
                        SeniorPlayer = seniorPlayer
                    });
            }
        }

        private async Task PersistAvatarLayerAsync(
            Layer layerNode,
            int index,
            Domain.SeniorPlayer seniorPlayer,
            CancellationToken cancellationToken)
        {
            var layer = await this.seniorPlayerAvatarLayerRepository.Query(x => x.SeniorPlayerHattrickId == seniorPlayer.HattrickId
                                                                              && x.Index == index)
                .OrderBy(x => x.Index)
                .SingleOrDefaultAsync(cancellationToken);

            if (layer == null)
            {
                await this.seniorPlayerAvatarLayerRepository.InsertAsync(
                    new Domain.SeniorPlayerAvatarLayer
                    {
                        ImageUrl = layerNode.Image,
                        Index = index,
                        XCoordinate = layerNode.X,
                        YCoordinate = layerNode.Y,
                        Type = GetAvatarLayerType(layerNode.Image),
                        SeniorPlayer = seniorPlayer
                    });
            }
            else
            {
                layer.ImageUrl = layerNode.Image;
                layer.Index = index;
                layer.XCoordinate = layerNode.X;
                layer.YCoordinate = layerNode.Y;
                layer.Type = GetAvatarLayerType(layerNode.Image);

                await this.seniorPlayerAvatarLayerRepository.UpdateAsync(layer);
            }
        }
    }
}