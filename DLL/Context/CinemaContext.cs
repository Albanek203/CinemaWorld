using System;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Context {
    public class CinemaContext : DbContext {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User>        Users        { get; set; }
        public DbSet<LoginData>   LoginDatas   { get; set; }
        public DbSet<Person>      Persons      { get; set; }
        public DbSet<ActionsData> ActionsDatas { get; set; }
        public DbSet<Session>     Sessions     { get; set; }
        public DbSet<Film>        Films        { get; set; }
        public DbSet<Seat>        Seats        { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<LoginData>(LoginDataConfigure);
            modelBuilder.Entity<User>(UserConfigure);
            modelBuilder.Entity<Person>(PersonConfigure);
            modelBuilder.Entity<ActionsData>(ActionsDatasConfigure);
            modelBuilder.Entity<Session>(SessionConfigure);
            modelBuilder.Entity<Film>(FilmConfigure);
            modelBuilder.Entity<Seat>(SeatConfigure);
        }

        private void UserConfigure(EntityTypeBuilder<User> builder) {
            builder.Property(x => x.Role).IsRequired();

            builder.HasOne(x => x.Person)
                   .WithOne(x => x.User)
                   .HasForeignKey<User>(x => x.PersonId)
                   .IsRequired();
            builder.HasOne(x => x.LoginData)
                   .WithOne(x => x.User)
                   .HasForeignKey<User>(x => x.LoginDataId)
                   .IsRequired();
            builder.HasMany(x => x.ActionsData)
                   .WithOne(x => x.User);

            builder.HasData(new User {
                Id = 1,
                PersonId = 1,
                LoginDataId = 1,
                Role = 0,
            });
        }

        private void LoginDataConfigure(EntityTypeBuilder<LoginData> builder) {
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.HasOne(x => x.User)
                   .WithOne(x => x.LoginData)
                   .IsRequired();

            builder.HasData(new LoginData {
                Id = 1,
                Email = "albanekpiss@gmail.com",
                Login = "funnybored24",
                Password = "strongpassbymy24"
            });
        }

        private void PersonConfigure(EntityTypeBuilder<Person> builder) {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Salary).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.EmployeeDate).IsRequired();

            builder.HasData(new Person {
                Id = 1,
                Name = "Oleh",
                Surname = "Border",
                Salary = 4200,
                BirthDate = new DateTime(2002, 7, 24),
                EmployeeDate = new DateTime(2012, 4, 12)
            });
        }

        private void ActionsDatasConfigure(EntityTypeBuilder<ActionsData> builder) {
            builder.HasOne(x => x.User)
                   .WithMany(x => x.ActionsData)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();
            builder.Property(x => x.TypeOperation).IsRequired();
            builder.Property(x => x.ObjectDataType).IsRequired();
            builder.Property(x => x.Cost).IsRequired();
            builder.Property(x => x.Time).IsRequired();
        }

        private void SessionConfigure(EntityTypeBuilder<Session> builder) {
            builder.Property(x => x.HallNumber).IsRequired();
            builder.Property(x => x.DateSession).IsRequired();

            builder.HasOne(x => x.Film)
                   .WithOne(x => x.Session)
                   .HasForeignKey<Session>(x => x.FilmId)
                   .IsRequired();
            builder.HasMany(x => x.Seats)
                   .WithOne(x => x.Session)
                   .IsRequired();
        }

        private void FilmConfigure(EntityTypeBuilder<Film> builder) {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Genre).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Is3D).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.HasOne(x => x.Session)
                   .WithOne(x => x.Film)
                   .HasForeignKey<Film>(x => x.SessionId)
                   .IsRequired();
        }

        private void SeatConfigure(EntityTypeBuilder<Seat> builder) {
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.HasOne(x => x.Session)
                   .WithMany(x => x.Seats)
                   .IsRequired();
        }
    }
}