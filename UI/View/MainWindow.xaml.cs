using System.Windows;
using System.Windows.Input;
using DLL.Models;

namespace UI.View; 

public partial class MainWindow : Window {
    public MainWindow(User user) {
        InitializeComponent();
    }
    private void UIMain_OnMouseDown(object sender, MouseButtonEventArgs e) {  }
}