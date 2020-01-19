using Sitecore.Abstractions;
using Sitecore.ContentTagging.Pipelines.GetTaggingConfiguration;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines
{
    public class GetDefaultConfigurationName
    {
        /// <summary>BaseSettings</summary>
        protected BaseSettings Settings;

        /// <summary>Constructor</summary>
        /// <param name="settings">setting</param>
        public GetDefaultConfigurationName(BaseSettings settings)
        {
            this.Settings = settings;
        }

        /// <summary>Pipeline processor entry point</summary>
        /// <param name="args"></param>
        public void Process(GetTaggingConfigurationArgs args)
        {
            string setting = this.Settings.GetSetting("RelatedContentTagging.DefaultConfigurationName", "Default");
            args.ConfigurationName = setting;
        }
    }
}