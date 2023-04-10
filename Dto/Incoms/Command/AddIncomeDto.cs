using System.ComponentModel.DataAnnotations;

namespace Dto.Incoms.Command
{
    public class AddIncomeDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public long BasicSalary { get; set; }       //حقوقو پایه
        [Required]
        public long Allowance { get; set; }         //حق جذب
        [Required]
        public long Transportation { get; set; }    //حق ایاب و ذهاب
        [Required]
        public string OverTimeCalculator { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
    }
}
