using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.ContentTagging.Jobs;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using Sitecore.Shell.Applications.Dialogs.ProgressBoxes;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Extentions;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services;
using Sitecore;
using Sitecore.ContentTagging;
using Message = Sitecore.ContentTagging.Core.Messaging.Message;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Commands
{
    public class RelatedContentCommand : Command
    {
        /// <summary>Base pipeline manager</summary>
        [NonSerialized]
        protected readonly BaseCorePipelineManager PipelineManager;

        /// <summary>Content tagging runner</summary>
        public IRelatedContentTaggingRunner ContentTaggingRunner { get; set; }

        /// <summary>MessageBusFactory Factory</summary>
        public IMessageBusFactory MessageBusFactory { get; set; }

        /// <summary>ButtonStateProvider</summary>
        public Providers.IButtonStateProvider ButtonStateProvider { get; set; }

        /// <summary>Constructor</summary>
        public RelatedContentCommand()
        {
            this.ContentTaggingRunner = ServiceLocator.ServiceProvider.GetService<IRelatedContentTaggingRunner>();
            this.MessageBusFactory = ServiceLocator.ServiceProvider.GetService<IMessageBusFactory>();
            this.PipelineManager = ServiceLocator.ServiceProvider.GetService<BaseCorePipelineManager>();
            this.ButtonStateProvider = ServiceLocator.ServiceProvider.GetService<Providers.IButtonStateProvider>();
        }

        /// <inheritdoc />
        public override void Execute(CommandContext context)
        {
            Item obj = context.Items[0];
            NameValueCollection parameters = context.Parameters;
            string str1;
            if (parameters == null)
            {
                str1 = (string)null;
            }
            else
            {
                string index = "tagtree";
                str1 = parameters[index];
            }
            string str2 = str1;
            bool flag = str2 != null && bool.Parse(str2);
            var source = new List<Item>();
            if (flag)
            {
                var descendants = obj.Axes.GetDescendants().Where(x => x.IsDerived(Constants.Templates.ReletedContentTagging));
                if(obj.IsDerived(Constants.Templates.ReletedContentTagging))
                    source.Add(obj);
                source.AddRange(descendants);
            }
            else if (obj.IsDerived(Constants.Templates.ReletedContentTagging))
                source.Add(obj);
            if (!source.Any())
                return;
            MessageBus messageBus = this.MessageBusFactory.Create();
            JobContextMessageHandler contextMessageHandler = new JobContextMessageHandler();
            messageBus.Subscribe(contextMessageHandler);
            ClientPipelineArgs args = new ClientPipelineArgs();
            args.CustomData["items"] = source;
            args.CustomData["messageBus"] = messageBus;
            Context.ClientPage.Start(this, "Run", args);
        }

       

        /// <summary>Runs the command in ProgressBox.</summary>
        /// <param name="args">The args.</param>
        protected void Run(ClientPipelineArgs args)
        {
            ProgressBox.ExecuteSync(Translate.Text("Related Content Tagging"), Translate.Text("Related Tagging Content"), "People/16x16/hammer.png", this.RunTagging, this.TaggingDone);
        }

        /// <inheritdoc />
        public override CommandState QueryState(CommandContext context)
        {
            Item obj;
            if ((obj = context.Items.FirstOrDefault()) != null)
                return this.ButtonStateProvider.GetButtonStateForUser(obj, Context.User);
            return CommandState.Disabled;
        }

        /// <summary>Runs process of tagging content</summary>
        /// <param name="args"></param>
        protected void RunTagging(ClientPipelineArgs args)
        {
            object obj = args.CustomData["items"];
            MessageBus messageBus = (MessageBus)args.CustomData["messageBus"];
            IEnumerable<Item> objs;
            if ((objs = obj as IEnumerable<Item>) != null)
            {
                foreach (Item contentItem in objs)
                {
                    using (new SecurityEnabler())
                    {
                        if (contentItem.Security.CanWrite(Context.User))
                        {
                            this.ContentTaggingRunner.Run(contentItem, messageBus);
                        }
                        else
                        {
                            string str = Translate.Text("Item '{0}' was not tagging. User doesn't have write access to it.", (object)contentItem.Paths.Path);
                            messageBus.SendMessage(new Message
                            {
                                Body = str,
                                Level = MessageLevel.Warning
                            });
                            Log.Warn(string.Format(CultureInfo.InvariantCulture, "Item '{0}' was not tagging. User doesn't have write access to it.", contentItem.Paths.Path), this);
                        }
                    }
                }
            }
            Thread.Sleep(500);
        }

        /// <summary>Callback on tagging process done</summary>
        /// <param name="args"></param>
        protected void TaggingDone(ClientPipelineArgs args)
        {
            MessageBus messageBus = (MessageBus)args.CustomData["messageBus"];
            IEnumerable<Message> receivedMessages1 = messageBus.ReceivedMessages;
            bool Func1(Message x) => x.Level == MessageLevel.Error;
            Func<Message, bool> predicate1 = Func1;
            if (receivedMessages1.Any(predicate1))
            {
                SheerResponse.ShowError(Translate.Text("Error"), Translate.Text("There was an error during tagging. Ask your system administrator for more information."));
            }
            else
            {
                IEnumerable<Message> receivedMessages2 = messageBus.ReceivedMessages;
                bool Func2(Message x) => x.Level == MessageLevel.Warning;
                Func<Message, bool> predicate2=Func2;
                if (!receivedMessages2.Any(predicate2))
                    return;
                SheerResponse.Alert(Translate.Text("There were warnings during tagging. Ask your system administrator for more information."), Array.Empty<string>());
            }
        }
    }
}