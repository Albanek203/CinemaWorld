using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
    }
}