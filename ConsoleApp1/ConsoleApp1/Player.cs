using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{

    public class RootObject
    {
        public Player[] PLayer { get; set; }
    }

    public class Player
    {
       [JsonProperty(PropertyName = "first_name")]
        public string firstName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "point_per_game")]
        public string pointsPerGame { get; set; }
        [JsonProperty(PropertyName = "second_name")]
        public string secondName { get; set; }
        [JsonProperty(PropertyName = "team_name")]
        public string teamName { get; set; }
      
    }

}
