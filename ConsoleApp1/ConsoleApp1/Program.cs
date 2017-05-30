using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net;

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

            //foreach(var line in fileContents)
            //    Console.WriteLine(line.firstName);

            //Console.WriteLine(GetNewFromPlayer("Yulius"));

            var player = GetNewFromPlayer("Alvin sumara");
            foreach(var name in player)
            {
                Console.WriteLine("Tanggal : {0}", name.datePublished);
                Console.WriteLine("URL : {0}", name.url);
            }
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

        public static string GetGoogleHomePage()
        {
            var webClient = new WebClient();
            byte[] googleHome = webClient.DownloadData("https://www.dotnetperls.com/static"); //ambil HTML input lewat bite[]

            using (var stream = new MemoryStream(googleHome))//masukin data ke suatu variable
            using (var reader = new StreamReader(stream))//di terjemahin di sini
            {
                return reader.ReadToEnd();// di baca di sini
            }
        }

        public static List<NewsResult> GetNewFromPlayer(string playerName)
        {
            var results = new List<NewsResult>();
            var webClient = new WebClient();
            webClient.Headers.Add("Ocp-Apim-Subscription-Key", "a19b42b1aec44371aede1c06414907f0");
            byte[] playerNews = webClient.DownloadData(string.Format("https://api.cognitive.microsoft.com/bing/v5.0/news/search?q={0}&mkt=en-us", playerName)); //ambil HTML input lewat bite[]
            var serializer = new JsonSerializer();

            using (var stream = new MemoryStream(playerNews))//masukin data ke suatu variable
            using (var reader = new StreamReader(stream))//di terjemahin di sini
            using (var jsonReader = new JsonTextReader(reader))
            {
                results = serializer.Deserialize<NewsSearch>(jsonReader).newsResults;
            }

            return results;
        }
    }

}

