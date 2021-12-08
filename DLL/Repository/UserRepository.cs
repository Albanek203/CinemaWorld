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
    public class UserRepository : BaseRepository<User> {
        public UserRepository(CinemaContext context) : base(context) { }

        public override async Task<IReadOnlyCollection<User>> GetAllAsync() {
            return await Entities.Include(person => person.Person)
                                 .Include(loginData => loginData.LoginData)
                                 .Include(actionsDatas => actionsDatas.ActionsData)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }

        public override async Task<IReadOnlyCollection<User>> FindByConditionAsync(
            Expression<Func<User, bool>> predicate) {
            return await Entities.Where(predicate)
                                 .Include(person => person.Person)
                                 .Include(loginData => loginData.LoginData)
                                 .Include(actionsDatas => actionsDatas.ActionsData)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
    }
}