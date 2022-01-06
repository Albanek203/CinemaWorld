using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using BLL.DTO.Enumerations;
using BLL.Services;
using DLL.Models;
using UI.Infrastructure;

namespace UI.ViewModel {
    public class SessionLibraryViewModel : BaseViewModel {
        private readonly FilmService _filmService;
        private readonly SessionService _sessionService;
        private readonly TicketService _ticketService;
        public SessionLibraryViewModel(FilmService    filmService
                                     , SessionService sessionService
                                     , TicketService  ticketService) {
            _filmService    = filmService;
            _sessionService = sessionService;
            _ticketService  = ticketService;
            Task.Factory.StartNew(async () => {
                _lstAllFilms     = (IList<Film>)await filmService.GetAllFilmsAsync();
                ListFilms        = _lstAllFilms;
                LstFilmSortIndex = 0;
            });
            FilmSortBy           = new List<string> { Properties.Resources.TName, Properties.Resources.TRating };
            SeatsPanelVisibility = Visibility.Collapsed;
            SeatInfoVisibility   = Visibility.Collapsed;
        }

#region Library panel
        private string _filmName;
        public string FilmName {
            get => _filmName;
            set {
                _filmName = value;
                OnPropertyChanged(nameof(FilmName));
            }
        }
        private Film _currentFilm;
        private IList<Film> _lstAllFilms;
        private IList<Film> _lstFilms;
        public IList<Film> ListFilms {
            get => _lstFilms;
            set {
                _lstFilms = value;
                OnPropertyChanged(nameof(ListFilms));
            }
        }
        private IList<Session> _lstSessions;
        public IList<Session> ListSessions {
            get => _lstSessions;
            set {
                _lstSessions = value;
                OnPropertyChanged(nameof(ListSessions));
            }
        }
        private string _searchFilm;
        public string SearchFilm {
            get => _searchFilm;
            set {
                _searchFilm = value;
                ListFilms = new ObservableCollection<Film>(string.IsNullOrWhiteSpace(value)
                                                               ? _lstAllFilms
                                                               : _lstAllFilms
                                                                   .Where(x => x.Name!.ToLower().Contains(value))
                                                                   .ToList());
                OnPropertyChanged(nameof(SearchFilm));
            }
        }
        private IList<string> _filmSortBy;
        public IList<string> FilmSortBy {
            get => _filmSortBy;
            set {
                _filmSortBy = value;
                OnPropertyChanged(nameof(FilmSortBy));
            }
        }
        private int _lstFilmsSortIndex;
        public int LstFilmSortIndex {
            get => _lstFilmsSortIndex;
            set {
                _lstFilmsSortIndex = value;
                ListFilms = new ObservableCollection<Film>(_lstFilmsSortIndex switch {
                    0 => ListFilms.OrderBy(x => x.Name).ToList()
                  , 1 => ListFilms.OrderBy(x => x.Rating).ToList()
                  , _ => ListFilms
                });
                OnPropertyChanged(nameof(LstFilmSortIndex));
            }
        }
        private RelayCommand _showSessions;
        public ICommand ShowSessions => _showSessions ??= new RelayCommand(ExecuteShowSessions, CanExecuteShowSessions);
        private async void ExecuteShowSessions(object obj) {
            var film = await _filmService.GetFilmAsync((int)obj);
            _currentFilm = film;
            FilmName     = film.Name!;
            ListSessions = film.Sessions!;
        }
        private bool CanExecuteShowSessions(object obj) => true;
#endregion

#region Seats panel
        private Session _currentSession;
        public Session CurrentSession {
            get => _currentSession;
            set {
                _currentSession = value;
                OnPropertyChanged(nameof(CurrentSession));
            }
        }
        private Seat _selectedSeat;
        public Seat SelectedSeat {
            get => _selectedSeat;
            set {
                _selectedSeat = value;
                OnPropertyChanged(nameof(SelectedSeat));
            }
        }
        private SolidColorBrush _selectedTicketFill;
        public SolidColorBrush SelectedTicketFill {
            get => _selectedTicketFill;
            set {
                _selectedTicketFill = value;
                OnPropertyChanged(nameof(SelectedTicketFill));
            }
        }
        private Visibility _libraryVisibility;
        public Visibility LibraryVisibility {
            get => _libraryVisibility;
            set {
                _libraryVisibility = value;
                OnPropertyChanged(nameof(LibraryVisibility));
            }
        }
        private Visibility _seatsPanelVisibility;
        public Visibility SeatsPanelVisibility {
            get => _seatsPanelVisibility;
            set {
                _seatsPanelVisibility = value;
                OnPropertyChanged(nameof(SeatsPanelVisibility));
            }
        }
        private Visibility _seatInfoVisibility;
        public Visibility SeatInfoVisibility {
            get => _seatInfoVisibility;
            set {
                _seatInfoVisibility = value;
                OnPropertyChanged(nameof(SeatInfoVisibility));
            }
        }
        private BindingExpression _seatButtonBinding;
        private RelayCommand _buyTicket;
        public ICommand BuyTicket => _buyTicket ??= new RelayCommand(ExecuteBuyTicket, CanExecuteBuyTicket);
        private async void ExecuteBuyTicket(object obj) {
            CurrentSession = (await _sessionService.GetAllSessionAsync(_currentFilm.Id)).ToList()
                .First(x => x.Id == (int)obj);
            LibraryVisibility    = Visibility.Collapsed;
            SeatsPanelVisibility = Visibility.Visible;
        }
        private bool CanExecuteBuyTicket(object obj) => true;
        private RelayCommand _selectTicket;
        public ICommand SelectTicket => _selectTicket ??= new RelayCommand(ExecuteSelectTicket, CanExecuteSelectTicket);
        private void ExecuteSelectTicket(object obj) {
            var seatButton = (Button)obj;
            var index      = (int)seatButton.Content;
            SelectedSeat       = _currentSession.Seats!.First(x => x.Id == index);
            SeatInfoVisibility = Visibility.Visible;
            switch (SelectedSeat.Status) {
                case (int)SeatStatusType.NotTaken:
                    SelectedTicketFill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;
                case (int)SeatStatusType.Taken:
                    SelectedTicketFill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;
                case (int)SeatStatusType.Reserved:
                    SelectedTicketFill = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    break;
            }
            _seatButtonBinding = seatButton.GetBindingExpression(Control.BackgroundProperty)!;
        }
        private bool CanExecuteSelectTicket(object obj) => true;
        private RelayCommand _ticketManipulation;
        public ICommand TicketManipulation =>
            _ticketManipulation ??= new RelayCommand(ExecuteTicketManipulation, CanExecuteTicketManipulation);
        private async void ExecuteTicketManipulation(object obj) {
            SeatInfoVisibility = Visibility.Collapsed;
            var success =
                await _ticketService.TicketManipulationAsync(CurrentSession, Enum.Parse<OperationType>((string)obj)
                                                           , SelectedSeat.Number);
            CurrentSession.Seats!.First(x => x.Id == SelectedSeat.Number).Status =
                (int)Enum.Parse<OperationType>((string)obj);
            _seatButtonBinding.UpdateTarget();
        }
        private bool CanExecuteTicketManipulation(object obj) {
            if (SelectedSeat == null) return false;
            var operationType = Enum.Parse<OperationType>((string)obj);
            if (operationType == OperationType.Cancel) return true;
            return SelectedSeat.Status != (int)OperationType.Sell && SelectedSeat.Status != (int)OperationType.Reserved;
        }
        private RelayCommand _return;
        public ICommand Return => _return ??= new RelayCommand(ExecuteReturn, CanExecuteReturn);
        private void ExecuteReturn(object obj) {
            LibraryVisibility    = Visibility.Visible;
            SeatsPanelVisibility = Visibility.Collapsed;
            CurrentSession       = null!;
        }
        private bool CanExecuteReturn(object obj) => true;
#endregion
    }
}