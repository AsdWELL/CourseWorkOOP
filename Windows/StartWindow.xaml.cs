using System.Windows;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private Task _goToMainWindow;
        private bool _isStartBtnClicked = false;
        
        public StartWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _goToMainWindow = Task.Delay(5000);

            await _goToMainWindow;

            if (!_isStartBtnClicked)
                GoToMainWindow();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            _isStartBtnClicked = true;
            
            GoToMainWindow();
        }

        private void GoToMainWindow()
        {
            new MainWindow().Show();

            Close();
        }
    }
}
