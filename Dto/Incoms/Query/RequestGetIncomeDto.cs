namespace Dto.Incoms.Query
{
    public class RequestGetIncomeDto
    {
        public int UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
