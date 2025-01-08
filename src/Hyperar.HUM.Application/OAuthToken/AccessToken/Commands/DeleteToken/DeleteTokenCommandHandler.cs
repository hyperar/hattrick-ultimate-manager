namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.DeleteToken
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using MediatR;

    internal class DeleteTokenCommandHandler : IRequestHandler<DeleteTokenCommand>
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.OAuthToken> oauthTokenRepository;

        public DeleteTokenCommandHandler(
            IDatabaseContext databaseContext,
            IRepository<Domain.OAuthToken> oauthTokenRepository)
        {
            this.databaseContext = databaseContext;
            this.oauthTokenRepository = oauthTokenRepository;
        }

        public async Task Handle(DeleteTokenCommand request, CancellationToken cancellationToken)
        {
            var oauthToken = await this.oauthTokenRepository.GetByIdAsync(request.OAuthTokenId);

            ArgumentNullException.ThrowIfNull(oauthToken);

            try
            {
                await this.databaseContext.BeginTransactionAsync();

                await this.oauthTokenRepository.DeleteAsync(request.OAuthTokenId);
            }
            finally
            {
                await this.databaseContext.EndTransactionAsync();
            }
        }
    }
}