using System.ComponentModel.DataAnnotations;

namespace apbd_int_cw5.Requests
{
    public class PromotionRequest
    {
        [Required]
        public string Studies { get; set; }

        [Required]
        public int Semester { get; set; }
    }
}
