using System;
using System.Linq;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
using Sitecore.Shell.Framework.Commands;

namespace Semantic.Foundation.RelatedContentTagging.Providers
{
    public class ButtonStateProvider : IButtonStateProvider
    {
        [NonSerialized]
        protected readonly BaseCorePipelineManager PipelineManager;

        public ButtonStateProvider(BaseCorePipelineManager baseCorePipelineManager)
        {
            this.PipelineManager = baseCorePipelineManager;
        }

        public CommandState GetButtonStateForUser(Item item, User user)
        {
            if (item == null)
                return CommandState.Hidden;
            if (item.Appearance.ReadOnly || !item.Security.CanWrite(user))
                return CommandState.Disabled;
            var configurationArgs = new GetRelatedContentTaggingConfigurationArgs();
            this.PipelineManager.Run("getRelatedTaggingConfiguration", configurationArgs, "RealtedContentTagging");
            bool flag1 = configurationArgs.ProvidersSet.DiscoveryProvider!=null;
            bool flag2 = configurationArgs.ProvidersSet.ContentProviders.Any();
            bool flag3 = configurationArgs.ProvidersSet.Taggers.Any();
            return this.CheckLocking(item, user) & flag2 & flag1 & flag3 ? CommandState.Enabled : CommandState.Disabled;
        }

        protected virtual bool CheckLocking(Item item, User user)
        {
            return user.IsAdministrator || item.Locking.IsLocked() && item.Locking.HasLock();
        }
    }

    public interface IButtonStateProvider
    {
        CommandState GetButtonStateForUser(Item item, User user);
    }
}