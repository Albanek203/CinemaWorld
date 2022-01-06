using BLL.DTO.Enumerations;
using DLL.Models;
using DLL.Repository;

namespace BLL.Services {
    public class ActionsDataService {
        private readonly ActionsDataRepository _actionsDataRepository;
        public ActionsDataService(ActionsDataRepository actionsDataRepository) {
            _actionsDataRepository = actionsDataRepository;
        }
        public async Task Insert(int            userId
                               , OperationType  operationType
                               , ObjectDataType objectDataType
                               , float          cost
                               , DateTime       time) {
            await _actionsDataRepository.InsertAsync(new ActionsData {
                UserId         = userId
              , TypeOperation  = (int)operationType
              , ObjectDataType = (int)objectDataType
              , Cost           = cost
              , Time           = time
            });
        }
    }
}