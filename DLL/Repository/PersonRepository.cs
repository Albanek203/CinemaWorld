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
    public class PersonRepository : BaseRepository<Person> {
        public PersonRepository(CinemaContext context) : base(context) { }

        public override async Task<IReadOnlyCollection<Person>> GetAllAsync() {
            return await Entities.Include(user => user.User)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }

        public override async Task<IReadOnlyCollection<Person>> FindByConditionAsync(
            Expression<Func<Person, bool>> predicate) {
            return await Entities.Include(user => user.User)
                                 .Where(predicate)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
    }
}