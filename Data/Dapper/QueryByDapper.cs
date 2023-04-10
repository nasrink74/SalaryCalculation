using Common;

namespace Data.Dapper
{
    public class QueryByDapper : IQueryByDapper, IScopedDependency
    {
        public string GetIncomes => "select * from dbo.Income where UserId=@UserId And Date >= @from And Date <= @to";

        public string GetUserByUserId => "select * from dbo.[User] where Id=@Id";
        public string GetAllUser=> "select * from dbo.[User]";
    }
}
