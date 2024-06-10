using System.ComponentModel.DataAnnotations;

namespace Araintelsoftware.Models
{
    public class Secrets
    {
        [Key]

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}