using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Neo4jClient;
using System.Configuration;

namespace GraphTestBot.Dialogs
{
    [Serializable]
    public class CompanyDialog : IDialog<object>
    {
        private GraphClient graphClient;
        
        public CompanyDialog()
        {
            var appSettings = ConfigurationManager.AppSettings;
            graphClient = new GraphClient(new Uri(appSettings["neo4jDB"]), appSettings["neo4jUser"], appSettings["neo4jPassword"]);
            graphClient.Connect();
        }

        public async Task StartAsync(IDialogContext context)
        {
            DAL.graph = graphClient;

            context.Fail(new NotImplementedException("Search using CompanyDialog is not implemented and is instead being used to show context.Fail"));
        }

    }
}