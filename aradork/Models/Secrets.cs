using System.ComponentModel.DataAnnotations;

namespace aradork.Models
{
    public class Secrets
    {
        [Key]

        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

    }
}