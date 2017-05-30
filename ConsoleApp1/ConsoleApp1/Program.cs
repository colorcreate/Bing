using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "players.json");

            var fileContents = DeserializedPlayer(fileName);

            foreach(var line in fileContents)
                Console.WriteLine(line.first_name);
        }
        public static List<GameResults> ReadFootballResults (string fileName)
        {
            var soccerResult = new List<GameResults>();
            using (var reader = new StreamReader(fileName))
            {
                var line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    GameResults gr = new GameResults();
                    DateTime gameDate;
                    HomeOrAway HomeOrAway;
                    int parseInt;
                    Double PossessionPercent;

                    if (DateTime.TryParse(values[0], out gameDate))
                        gr.GameDate = gameDate;

                    gr.TeamName = values[1];

                    if (Enum.TryParse(values[2], out HomeOrAway))
                        gr.HomeOrAway = HomeOrAway;

                    if (Enum.TryParse(values[3], out parseInt))
                        gr.Goals = parseInt;

                    if (Enum.TryParse(values[4], out parseInt))
                        gr.GoalAttempts = parseInt;

                    if (Enum.TryParse(values[5], out parseInt))
                        gr.ShotsOnGoal = parseInt;

                    if (Enum.TryParse(values[6], out parseInt))
                        gr.ShotsOffGoal = parseInt;
                    
                    if (Double.TryParse(values[7], out PossessionPercent))
                    {
                       gr.PossessionPercent = PossessionPercent;
                    }


                    soccerResult.Add(gr);
                }
                return soccerResult;
            }
        }

        public static List<Player> DeserializedPlayer(string fileName)
        {
            var players = new List<Player>();
            var serializer = new JsonSerializer();

            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }

                return players;
        }
    }

}
