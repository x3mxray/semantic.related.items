﻿using Semantic.Foundation.RelatedContentTagging.Providers;
using Sitecore.Abstractions;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines
{
    public class GetDefaultConfigurationName
    {
        protected BaseSettings Settings;

        public GetDefaultConfigurationName(BaseSettings settings)
        {
            this.Settings = settings;
        }

        public void Process(GetRelatedContentTaggingConfigurationArgs args)
        {
            string setting = this.Settings.GetSetting("RelatedContentTagging.DefaultConfigurationName", "Default");
            args.ConfigurationName = setting;
        }
    }
}