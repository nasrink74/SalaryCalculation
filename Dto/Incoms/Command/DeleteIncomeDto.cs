namespace Dto.Incoms.Command
{
    public class DeleteIncomeDto
    {
        public int UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
