using System.Windows;
using System.Windows.Input;
using UI.ViewModel;

namespace UI.View {
    public partial class LoginView : Window {
        public LoginView(LoginViewModel viewModel) {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void UILogin_OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}