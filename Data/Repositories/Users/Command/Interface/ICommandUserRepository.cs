using Data.Repositories.General.Command.Interface;
using Entities;

namespace Data.Repositories.Users.Command.Interface
{
    public interface ICommandUserRepository : ICommandBaseRepository<User>
    {
    }
}
