using System;
using System.Linq;
using BLL.DTO.Enumerations;
using BLL.Services;
using DLL.Context;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BLLTest {
    public class ServiceTest {
        private const string ConnectString =
            @"Data Source=localhost;Initial Catalog=CinemaDbTest;Integrated Security=True;Connect Timeout=30";
        private static CinemaContext _context;
        private readonly UserRepository _userRepository;
        private readonly SeatRepository _seatRepository;
        private readonly ActionsDataRepository _actionsDataRepository;
        private readonly SessionRepository _sessionRepository;
        private readonly LoginDataRepository _loginDataRepository;
        private readonly FilmRepository _filmRepository;
        public ServiceTest() {
            var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option        = optionBuilder.UseSqlServer(ConnectString).Options;
            _context               = new CinemaContext(option);
            _userRepository        = new UserRepository(_context);
            _seatRepository        = new SeatRepository(_context);
            _actionsDataRepository = new ActionsDataRepository(_context);
            _sessionRepository     = new SessionRepository(_context);
            _loginDataRepository   = new LoginDataRepository(_context);
            _filmRepository        = new FilmRepository(_context);
        }
        [Fact]
        public async void CheckTicketService() {
            var ticketService =
                new TicketService((await _userRepository.FindByConditionAsync(x => x.Id == 1)).ToList().First()
                                , new ActionsDataService(_actionsDataRepository), _seatRepository);
            Assert.NotNull(ticketService);
            var session =
                (await _sessionRepository.FindByConditionAsync(
                     x => x.HallNumber.Equals(321) && x.DateSession.Equals(new DateTime(2021, 10, 13, 15, 15, 0))))
                .ToList().First();
            Assert.NotNull(session);
            await ticketService.TicketManipulationAsync(session, OperationType.Sell, 2);
        }
        [Fact]
        public async void CheckLoginService() {
            var loginService = new LoginService(_loginDataRepository);
            var user         = await loginService.LoginAsync("albanekpiss@gmail.com", "strongpassbymy24");
            Assert.NotNull(user);
        }
        [Fact]
        public async void CheckSessionService() {
            var sessionService = new SessionService(_sessionRepository, _seatRepository, _filmRepository);
            var success =
                await sessionService.AddSessionAsync(322, "Spider-Man: There is no way home"
                                                   , new DateTime(2021, 12, 18, 22, 0, 0));
            Assert.True(success);
        }
    }
}