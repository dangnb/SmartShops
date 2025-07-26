using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dappers.Repositories;
public interface IUserRepository : IGenericRepository<Entities.Identity.AppUser>
{
}
