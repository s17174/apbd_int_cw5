using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_int_cw5.Requests
{
    public class PromoteStudentRequest
    {
        [Required(ErrorMessage = "Nazwa studiów jest wymagana")]
        public string Studies { get; set; }
        [Required(ErrorMessage = "Numer semestru jest wymagany")]
        [RegularExpression("[1-9][0-9]{0,1}")]
        public int Semester { get; set; }
    }
}
