using System;
using System.Linq;
using DLL.Context;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DLLTest {
    public class ContextTest
    {
        private readonly CinemaContext _context;

        public ContextTest() {
            var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option =
                optionBuilder.UseSqlServer(
                    @"Data Source=GARGULYA;Initial Catalog=CinemaDbTest;Integrated Security=True;Connect Timeout=30").Options;
            _context = new CinemaContext(option);
        }

        [Fact]
        public void CreateDb() {
            var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option =
                optionBuilder.UseSqlServer(
                    @"Data Source=GARGULYA;Initial Catalog=CinemaDbTest;Integrated Security=True;Connect Timeout=30").Options;
            try {
                var context = new CinemaContext(option);
                Assert.NotNull(context);
            } catch (Exception e) { Assert.False(false,e.Message); }
        }
        [Fact]
        public void AddFilmTest() {
            // Prepare data
            _context.Persons.Add(new Person {
                Name = "test",
                Surname = "test",
                Salary = 1234,
                BirthDate = DateTime.Now,
                EmployeeDate = DateTime.Now
            });
            _context.SaveChanges();

            var film = _context.Films.Where(x=>x.Name == "test");
            Assert.NotNull(film);
        }
    }
}