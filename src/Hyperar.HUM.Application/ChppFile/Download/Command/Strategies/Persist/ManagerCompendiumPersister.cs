namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist.Constants;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium;

    public class ManagerCompendiumPersister : IFilePersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public ManagerCompendiumPersister(
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.countryRepository = countryRepository;
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

            await this.PersistManagerNodeAsync(
                managerCompendium.Manager,
                userProfile,
                cancellationToken);
        }

        private static SupporterTier GetSupporterTier(string value)
        {
            return value switch
            {
                SupporterTierName.None => SupporterTier.None,
                SupporterTierName.Silver => SupporterTier.Silver,
                SupporterTierName.Gold => SupporterTier.Gold,
                SupporterTierName.Platinum => SupporterTier.Platinum,
                SupporterTierName.Diamond => SupporterTier.Diamond,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        private async Task PersistManagerNodeAsync(
            Manager managerNode,
            Domain.UserProfile userProfile,
            CancellationToken cancellationToken)
        {
            var manager = await this.managerRepository.GetByIdAsync(managerNode.UserId);

            if (manager is null)
            {
                var country = await this.countryRepository.GetByIdAsync(
                    managerNode.Country.Id);

                ArgumentNullException.ThrowIfNull(country);

                manager = await this.managerRepository.InsertAsync(new Domain.Manager
                {
                    HattrickId = managerNode.UserId,
                    UserName = managerNode.LoginName,
                    SupporterTier = GetSupporterTier(managerNode.SupporterTier),
                    CurrencyName = managerNode.Currency.CurrencyName,
                    CurrencyRate = managerNode.Currency.CurrencyRate,
                    AvatarBytes = await ImageHelpers.BuildAvatarAsync(managerNode.Avatar, cancellationToken),
                    Country = country,
                    UserProfile = userProfile
                });
            }
            else
            {
                manager.CurrencyName = managerNode.Currency.CurrencyName;
                manager.CurrencyRate = managerNode.Currency.CurrencyRate;
                manager.AvatarBytes = await ImageHelpers.BuildAvatarAsync(managerNode.Avatar, cancellationToken);

                await this.managerRepository.UpdateAsync(manager);
            }
        }
    }
}