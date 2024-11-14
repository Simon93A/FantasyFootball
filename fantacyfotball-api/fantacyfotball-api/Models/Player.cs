using System.ComponentModel.DataAnnotations;

namespace fantacyfotball_api.Models
{
    public class Player
    {
        [Key]
        public int _id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }
    }
}
