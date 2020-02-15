using System.Text;
using TrelloSharpEasy.Services;

namespace DemoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var appKey = "";
            var userToken = "";

            var easyService = new EasyService(appKey, userToken);

            var board = easyService.GetBoard("");

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
