using System.Linq.Expressions;
using DLL.Context;
using DLL.Interfaces;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository {
    public class SessionRepository : BaseRepository<Session> {
        public SessionRepository(CinemaContext context) : base(context) { }
        public override async Task<IReadOnlyCollection<Session>> GetAllAsync() {
            return await Entities.Include(film => film.Film)
                                 .Include(seats => seats.Seats)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
        public override async Task<IReadOnlyCollection<Session>> FindByConditionAsync(
                Expression<Func<Session, bool>> predicate) {
            return await Entities.Where(predicate)
                                 .Include(film => film.Film)
                                 .Include(seats => seats.Seats)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
    }
}