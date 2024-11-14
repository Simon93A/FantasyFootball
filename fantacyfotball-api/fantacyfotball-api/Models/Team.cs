using System.ComponentModel.DataAnnotations;

namespace fantacyfotball_api.Models
{
    public class Team
    {
        [Key]
        public string _id { get; set; }
        public string Name { get; set; }
        public List<int> PlayersID { get; set; }
        public string UserID { get; set; }

    }
}
