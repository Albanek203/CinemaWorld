using System;
using System.Linq;
using DLL.Context;
using DLL.Models;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DLLTest {
    public class ContextTest {
        private const string ConnectString =
            @"Data Source=localhost;Initial Catalog=CinemaDbTest;Integrated Security=True;Connect Timeout=30";
        private readonly CinemaContext _context;
        public ContextTest() {
            var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option        = optionBuilder.UseSqlServer(ConnectString).Options;
            _context = new CinemaContext(option);
        }
        [Fact]
        public void CreateDbTest() {
            var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option        = optionBuilder.UseSqlServer(ConnectString).Options;
            try {
                var context = new CinemaContext(option);
                Assert.NotNull(context);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }

#region Add datas test
        [Fact]
        public void CheckActionsDataAdd() {
            // Prepare data
            try {
                _context.ActionsDatas.Add(new ActionsData {
                    User = new User {
                        Person = new Person {
                            Name         = "TestActionData"
                          , Surname      = "TestActionData"
                          , Salary       = 10
                          , BirthDate    = DateTime.Now
                          , EmployeeDate = DateTime.Now
                        }
                      , LoginData = new LoginData {
                            Email    = "TestActionData@gmail.com"
                          , Login    = "TestActionData"
                          , Password = "TestActionData"
                        }
                      , Role = 1
                    }
                  , TypeOperation  = 0
                  , ObjectDataType = 1
                  , Cost           = 1000
                  , Time           = DateTime.Now
                });
                _context.SaveChangesAsync();
            } catch (Exception e) { Assert.False(false, e.Message); }
            var obj = _context.ActionsDatas.Where(x => x.User!.Person!.Name == "TestActionData");
            Assert.NotNull(obj);
        }
        [Fact]
        public void CheckFilmAdd() {
            try {
                _context.Films.Add(new Film {
                    Name  = "Spider-Man: There is no way home"
                  , Genre = 3
                  , Duration =
                        "American superhero film based on Marvel Comics about the character of the same name"
                  , Is3D    = true
                  , Rating  = 4f
                  , Price   = 1000
                });
                _context.SaveChangesAsync();
            } catch (Exception e) { Assert.False(false, e.Message); }
            var obj = _context.Films.Where(x => x.Name == "Spider-Man: There is no way home");
            Assert.NotNull(obj);
        }
        [Fact]
        public void CheckLoginDataAdd() {
            try {
                _context.LoginDatas.Add(new LoginData {
                    Email = "TestLoginData@gmail.com", Login = "TestLoginData", Password = "TestLoginData"
                });
                _context.SaveChanges();
            } catch (Exception e) { Assert.False(false, e.Message); }
            var obj = _context.LoginDatas.Where(x => x.Login == "TestLoginData");
            Assert.NotNull(obj);
        }
        [Fact]
        public void CheckPersonAdd() {
            try {
                _context.Persons.Add(new Person {
                    Name         = "TestPerson"
                  , Surname      = "TestPerson"
                  , Salary       = 234
                  , BirthDate    = DateTime.Now
                  , EmployeeDate = DateTime.Now
                });
                _context.SaveChanges();
            } catch (Exception e) { Assert.False(false, e.Message); }
            var obj = _context.Persons.Where(x => x.Name == "TestPerson");
            Assert.NotNull(obj);
        }
        [Fact]
        public void CheckSeatAdd() {
            try {
                _context.Seats.Add(new Seat { Number = 2, Price = 100, Status = 2, Session = new Session() });
                _context.SaveChanges();
            } catch (Exception e) { Assert.False(false, e.Message); }
            var obj = _context.Seats.Where(x => x.Number == 2);
            Assert.NotNull(obj);
        }
        [Fact]
        public void CheckSessionAdd() {
            try {
                _context.Sessions.Add(new Session {
                    HallNumber = 321, DateSession = new DateTime(2021, 10, 13, 15, 15, 0)
                });
                _context.SaveChanges();
            } catch (Exception e) { Assert.False(false, e.Message); }
            var obj = _context.Sessions.Where(x => x.HallNumber == 321);
            Assert.NotNull(obj);
        }
        [Fact]
        public void CheckUserAdd() {
            try {
                _context.Users.Add(new User { Role = 2 });
                _context.SaveChanges();
            } catch (Exception e) { Assert.False(false, e.Message); }
            var obj = _context.Users.Where(x => x.Role == 2);
            Assert.NotNull(obj);
        }
#endregion

#region Repository test
        [Fact]
        public void CheckActionsDataRepository() {
            try {
                var repository = new ActionsDataRepository(_context);
                Assert.NotNull(repository);
                var lstAll = repository.GetAllAsync();
                Assert.NotNull(lstAll);
                var lstPredicate = repository.FindByConditionAsync(x => x.Id == 1);
                Assert.NotNull(lstPredicate);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }
        [Fact]
        public void CheckFilmRepository() {
            try {
                var repository = new FilmRepository(_context);
                Assert.NotNull(repository);
                var lstAll = repository.GetAllAsync();
                Assert.NotNull(lstAll);
                var lstPredicate = repository.FindByConditionAsync(x => x.Id == 1);
                Assert.NotNull(lstPredicate);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }
        [Fact]
        public void CheckLoginDataRepository() {
            try {
                var repository = new LoginDataRepository(_context);
                Assert.NotNull(repository);
                var lstAll = repository.GetAllAsync();
                Assert.NotNull(lstAll);
                var lstPredicate = repository.FindByConditionAsync(x => x.Id == 1);
                Assert.NotNull(lstPredicate);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }
        [Fact]
        public void CheckPersonRepository() {
            try {
                var repository = new PersonRepository(_context);
                Assert.NotNull(repository);
                var lstAll = repository.GetAllAsync();
                Assert.NotNull(lstAll);
                var lstPredicate = repository.FindByConditionAsync(x => x.Id == 1);
                Assert.NotNull(lstPredicate);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }
        [Fact]
        public void CheckSeatRepository() {
            try {
                var repository = new SeatRepository(_context);
                Assert.NotNull(repository);
                var lstAll = repository.GetAllAsync();
                Assert.NotNull(lstAll);
                var lstPredicate = repository.FindByConditionAsync(x => x.Id == 1);
                Assert.NotNull(lstPredicate);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }
        [Fact]
        public void CheckSessionRepository() {
            try {
                var repository = new SessionRepository(_context);
                Assert.NotNull(repository);
                var lstAll = repository.GetAllAsync();
                Assert.NotNull(lstAll);
                var lstPredicate = repository.FindByConditionAsync(x => x.Id == 1);
                Assert.NotNull(lstPredicate);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }
        [Fact]
        public void CheckUserRepository() {
            try {
                var repository = new UserRepository(_context);
                Assert.NotNull(repository);
                var lstAll = repository.GetAllAsync();
                Assert.NotNull(lstAll);
                var lstPredicate = repository.FindByConditionAsync(x => x.Id == 1);
                Assert.NotNull(lstPredicate);
            } catch (Exception e) { Assert.False(false, e.Message); }
        }
#endregion
    }
}