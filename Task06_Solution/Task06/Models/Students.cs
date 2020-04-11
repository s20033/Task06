using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task06.Models
{
    public class Students
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Enrollment IdEnrollment { get; set; }
        public Study Studies { get; set; }

       
    }
}
