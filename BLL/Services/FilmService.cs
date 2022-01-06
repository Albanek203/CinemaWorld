using DLL.Models;
using DLL.Repository;

namespace BLL.Services {
    public class FilmService {
        private readonly FilmRepository _filmRepository;
        public FilmService(FilmRepository filmRepository) { _filmRepository = filmRepository; }
        public async Task<IReadOnlyCollection<Film>> GetAllFilmsAsync() => await _filmRepository.GetAllAsync();
        public async Task<Film> GetFilmAsync(int index) =>
            (await _filmRepository.FindByConditionAsync(x => x.Id == index)).First();
    }
}