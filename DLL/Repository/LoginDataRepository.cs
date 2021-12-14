using System.Linq.Expressions;
using DLL.Context;
using DLL.Interfaces;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository {
    public class LoginDataRepository : BaseRepository<LoginData> {
        public LoginDataRepository(CinemaContext context) : base(context) { }
        public override async Task<IReadOnlyCollection<LoginData>> GetAllAsync() {
            return await Entities.Include(user => user.User)
                                 .ThenInclude(user => user!.Person)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
        public override async Task<IReadOnlyCollection<LoginData>> FindByConditionAsync(
                Expression<Func<LoginData, bool>> predicate) {
            return await Entities.Where(predicate)
                                 .Include(user => user.User)
                                 .ThenInclude(user => user!.Person)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
    }
}