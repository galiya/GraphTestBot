using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Neo4jClient;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Bot.Builder.Dialogs;
using GraphTestBot.Dialogs;

namespace GraphTestBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));


                await Conversation.SendAsync(activity, () => new StartDialog());

                //var results = graphClient.Cypher
                //    .Match("(people:Person)")
                //    .Return(people => people.As<Actor>())
                //    .Limit(5)
                //    .Results;

                //// return our reply to the user
                //Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");

                //reply.Attachments = new List<Attachment>();

                //List<CardImage> cardImages = new List<CardImage>();
                //List<CardAction> cardButtons = new List<CardAction>();

                //foreach (var item in results)
                //{
                    
                //    cardImages.Add(new CardImage(url: "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png"));

                //    CardAction plButton = new CardAction()
                //    {
                //        Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                //        Type = "openUrl",
                //        Title = "WikiPedia Page"
                //    };
                //    cardButtons.Add(plButton);

                //    HeroCard plCard = new HeroCard()
                //    {
                //        Title = string.Format("Actor: {0}, born in {1}", item.name, item.born),
                //        Images = cardImages,
                //        Buttons = cardButtons
                //    };

                //    reply.Attachments.Add(plCard.ToAttachment());
                //}
                

                //await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}