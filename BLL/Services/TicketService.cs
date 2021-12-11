using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO.Enumerations;
using DLL.Models;
using DLL.Repository;

namespace BLL.Services {
    public class TicketService {
        private readonly ActionsDataRepository _actionsDataRepository;
        private readonly SeatRepository        _seatRepository;
        private readonly User                  _user;
        public TicketService(User user, ActionsDataRepository actionsDataRepository, SeatRepository seatRepository) {
            _actionsDataRepository = actionsDataRepository;
            _seatRepository = seatRepository;
            _user = user;
        }
        public async Task<bool> TicketManipulation(Session session, OperationType operationType
                                                 , int numberSeat) {
            try {
                var seat = session.Seats!.Where(x => x.Number.Equals(numberSeat)).ToList().First();
                switch (operationType) {
                    case OperationType.Sell:
                        await _seatRepository.ChangeStatusSeatAsync(x => x.Equals(seat)
                                                                  , (int) SeatStatusType.Taken);
                        await _actionsDataRepository.InsertAsync(new ActionsData {
                                User = _user
                              , TypeOperation = (int) OperationType.Sell
                              , ObjectDataType = (int) ObjectDataType.Ticket
                              , Cost = seat.Price
                              , Time = DateTime.Now
                        });
                        break;
                    case OperationType.Reserved:
                        await _seatRepository.ChangeStatusSeatAsync(x => x.Equals(seat)
                                                                  , (int) SeatStatusType.Reserved);
                        await _actionsDataRepository.InsertAsync(new ActionsData {
                                User = _user
                              , TypeOperation = (int) OperationType.Reserved
                              , ObjectDataType = (int) ObjectDataType.Ticket
                              , Cost = 0
                              , Time = DateTime.Now
                        });
                        break;
                    case OperationType.Cancel:
                        await _seatRepository.ChangeStatusSeatAsync(x => x.Equals(seat)
                                                                  , (int) SeatStatusType.NotTaken);
                        await _actionsDataRepository.InsertAsync(new ActionsData {
                                User = _user
                              , TypeOperation = (int) OperationType.Cancel
                              , ObjectDataType = (int) ObjectDataType.Ticket
                              , Cost = seat.Price
                              , Time = DateTime.Now
                        });
                        break;
                    default: break;
                }
            }
            catch (Exception e) { return false; }
            return true;
        }
    }
}