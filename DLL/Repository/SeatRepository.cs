using System.Linq.Expressions;
using DLL.Context;
using DLL.Interfaces;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository {
    public class SeatRepository : BaseRepository<Seat> {
        public SeatRepository(CinemaContext context) : base(context) { }
        public override async Task<IReadOnlyCollection<Seat>> GetAllAsync() {
            return await Entities.Include(session => session.Session)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
        public override async Task<IReadOnlyCollection<Seat>> FindByConditionAsync(
                Expression<Func<Seat, bool>> predicate) {
            return await Entities.Where(predicate)
                                 .Include(session => session.Session)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
        public async Task ChangeStatusSeatAsync(Expression<Func<Seat, bool>> predicate, int status) {
            var model = await Entities.Where(predicate).FirstAsync();
            model.Status = status;
            Entities.Update(model);
            await CinemaContext.SaveChangesAsync();
        }
    }
}