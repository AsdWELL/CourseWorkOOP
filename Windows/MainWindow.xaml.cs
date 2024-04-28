using System.Windows;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ExhibitsWindow _exhibitsWindow;
        private static VisitorsWindow _visitorsWindow;

        public static MuseumContext _museumContext;

        public MainWindow()
        {
            InitializeComponent();
            _museumContext = new MuseumContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Годов Дмитрий 22ВП1\nТема: Музей");
            _exhibitsWindow = new ExhibitsWindow();
            _exhibitsWindow.Owner = this;
            _visitorsWindow = new VisitorsWindow();
            _visitorsWindow.Owner = this;

            _museumContext.Database.EnsureCreated();
        }

        private void ExhibitsBtn_Click(object sender, RoutedEventArgs e)
        {
            _exhibitsWindow.Show();
            Hide();
        }

        private void VisitorsBtn_Click(object sender, RoutedEventArgs e)
        {
            _visitorsWindow.Show();
            Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _museumContext.SaveChanges();
        }
    }
}