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
            var appKey = Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_APP_KEY", EnvironmentVariableTarget.User);
            var userToken = Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_USER_TOKEN", EnvironmentVariableTarget.User);
            var boardId = Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_BOARD_ID", EnvironmentVariableTarget.User);
            var hourlyRate = Convert.ToDecimal(Environment.GetEnvironmentVariable("TRELLO_SHARP_EASY_HOURLY_RATE", EnvironmentVariableTarget.User));

            var easyService = new EasyService(appKey, userToken);

            var board = easyService.GetBoard(boardId);

            var list = board.Lists.Find(l => l.Name.Equals("Aguardando Aprovação"));
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
