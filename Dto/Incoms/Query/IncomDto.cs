using System.ComponentModel.DataAnnotations;

namespace Dto.Incoms.Query
{
    public class IncomDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
    }
}
