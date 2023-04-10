namespace Dto.Incoms.Query
{
    public class GetAllIncomeDto
    {
        public long BasicSalary { get; set; }       //حقوقو پایه
        public long Allowance { get; set; }         //حق جذب
        public long Transportation { get; set; }    //حق ایاب و ذهاب
        public string Date { get; set; }
        public long Receipt { get; set; }
    }
}
