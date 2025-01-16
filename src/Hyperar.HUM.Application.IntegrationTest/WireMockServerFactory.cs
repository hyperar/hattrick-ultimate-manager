namespace Hyperar.HUM.Application.IntegrationTest
{
    using WireMock.RequestBuilders;
    using WireMock.ResponseBuilders;
    using WireMock.Server;

    public static class WireMockServerFactory
    {
        private static readonly object serverLock = new object();

        private static WireMockServer? server;

        public static WireMockServer GetServer()
        {
            lock (serverLock)
            {
                if (server == null)
                {
                    server = WireMockServer.Start(null, true, false);

                    // GetRequestToken => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/request_token.ashx")
                            .UsingGet())
                        .RespondWith(
                            Response.Create()
                            .WithBody($"oauth_token={Constants.OAuth.ValidRequestToken}&oauth_token_secret={Constants.OAuth.ValidRequestSecret}"));

                    // GetAuthorizationUrl - Valid Request Token => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/authorize.aspx")
                            .UsingGet()
                            .WithParam("oauth_token", Constants.OAuth.ValidRequestToken))
                        .ThenRespondWithOK();

                    // GetAuthorizationUrl - Invalid Request Token => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/authorize.aspx")
                            .UsingGet()
                            .WithParam("oauth_token", Constants.OAuth.InvalidRequestToken))
                        .ThenRespondWithStatusCode(401);

                    // CheckToken - Valid Token => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/check_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Constants.OAuth.ValidAccessToken))
                        .ThenRespondWithOK();

                    // CheckToken - Invalid Token => 401 Unauthorized.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/check_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Constants.OAuth.InvalidAccessToken))
                        .ThenRespondWithStatusCode(401);

                    // RevokeToken - Valid Token => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/invalidate_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Constants.OAuth.ValidAccessToken))
                        .ThenRespondWithOK();

                    // RevokeToken - Invalid Token => 401 Unauthorized.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/invalidate_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Constants.OAuth.InvalidAccessToken))
                        .ThenRespondWithStatusCode(401);
                }
            }

            return server;
        }
    }
}
