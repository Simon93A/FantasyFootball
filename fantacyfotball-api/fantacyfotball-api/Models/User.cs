using System.ComponentModel.DataAnnotations;

namespace fantacyfotball_api.Models
{
    public class User
    {
        [Key]
        public string _id { get; set; }
        public string UserName { get; set; }
        public decimal Money { get; set; }
        public string? TeamId { get; set; }

    }
}
