using System;
using System.Collections.Generic;
using System.Xml;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Configuration;
using Sitecore.Xml;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services
{
    public class RelatedItemContentTaggingConfigurationService
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
                    TaxonomyProviders = new List<string>()
                };
            return taggingConfiguration;
        }

        /// <summary>Reads all configurations from web config</summary>
        /// <returns></returns>
        protected virtual IDictionary<string, ItemContentTaggingConfiguration> InitializeItemContentTaggingConfigurations()
        {
            Dictionary<string, ItemContentTaggingConfiguration> dictionary = new Dictionary<string, ItemContentTaggingConfiguration>();
            foreach (string configurationsName in this.GetAllConfigurationsNames())
                dictionary.Add(configurationsName, this.GetConfiguration(configurationsName));
            return dictionary;
        }

        /// <summary>
        /// Get Item Content Tagging configuration model from sitecore configuration by name
        /// </summary>
        /// <param name="configurationName"></param>
        /// <returns></returns>
        protected ItemContentTaggingConfiguration GetConfiguration(string configurationName)
        {
            return new ItemContentTaggingConfiguration
            {
                ContentProviders = this.GetConfigurationProvidersNames(configurationName, "content"),
                DiscoveryProviders = this.GetConfigurationProvidersNames(configurationName, "discovery"),
                TaxonomyProviders = this.GetConfigurationProvidersNames(configurationName, "taxonomy"),
                Taggers = this.GetConfigurationProvidersNames(configurationName, "tagger")
            };
        }

        /// <summary>Get list of pro</summary>
        /// <param name="configurationName">Item content tagging configuration name</param>
        /// <param name="category">content tagging provider category</param>
        /// <returns></returns>
        protected virtual IEnumerable<string> GetConfigurationProvidersNames(string configurationName, string category)
        {
            List<string> stringList = new List<string>();
            foreach (XmlNode configNode in this.ConfigurationFactory.GetConfigNodes("relatedContentTagging/configurations/config[@name='" + configurationName + "']/" + category + "/provider"))
            {
                string attribute = XmlUtil.GetAttribute("name", configNode);
                if (!string.IsNullOrWhiteSpace(attribute))
                    stringList.Add(attribute);
            }
            return stringList;
        }

        /// <summary>Returns all configurations names</summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> GetAllConfigurationsNames()
        {
            List<string> stringList = new List<string>();
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
        /// <summary>
        /// Get ItemContentTaggingConfiguration by configuration name
        /// </summary>
        /// <param name="configurationName"></param>
        /// <returns></returns>
        ItemContentTaggingConfiguration GetConfigurationByName(string configurationName);
    }
}