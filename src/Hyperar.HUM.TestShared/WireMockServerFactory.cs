namespace Hyperar.HUM.TestShared
{
    using Hyperar.HUM.ChppApiClient.Constants;
    using WireMock.RequestBuilders;
    using WireMock.ResponseBuilders;
    using WireMock.Server;
    using WireMock.Settings;
    using WireMock.Types;

    public static class WireMockServerFactory
    {
        private static readonly object serverLock = new object();

        private static WireMockServer? server;

        public static int StartServerAndGetPort()
        {
            lock (serverLock)
            {
                if (server == null)
                {
                    server = WireMockServer.Start(new
                    WireMockServerSettings
                    {
                        AcceptAnyClientCertificate = true,
                        ClientCertificateMode = ClientCertificateMode.NoCertificate,
                        Port = null,
                        UseHttp2 = false,
                        UseSSL = true
                    });

                    // GetRequestToken Valid Consumer => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/request_token.ashx")
                            .WithParam("oauth_consumer_key", Valid.ConsumerKey)
                            .UsingGet())
                        .RespondWith(
                            Response.Create()
                            .WithBody($"oauth_token={Valid.RequestToken}&oauth_token_secret={Valid.RequestSecret}&oauth_callback_confirmed=true"));

                    // GetRequestToken Invalid Consumer => 401 Unauthorized.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/request_token.ashx")
                            .WithParam("oauth_consumer_key", Invalid.ConsumerKey)
                            .UsingGet())
                        .RespondWith(
                            Response.Create()
                                .WithStatusCode(401)
                                .WithBody(
                                    "<!DOCTYPE html public \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">" +
                                    "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                                    "    <head>" +
                                    "        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\"/>" +
                                    "        <title>401 - Unauthorized: Access is denied due to invalid credentials.</title>" +
                                    "        <style type=\"text/css\">" +
                                    "            <!--" +
                                    "                body{margin:0;font-size:.7em;font-family:Verdana, Arial, Helvetica, sans-serif;background:#EEEEEE;}" +
                                    "                fieldset{padding:0 15px 10px 15px;} " +
                                    "                h1{font-size:2.4em;margin:0;color:#FFF;}" +
                                    "                h2{font-size:1.7em;margin:0;color:#CC0000;} " +
                                    "                h3{font-size:1.2em;margin:10px 0 0 0;color:#000000;} " +
                                    "                #header{width:96%;margin:0 0 0 0;padding:6px 2% 6px 2%;font-family:\"trebuchet MS\", Verdana, sans-serif;color:#FFF;" +
                                    "                background-color:#555555;}" +
                                    "                #content{margin:0 0 0 2%;position:relative;}" +
                                    "                .content-container{background:#FFF;width:96%;margin-top:8px;padding:10px;position:relative;}" +
                                    "            -->" +
                                    "        </style>" +
                                    "    </head>" +
                                    "    <body>" +
                                    "        <div id=\"header\">" +
                                    "            <h1>Server Error</h1>" +
                                    "        </div>" +
                                    "        <div id=\"content\">" +
                                    "            <div class=\"content-container\">" +
                                    "                <fieldset>" +
                                    "                    <h2>401 - Unauthorized: Access is denied due to invalid credentials.</h2>" +
                                    "                    <h3>You do not have permission to view this directory or page using the credentials that you supplied.</h3>" +
                                    "                </fieldset>" +
                                    "            </div>" +
                                    "        </div>" +
                                    "    </body>" +
                                    "</html>"));

                    // CheckToken - Valid Token => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/check_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Valid.AccessToken))
                        .RespondWith(
                            Response.Create()
                                .WithStatusCode(200)
                                .WithBody(
                                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                    "<HattrickData>" +
                                    "    <FileName>check_token</FileName>" +
                                    "    <Version>1.0</Version>" +
                                    "    <UserID>12345678</UserID>" +
                                   $"    <FetchedDate>{DateTime.Now:yyyy-MM-dd HH:mm:ss}</FetchedDate>" +
                                   $"    <Token>{Valid.AccessToken}</Token>" +
                                   $"    <Created>{DateTime.Now.AddDays(-5):yyyy-MM-dd HH:mm:ss}</Created>" +
                                    "    <User>12345678</User>" +
                                    "    <Expires>9999-12-31 23:59:59</Expires>" +
                                    "    <ExtendedPermissions/>" +
                                    "</HattrickData>"));

                    // CheckToken - Invalid Token => 401 Unauthorized.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/check_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Invalid.AccessToken))
                        .ThenRespondWithStatusCode(401);

                    // RevokeToken - Valid Token => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/invalidate_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Valid.AccessToken))
                        .RespondWith(
                            Response.Create()
                            .WithStatusCode(200)
                            .WithBody($"Invalidated token {Valid.AccessToken}"));

                    // RevokeToken - Invalid Token => 401 Unauthorized.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/invalidate_token.ashx")
                            .UsingGet()
                            .WithParam("oauth_token", Invalid.AccessToken))
                        .ThenRespondWithStatusCode(401);

                    // GetAccessToken Valid Verifier => 200 OK.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/access_token.ashx")
                            .WithParam("oauth_verifier", Valid.VerificationCode)
                            .UsingGet())
                        .RespondWith(
                            Response.Create()
                            .WithBody($"oauth_token={Valid.AccessToken}&oauth_token_secret={Valid.AccessSecret}"));

                    // GetAccessToken Invalid Verifier => 401 Unauthorized.
                    server.Given(
                        Request.Create()
                            .WithPath("/oauth/access_token.ashx")
                            .WithParam("oauth_verifier", Invalid.VerificationCode)
                            .UsingGet())
                        .RespondWith(
                            Response.Create()
                                .WithStatusCode(401)
                                .WithBody(
                                    "<!DOCTYPE html public \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">" +
                                    "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                                    "    <head>" +
                                    "        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\"/>" +
                                    "        <title>401 - Unauthorized: Access is denied due to invalid credentials.</title>" +
                                    "        <style type=\"text/css\">" +
                                    "            <!--" +
                                    "                body{margin:0;font-size:.7em;font-family:Verdana, Arial, Helvetica, sans-serif;background:#EEEEEE;}" +
                                    "                fieldset{padding:0 15px 10px 15px;} " +
                                    "                h1{font-size:2.4em;margin:0;color:#FFF;}" +
                                    "                h2{font-size:1.7em;margin:0;color:#CC0000;} " +
                                    "                h3{font-size:1.2em;margin:10px 0 0 0;color:#000000;} " +
                                    "                #header{width:96%;margin:0 0 0 0;padding:6px 2% 6px 2%;font-family:\"trebuchet MS\", Verdana, sans-serif;color:#FFF;" +
                                    "                background-color:#555555;}" +
                                    "                #content{margin:0 0 0 2%;position:relative;}" +
                                    "                .content-container{background:#FFF;width:96%;margin-top:8px;padding:10px;position:relative;}" +
                                    "            -->" +
                                    "        </style>" +
                                    "    </head>" +
                                    "    <body>" +
                                    "        <div id=\"header\">" +
                                    "            <h1>Server Error</h1>" +
                                    "        </div>" +
                                    "        <div id=\"content\">" +
                                    "            <div class=\"content-container\">" +
                                    "                <fieldset>" +
                                    "                    <h2>401 - Unauthorized: Access is denied due to invalid credentials.</h2>" +
                                    "                    <h3>You do not have permission to view this directory or page using the credentials that you supplied.</h3>" +
                                    "                </fieldset>" +
                                    "            </div>" +
                                    "        </div>" +
                                    "    </body>" +
                                    "</html>"));
                }

                return server.Port;
            }
        }
    }
}