using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Reflex.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            var userName = string.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("Hello, My name is Reflex, I am here to help you as an intelligent service of My Bank.");
            }
            await Respond(context);

            if (string.IsNullOrEmpty(userName))
                context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
            var userName = string.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("What is your name?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(string.Format("Hello {0}. How may I help you today?", userName));
                context.Done(context.Activity);
            }
            
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var message = await activity;
            var userName = string.Empty;
            var getName = false;

            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName && string.IsNullOrEmpty(userName) )
            {
                userName = message.Text.Trim();
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", getName);

                await Respond(context);
            }

            //context.Done(message);
        }
    }
}