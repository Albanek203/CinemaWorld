using System.Windows.Controls;
using UI.ViewModel;

namespace UI.View.Pages {
    public partial class HomeView : Page {
        public HomeView(HomeViewModel viewModel) {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}