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
                "Data Source=GARGULYA;Initial Catalog=CinemaDbTest;Integrated Security=True;Connect Timeout=30";
        private static   CinemaContext         _context;
        private readonly UserRepository        _userRepository;
        private readonly SeatRepository        _seatRepository;
        private readonly ActionsDataRepository _actionsDataRepository;
        private readonly SessionRepository     _sessionRepository;
        private readonly LoginDataRepository   _loginDataRepository;
        public ServiceTest() {
            var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option =
                    optionBuilder.UseSqlServer(ConnectString).Options;
            _context = new CinemaContext(option);
            _userRepository = new UserRepository(_context);
            _seatRepository = new SeatRepository(_context);
            _actionsDataRepository = new ActionsDataRepository(_context);
            _sessionRepository = new SessionRepository(_context);
            _loginDataRepository = new LoginDataRepository(_context);
        }
        [Fact]
        public async void CheckTicketService() {
            var ticketService =
                    new TicketService((await _userRepository.FindByConditionAsync(x => x.Id == 1)).ToList().First(),
                                      _actionsDataRepository, _seatRepository);
            Assert.NotNull(ticketService);
            var session = (await _sessionRepository.FindByConditionAsync(x =>
                                                                                 x.HallNumber.Equals(321) &&
                                                                                 x.DateSession
                                                                                         .Equals(new DateTime(2021, 10
                                                                                           , 13, 15, 15
                                                                                           , 0))))
                          .ToList()
                          .First();
            Assert.NotNull(session);
            await ticketService.TicketManipulation(session, OperationType.Sell, 2);
        }
        [Fact]
        public async void CheckLoginService() {
            var loginService = new LoginService(_loginDataRepository);
            var user = await loginService.Login("albanekpiss@gmail.com", "strongpassbymy24");
            Assert.NotNull(user);
        }
    }
}