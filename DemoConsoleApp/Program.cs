using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.RegularExpressions;
using TrelloSharpEasy.Services;

namespace DemoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var appKey = Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_APP_KEY", EnvironmentVariableTarget.User);
            //var userToken = Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_USER_TOKEN", EnvironmentVariableTarget.User);

            var trelloAppKey = "8b51561a66106fabea5ab91fd31f86e2";
            var trelloUserToken = "111a737b23e7afe3eb94462753239f7377af921cdcf36480aeab17800faaa6a0";

            var fakeLogger = new FakeLogger<EasyService>();
            var easyService = new EasyService(trelloAppKey, trelloUserToken, fakeLogger);

            //DemoBudget(easyService);
            DemoListCards(easyService);
        }

        private static void DemoListCards(EasyService easyService)
        {
            //var boardId = Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_BOARD_ID", EnvironmentVariableTarget.User);
            var boardId = "5e93acca8ab6967b32e6fd9d";

            var board = easyService.GetBoard(boardId, false, false);

            var sb = new StringBuilder();

            foreach (var list in board.Lists)
            {
                foreach (var card in list.Cards)
                {
                    Console.WriteLine(card.Name);
                    sb.AppendLine(card.Name);
                }
            }

            var tasks = sb.ToString();
        }

        static void DemoBudget(EasyService easyService)
        {
            var boardId = Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_BOARD_ID", EnvironmentVariableTarget.User);
            var hourlyRate = Convert.ToDecimal(Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_HOURLY_RATE", EnvironmentVariableTarget.User));

            var board = easyService.GetBoard(boardId);

            var list = board.Lists.Find(l => l.Name.Equals("A Fazer"));
            var sb = new StringBuilder();
            var regex = new Regex(@"\(\d\d:\d\d\)");

            foreach (var card in list.Cards)
            {
                sb.AppendLine(card.Name);

                var analise = card.CheckLists.Find(chk => chk.Name.Equals("Análise"));
                if (analise != null)
                {
                    foreach (var item in analise.Items)
                    {
                        var hour = regex.Match(item.Name).Value;
                        if (String.IsNullOrEmpty(hour))
                            throw new Exception(item.Name + " sem não estimado.");

                        var name = item.Name.Replace(hour, "");
                        hour = hour.Replace("(", "").Replace(")", "");

                        var price = hourlyRate * ConvertHourToDecimal(hour);

                        sb.AppendLine($"'-   {name}\t{hour}\t{price.ToString("#,##0.00")}");
                    }
                }

            }

            var tasks = sb.ToString();
        }

        private static decimal ConvertHourToDecimal(string hhmm)
        {
            var array = hhmm.Split(':');

            var hours = Convert.ToDecimal(array[0]);
            var minutes = Convert.ToDecimal(array[1]);

            minutes = Math.Round(minutes / 60, 2, MidpointRounding.AwayFromZero);

            return hours + minutes;
        }
    }
}
