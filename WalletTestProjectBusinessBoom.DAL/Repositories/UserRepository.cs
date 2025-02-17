using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WalletTestProjectBusinessBoom.Core.Entities;
using WalletTestProjectBusinessBoom.Core.Interfaces;

namespace WalletTestProjectBusinessBoom.DAL.Repositories
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {
    }
}
