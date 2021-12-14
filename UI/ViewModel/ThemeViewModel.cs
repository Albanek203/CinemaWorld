using System;
using System.Collections.ObjectModel;
using System.Windows;
using UI.Enumeration;

namespace UI.ViewModel {
    public class ThemeViewModel : BaseViewModel{
        public ThemeViewModel() {
            ThemesList = new ObservableCollection<object>();
            foreach (var i in Enum.GetValues(typeof(ThemeType))) { ThemesList.Add(i); }
            Theme = ThemeType.Dark;
            ChangeTheme(Theme);
        }
        private ThemeType _theme;
        public ThemeType Theme {
            get => _theme;
            set {
                _theme = value;
                OnPropertyChanged(nameof(Theme));
            }
        }
        public ObservableCollection<object> ThemesList { get; }
        public void ChangeTheme(ThemeType theme) {
            _theme = theme;
            var name = theme.ToString();
            var uri  = new Uri(@"View\Themes\" + name + ".xaml", UriKind.Relative);

            var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}