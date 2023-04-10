using Common;
using Data.Repositories.General.Command.Repository;
using Data.Repositories.Incomes.Command.Interface;
using Entities;

namespace Data.Repositories.Incomes.Command.Repository
{
    public class CommandIncomeRepository: CommandBaseRepository<Income>, ICommandIncomeRepository, IScopedDependency
    {
        public CommandIncomeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
