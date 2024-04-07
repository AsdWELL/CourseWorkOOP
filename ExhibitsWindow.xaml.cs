using Microsoft.EntityFrameworkCore;
using System.Windows;
using static CourseWork.MainWindow;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для ExhibitsWindow.xaml
    /// </summary>
    public partial class ExhibitsWindow : Window
    {        
        public ExhibitsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _museumContext.Exhibits.Load();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Owner.Show();
            }
            catch (InvalidOperationException)
            {
                return;
            }

            e.Cancel = true;
            Hide();
        }
    }
}
