using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProje.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime? CreateDate { get; set; }

        //buna sıra gelecek :D
        //public int? StudentId { get; set; }
        //[ForeignKey("StudentId")]
        //public Student Student { get; set; }
    }
}
