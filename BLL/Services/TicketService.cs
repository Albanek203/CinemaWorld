using BLL.DTO.Enumerations;
using DLL.Models;
using DLL.Repository;

namespace BLL.Services {
    public class TicketService {
        private readonly ActionsDataService _actionsDataService;
        private readonly SeatRepository _seatRepository;
        private readonly User _user;
        public TicketService(User user, ActionsDataService actionsDataService, SeatRepository seatRepository) {
            _actionsDataService = actionsDataService;
            _seatRepository     = seatRepository;
            _user               = user;
        }
        public async Task<bool> TicketManipulationAsync(Session session, OperationType operationType, int numberSeat) {
            try {
                var seat = session.Seats!.First(x => x.Number.Equals(numberSeat));
                switch (operationType) {
                    case OperationType.Sell:
                        await _seatRepository.ChangeStatusSeatAsync(x => x.Equals(seat), (int)SeatStatusType.Taken);
                        await Task.Factory.StartNew(() => _actionsDataService.Insert(
                                                        _user.Id, OperationType.Sell, ObjectDataType.Ticket, seat.Price
                                                      , DateTime.Now));
                        break;
                    case OperationType.Reserved:
                        await _seatRepository.ChangeStatusSeatAsync(x => x.Equals(seat), (int)SeatStatusType.Reserved);
                        await Task.Factory.StartNew(() => _actionsDataService.Insert(
                                                        _user.Id, OperationType.Reserved, ObjectDataType.Ticket, 0
                                                      , DateTime.Now));
                        break;
                    case OperationType.Cancel:
                        await _seatRepository.ChangeStatusSeatAsync(x => x.Equals(seat), (int)SeatStatusType.NotTaken);
                        await Task.Factory.StartNew(() => _actionsDataService.Insert(
                                                        _user.Id, OperationType.Cancel, ObjectDataType.Ticket
                                                      , seat.Price, DateTime.Now));
                        break;
                    default:
                        break;
                }
            } catch (Exception e) { return false; }
            return true;
        }
    }
}