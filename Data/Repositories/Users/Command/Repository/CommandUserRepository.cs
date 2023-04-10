using Common;
using Data.Repositories.General.Command.Repository;
using Data.Repositories.Users.Command.Interface;
using Entities;

namespace Data.Repositories.Users.Command.Repository
{
    public class CommandUserRepository: CommandBaseRepository<User>, ICommandUserRepository, IScopedDependency
    {
        public CommandUserRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
    }
}
