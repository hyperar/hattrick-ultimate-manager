﻿namespace Hyperar.HUM.ChppApiClient
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Microsoft.Extensions.Configuration;

    public class ProtectedResourceUrlFactory : IProtectedResourceUrlFactory
    {
        private const string FileNameKey = "OAuth:Endpoints:ProtectedResources:{0}:FileName";

        private const string FileParameterKey = "file";

        private const string ParametersKey = "OAuth:Endpoints:ProtectedResources:{0}:Parameters";

        private const string ProtectedResourcesKey = "OAuth:Endpoints:Base:ProtectedResources";

        private const string VersionKey = "OAuth:Endpoints:ProtectedResources:{0}:Version";

        private const string VersionParameterKey = "version";

        private readonly IConfiguration configuration;

        public ProtectedResourceUrlFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string BuildUrl(XmlFileType fileType, NameValueCollection? parameters)
        {
            parameters ??= new NameValueCollection();

            parameters.Add(
                FileParameterKey,
                this.configuration[string.Format(
                    FileNameKey,
                    fileType)]);

            parameters.Add(
                VersionParameterKey,
                this.configuration[string.Format(
                    VersionKey,
                    fileType)]);

            this.ValidateParameters(fileType, parameters);

            var protectedResourcesUrl = this.configuration[ProtectedResourcesKey];

            ArgumentNullException.ThrowIfNull(protectedResourcesUrl);

            var uriBuilder = new UriBuilder(protectedResourcesUrl)
            {
                Query = this.BuildQueryString(fileType, parameters)
            };

            return uriBuilder.ToString();
        }

        private string BuildQueryString(XmlFileType fileType, NameValueCollection parameters)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < parameters.Count; i++)
            {
                var delimiter = i == 0 ? "?" : "&";

                var key = parameters.GetKey(i);

                ArgumentException.ThrowIfNullOrEmpty(key);

                var value = parameters.Get(i);

                ArgumentException.ThrowIfNullOrEmpty(value);

                stringBuilder.Append($"{delimiter}{key}={value}");
            }

            return stringBuilder.ToString();
        }

        private void ValidateParameters(XmlFileType fileType, NameValueCollection parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return;
            }

            var allowedParametersKeys = this.configuration.GetSection(string.Format(ParametersKey, fileType.ToString()))
                .Get<List<string>>() ?? new List<string>();

            allowedParametersKeys.Add("file");
            allowedParametersKeys.Add("version");

            var specifiedParametersKeys = parameters.Keys.Cast<string>()
                .ToArray()
                ;

            var unrecognizedParameters = specifiedParametersKeys.Select(x => x.ToLower())
                                                                     .Except(allowedParametersKeys.Select(x => x.ToLower()))
                                                                     .ToArray();

            if (unrecognizedParameters.Length != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(parameters));
            }
        }
    }
}