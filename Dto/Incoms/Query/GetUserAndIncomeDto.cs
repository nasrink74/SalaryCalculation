namespace Dto.Incoms.Query
{
    public class GetUserAndIncomeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PersonCode { get; set; }
        public List<GetAllIncomeDto> incomeDtos { get; set; }
    }
}
