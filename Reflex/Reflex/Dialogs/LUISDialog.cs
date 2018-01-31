using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Reflex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Reflex.Dialogs
{
    [LuisModel("2ce3f237-6ada-47ed-8a74-6dbcf98ca014", "54089de0f3f94fdf87dbe01f4a2ff4b9")]
    [Serializable]
    public class LUISDialog : LuisDialog<FinanceCalculation>
    {
        private readonly BuildFormDelegate<FinanceCalculation> FinanceCalculationForm;
        [field: NonSerialized()]
        protected Activity _message;

        public LUISDialog(BuildFormDelegate<FinanceCalculation> financeCalculationForm)
        {
            this.FinanceCalculationForm = financeCalculationForm;
        }

        protected override async Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            _message = (Activity)await item;
            try
            {
                await base.MessageReceived(context, item);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        [LuisIntent("FinanceCalculation")]
        public async Task FinanceCalculation(IDialogContext context, LuisResult result)
        {
            var form = new FormDialog<FinanceCalculation>(new FinanceCalculation(), this.FinanceCalculationForm, FormOptions.PromptInStart);
            context.Call<FinanceCalculation>(form, Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}