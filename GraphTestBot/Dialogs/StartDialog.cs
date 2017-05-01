using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace GraphTestBot.Dialogs
{
    [Serializable]
    public class StartDialog: IDialog<object>
    {
        private const string PersonOption = "By Person";
        private const string CompanyOption = "By Company";
        private const string CountryOption = "By Country";
        private const string JurisdictionOption = "By Jurisdiction";

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Text.ToLower().Contains("help") || message.Text.ToLower().Contains("support"))
            {
                // Change to Global Message Handling: https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-GlobalMessageHandlers

                await context.Forward(new HelpDialog(), this.ResumeAfterSupportDialog, message, CancellationToken.None);
            }
            // Add option for a proactive Bot
            else
            {
                
                this.ShowOptions(context);
            }
        }
        private void ShowOptions(IDialogContext context)
        {
            //PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { PersonOption, CompanyOption }, "How do you like to explore Panama Papers?", "Not a valid option", 3);
            PromptDialog.Choice(context, OnOptionSelected,
                new List<string>() { PersonOption, CompanyOption },
                "Hi, I'm GraphTestBot. I'm here to help. I can't answer all your questions, but I can help you to explore ICIJ 'Panama Papers' database and search for people, companies, countries or jurisdictions and relations between those.",
                "Not a valid option",
                3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;
                switch (optionSelected)
                {
                    case PersonOption:
                        context.Call(new PersonDialog(), ResumeAfterOptionDialog);
                        break;
                    case CompanyOption:
                        context.Call(new CompanyDialog(), ResumeAfterOptionDialog);
                        break;
                }

            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");
                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        {
            var ticketNumber = await result;
            await context.PostAsync($"Thanks for contacting our support team. Your ticket number is {ticketNumber}.");
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}