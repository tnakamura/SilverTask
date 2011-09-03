using System.Windows;
using System.Windows.Controls;
using SilverTask.ViewModels;

namespace SilverTask
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                var viewModel = new MainViewModel();
                DataContext = viewModel;
                viewModel.Tasks.LoadTasks();
            });
        }
    }
}
