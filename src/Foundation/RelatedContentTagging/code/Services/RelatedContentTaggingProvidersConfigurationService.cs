using System;
using System.Collections.Generic;
using System.Xml;
using Sitecore.Abstractions;
using Sitecore.Reflection;
using Sitecore.Xml;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services
{
    public class RelatedContentTaggingProvidersConfigurationService : IRelatedContentTaggingProvidersConfigurationService
    {
        protected IList<string> ProviderCategories = new List<string>
        {
            "content",
            "tagger",
            "discovery"
        };
    
        protected Lazy<IDictionary<string, Type>> ProvidersConfiguration;
      
        protected BaseFactory ConfigurationFactory;

      public RelatedContentTaggingProvidersConfigurationService(BaseFactory configurationFactory)
        {
            this.ProvidersConfiguration = new Lazy<IDictionary<string, Type>>(this.ReadProvidersConfiguration);
            this.ConfigurationFactory = configurationFactory;
        }

        public Type GetProviderTypeByName(string providerName)
        {
            if (!this.ProvidersConfiguration.Value.TryGetValue(providerName, out var type))
                return (Type)null;
            return type;
        }

      
        private IDictionary<string, Type> ReadProvidersConfiguration()
        {
            Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
            foreach (string providerCategory in this.ProviderCategories)
            {
                foreach (XmlNode configNode in this.ConfigurationFactory.GetConfigNodes("relatedContentTagging/providers/" + providerCategory + "/add"))
                {
                    string attribute = XmlUtil.GetAttribute("name", configNode);
                    Type providerType = this.GetProviderType(configNode);
                    if (!string.IsNullOrWhiteSpace(attribute) && providerType != (Type)null)
                        dictionary.Add(attribute, providerType);
                }
            }
            return dictionary;
        }

        protected virtual Type GetProviderType(XmlNode configNode)
        {
            string attribute = XmlUtil.GetAttribute("type", configNode);
            if (attribute.Length <= 0)
                return (Type)null;
            return ReflectionUtil.GetTypeInfo(attribute);
        }
    }

    public interface IRelatedContentTaggingProvidersConfigurationService
    {
        Type GetProviderTypeByName(string providerName);
    }
}