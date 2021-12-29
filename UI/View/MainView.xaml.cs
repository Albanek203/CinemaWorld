using System.Windows;
using System.Windows.Input;
using UI.ViewModel;

namespace UI.View {
    public partial class MainView : Window {
        public MainView(MainViewModel viewModel) {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void UIMain_OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}