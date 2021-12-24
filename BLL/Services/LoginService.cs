using DLL.Models;
using DLL.Repository;

namespace BLL.Services {
    public class LoginService {
        private readonly LoginDataRepository _repository;
        public LoginService(LoginDataRepository repository) { _repository = repository; }
        public async Task<User?> Login(string login, string? password) {
            List<LoginData> data;
            try {
                data = (await _repository.FindByConditionAsync(x => login.Contains('@') == true
                                                                       ? x.Email == login &&
                                                                         x.Password == password
                                                                       : x.Login == login &&
                                                                         x.Password == password)).ToList();
            }
            catch (Exception e) { return null; }
            return data.Count == 0 ? null : data.First().User;
        }
    }
}