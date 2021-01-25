using System;
using System.ComponentModel.DataAnnotations;



namespace apbd_int_cw5.Requests
{
    public class EnrollStudentRequest
    {
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        // public string Email { get; set; }

        [Required(ErrorMessage = "Musisz podać imię")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwisko")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Musisz podać date urodzenia")]
        public String BirthDate { get; set; }
        //public object BirthDate { get; internal set; }
        [Required]
        public string Studies { get; set; }
    }
}