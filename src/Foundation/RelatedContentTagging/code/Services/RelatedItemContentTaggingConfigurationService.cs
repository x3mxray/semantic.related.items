using System;
using System.Collections.Generic;
using System.Xml;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Configuration;
using Sitecore.Xml;

namespace Semantic.Foundation.RelatedContentTagging.Services
{
    public class RelatedItemContentTaggingConfigurationService : IRelatedItemContentTaggingConfigurationService
    {
        protected BaseFactory ConfigurationFactory;
        protected Lazy<IDictionary<string, ItemContentTaggingConfiguration>> ItemContentTaggingConfigurations;

      
        public RelatedItemContentTaggingConfigurationService(BaseFactory configurationFactory)
        {
            this.ConfigurationFactory = configurationFactory;
            this.ItemContentTaggingConfigurations = new Lazy<IDictionary<string, ItemContentTaggingConfiguration>>(this.InitializeItemContentTaggingConfigurations);
        }

        public virtual ItemContentTaggingConfiguration GetConfigurationByName(string configurationName)
        {
            if (!ItemContentTaggingConfigurations.Value.TryGetValue(configurationName, out var taggingConfiguration))
                taggingConfiguration = new ItemContentTaggingConfiguration
                {
                    ContentProviders = new List<string>(),
                    DiscoveryProviders = new List<string>(),
                    Taggers = new List<string>(),
                };
            return taggingConfiguration;
        }

        protected virtual IDictionary<string, ItemContentTaggingConfiguration> InitializeItemContentTaggingConfigurations()
        {
            Dictionary<string, ItemContentTaggingConfiguration> dictionary = new Dictionary<string, ItemContentTaggingConfiguration>();
            foreach (string configurationsName in this.GetAllConfigurationsNames())
                dictionary.Add(configurationsName, this.GetConfiguration(configurationsName));
            return dictionary;
        }

        protected ItemContentTaggingConfiguration GetConfiguration(string configurationName)
        {
            return new ItemContentTaggingConfiguration
            {
                ContentProviders = this.GetConfigurationProvidersNames(configurationName, "content"),
                DiscoveryProviders = this.GetConfigurationProvidersNames(configurationName, "discovery"),
                Taggers = this.GetConfigurationProvidersNames(configurationName, "tagger")
            };
        }

        protected virtual IEnumerable<string> GetConfigurationProvidersNames(string configurationName, string category)
        {
            var stringList = new List<string>();
            foreach (XmlNode configNode in this.ConfigurationFactory.GetConfigNodes("relatedContentTagging/configurations/config[@name='" + configurationName + "']/" + category + "/provider"))
            {
                string attribute = XmlUtil.GetAttribute("name", configNode);
                if (!string.IsNullOrWhiteSpace(attribute))
                    stringList.Add(attribute);
            }
            return stringList;
        }


        protected virtual IEnumerable<string> GetAllConfigurationsNames()
        {
            var stringList = new List<string>();
            XmlNodeList configNodes = this.ConfigurationFactory.GetConfigNodes("relatedContentTagging/configurations/config");
            if (configNodes != null)
            {
                foreach (XmlNode node in configNodes)
                {
                    string attribute = XmlUtil.GetAttribute("name", node);
                    if (!string.IsNullOrWhiteSpace(attribute))
                        stringList.Add(attribute);
                }
            }
            return stringList;
        }
    }

    public interface IRelatedItemContentTaggingConfigurationService
    {
        ItemContentTaggingConfiguration GetConfigurationByName(string configurationName);
    }
}