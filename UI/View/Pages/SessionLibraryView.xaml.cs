using UI.ViewModel;

namespace UI.View.Pages {
    public partial class SessionLibraryView {
        public SessionLibraryView(SessionLibraryViewModel viewModel) {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}