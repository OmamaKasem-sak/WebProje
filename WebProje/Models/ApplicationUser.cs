using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProje.Models
{
    public class ApplicationUser:IdentityUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        public String PlaceOfBirth { get; set; }

        public string University { get; set; }

        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public int? HomelandId { get; set; }
        [ForeignKey("HomelandId")]

        public Homeland Homeland { get; set; }
    }
}
