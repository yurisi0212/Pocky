using MahApps.Metro.Controls;
using Pocky.MVVM.ViewModel;

namespace Pocky.MVVM.View {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }
    }
}