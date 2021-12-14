using DLL.Repository;

namespace BLL.Services;

public class SessionService {
    private readonly SessionRepository _repository;
    public SessionService(SessionRepository repository) { _repository = repository; }
    public async void AddSession() { }
}