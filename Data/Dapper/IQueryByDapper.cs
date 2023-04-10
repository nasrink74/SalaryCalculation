namespace Data.Dapper
{
    public interface IQueryByDapper
    {
        string GetIncomes { get; }
        string GetUserByUserId { get; }
        string GetAllUser { get; }
    }
}
