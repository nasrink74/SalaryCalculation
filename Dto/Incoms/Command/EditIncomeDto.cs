namespace Dto.Incoms.Command
{
    public class EditIncomeDto
    {

        public int UserId { get; set; }
        public long BasicSalary { get; set; }       //حقوقو پایه
        public long Allowance { get; set; }         //حق جذب
        public long Transportation { get; set; }    //حق ایاب و ذهاب
        public string OverTimeCalculator { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
