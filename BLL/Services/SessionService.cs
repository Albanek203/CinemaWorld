using BLL.DTO.Enumerations;
using DLL.Models;
using DLL.Repository;

namespace BLL.Services {
    public class SessionService {
        private readonly SessionRepository _sessionRepository;
        private readonly SeatRepository _seatRepository;
        private readonly FilmRepository _filmRepository;
        public SessionService(SessionRepository sessionRepository
                            , SeatRepository    seatRepository
                            , FilmRepository    filmRepository) {
            _sessionRepository = sessionRepository;
            _seatRepository    = seatRepository;
            _filmRepository    = filmRepository;
        }
        public async Task<bool> AddSessionAsync(int hallNumber, string filmName, DateTime dateTime) {
            if ((await _sessionRepository.FindByConditionAsync(x => x.HallNumber.Equals(hallNumber) &&
                                                                    x.DateSession.Equals(dateTime))).Count > 0) {
                return false;
            }
            var film    = (await _filmRepository.FindByConditionAsync(x => x.Name == filmName)).First();
            var session = new Session { HallNumber = hallNumber, Film = film, DateSession = dateTime };
            await _sessionRepository.InsertAsync(session);
            var firstRow  = film.Price * 25 / 100;
            var secondRow = film.Price * 30 / 100;
            var thirdRow  = film.Price * 45 / 100;
            for (var i = 1; i <= 66; i++) {
                switch (i) {
                    case <= 22:
                        await _seatRepository.InsertAsync(new Seat {
                            Number = i, Price = firstRow, Status = (int)SeatStatusType.NotTaken, Session = session
                        });
                        break;
                    case <= 44:
                        await _seatRepository.InsertAsync(new Seat {
                            Number = i, Price = secondRow, Status = (int)SeatStatusType.NotTaken, Session = session
                        });
                        break;
                    case <= 66:
                        await _seatRepository.InsertAsync(new Seat {
                            Number = i, Price = thirdRow, Status = (int)SeatStatusType.NotTaken, Session = session
                        });
                        break;
                }
            }
            return true;
        }
        public async Task<IReadOnlyCollection<Session>> GetAllSessionAsync() => await _sessionRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Session>> GetAllSessionAsync(int filmId) =>
            await _sessionRepository.FindByConditionAsync(x => x.Film!.Id == filmId);
        public async Task<Session> GetSessionAsync(int index) =>
            (await _sessionRepository.FindByConditionAsync(x => x.Id == index)).First();
    }
}