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
    public class ActionsDataRepository : BaseRepository<ActionsData> {
        public ActionsDataRepository(CinemaContext context) : base(context) { }

        public override async Task<IReadOnlyCollection<ActionsData>> GetAllAsync() {
            return await Entities.Include(user => user.User)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }

        public override async Task<IReadOnlyCollection<ActionsData>> FindByConditionAsync(
            Expression<Func<ActionsData, bool>> predicate) {
            return await Entities.Where(predicate)
                                 .Include(user => user.User)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
    }
}