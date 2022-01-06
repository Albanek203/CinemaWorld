using System.Linq.Expressions;
using DLL.Context;
using DLL.Interfaces;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository {
    public class FilmRepository : BaseRepository<Film> {
        public FilmRepository(CinemaContext context) : base(context) { }
        public override async Task<IReadOnlyCollection<Film>> GetAllAsync() {
            return await Entities.Include(session => session.Sessions)!
                                 .ThenInclude(session => session!.Seats)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
        public override async Task<IReadOnlyCollection<Film>> FindByConditionAsync(
                Expression<Func<Film, bool>> predicate) {
            return await Entities.Where(predicate).Include(session => session.Sessions)!
                                 .ThenInclude(session => session!.Seats)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
    }
}