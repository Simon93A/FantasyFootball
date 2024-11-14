namespace fantacyfotball_api.Models.DTOs
{
    public class CreateTeamDTO
    {
        public string userID { get; set; }
        public string TeamName { get; set; }
        public List<int> PlayerIDList { get; set; }
    }
}
