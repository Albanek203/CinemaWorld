using BLL.Services;
using DLL.Context;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Infrastructure {
    public static class BllConfigureService {
        public static void ConfigureService(IServiceCollection services, string connectionStr) {
            // DbContextOptionsBuilder
            services.AddTransient(x => {
                var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
                var option =
                        optionBuilder.UseSqlServer(connectionStr).Options;
                return option;
            });

            // Context
            services.AddTransient<CinemaContext>();

            // Repositories
            services.AddTransient<ActionsDataRepository>();
            services.AddTransient<FilmRepository>();
            services.AddTransient<LoginDataRepository>();
            services.AddTransient<PersonRepository>();
            services.AddTransient<SeatRepository>();
            services.AddTransient<SessionRepository>();
            services.AddTransient<UserRepository>();

            // Services
            services.AddTransient<LoginService>();
            services.AddTransient<TicketService>();
            services.AddTransient<SessionService>();
            services.AddTransient<FilmService>();
        }
    }
}