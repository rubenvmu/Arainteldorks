using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Araintelsoftware.Areas.Identity.Data
{
    public class SampleUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}