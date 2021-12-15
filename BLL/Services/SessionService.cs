using BLL.DTO.Enumerations;
using DLL.Models;
using DLL.Repository;

namespace BLL.Services {
    public class SessionService {
        private readonly SessionRepository _sessionRepository;
        private readonly SeatRepository    _seatRepository;
        private readonly FilmRepository    _filmRepository;
        public SessionService(SessionRepository sessionRepository, SeatRepository seatRepository
                            , FilmRepository filmRepository) {
            _sessionRepository = sessionRepository;
            _seatRepository = seatRepository;
            _filmRepository = filmRepository;
        }
        public async Task<bool> AddSession(int hallNumber, string filmName, DateTime dateTime) {
            if ((await _sessionRepository.FindByConditionAsync(x => x.HallNumber.Equals(hallNumber) &&
                                                                    x.DateSession.Equals(dateTime))).Count
              > 0) { return false; }
            var film = (await _filmRepository.FindByConditionAsync(x => x.Name == filmName)).First();
            var session = new Session {HallNumber = hallNumber, Film = film, DateSession = dateTime};
            await _sessionRepository.InsertAsync(session);
            var startPrice = film.Price / 36;
            for (var i = 1; i <= 36; i++) {
                await _seatRepository.InsertAsync(new Seat {
                        Number = i, Price = startPrice, Status = (int) SeatStatusType.NotTaken, Session = session
                });
            }
            return true;
        }
    }
}