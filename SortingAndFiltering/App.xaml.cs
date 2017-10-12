using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SortingAndFiltering
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow mainV;
        MainWindowViewModel mainVm;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            mainVm = new MainWindowViewModel();
            mainV = new MainWindow()
            {
                DataContext = mainVm
            };
            mainV.Show();
        }
    }
}
