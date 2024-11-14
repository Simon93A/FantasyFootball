using System.ComponentModel.DataAnnotations;

namespace fantacyfotball_api.Models.DTOs
{
    public class ShowTeamDTO
    {
        
        public string _id { get; set; }
        public string TeamName { get; set; }
        public List<Player> Players { get; set; }
        public string UserID { get; set; }
    }
}
