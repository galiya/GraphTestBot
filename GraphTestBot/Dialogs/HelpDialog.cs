using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace GraphTestBot.Dialogs
{
    //TODO: Change to a help dialog

    [Serializable]
    public class HelpDialog: IDialog<int>
    {

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var ticketNumber = new Random().Next(0, 20000);
            await context.PostAsync($"Your message '{message.Text}' was registered. Once we resolve it; we will get back to you.");
            context.Done(ticketNumber);
        }
    }
}