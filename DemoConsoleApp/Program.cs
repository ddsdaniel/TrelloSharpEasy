using System.Text;
using TrelloSharpEasy.Services;

namespace DemoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var appKey = "8b51561a66106fabea5ab91fd31f86e2";
            var userToken = "111a737b23e7afe3eb94462753239f7377af921cdcf36480aeab17800faaa6a0";

            var easyService = new EasyService(appKey, userToken);

            var board = easyService.GetBoard("5e2774560cba331e300b036b");

            var list = board.Lists.Find(l => l.Name.Equals("Aguardando Aprovação"));
            var sb = new StringBuilder();

            foreach (var card in list.Cards)
            {
                sb.AppendLine(card.Name);

                foreach (var checkList in card.CheckLists)
                {
                    foreach (var item in checkList.Items)
                    {
                        sb.AppendLine($"- {item.Name}");
                    }
                }

                sb.AppendLine();
            }

            var tasks = sb.ToString();
        }
    }
}
