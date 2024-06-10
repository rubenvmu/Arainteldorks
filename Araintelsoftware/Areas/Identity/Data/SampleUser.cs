using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Araintelsoftware.Areas.Identity.Data
{
    public class SampleUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public string FullName

        {

            get { return FirstName + " " + LastName; }

            set

            {

            }
        }

    }
}
