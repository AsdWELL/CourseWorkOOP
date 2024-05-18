using System.Windows;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExhibitsWindow _exhibitsWindow;
        private VisitorsWindow _visitorsWindow;

        public static MuseumContext _museumContext;

        public MainWindow()
        {
            InitializeComponent();
            _museumContext = new MuseumContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _museumContext.Database.EnsureCreated();
            _exhibitsWindow = new ExhibitsWindow();
            _exhibitsWindow.Owner = this;
            _visitorsWindow = new VisitorsWindow();
            _visitorsWindow.Owner = this;
        }

        private void ExhibitsBtn_Click(object sender, RoutedEventArgs e)
        {
            _exhibitsWindow.Show();
        }

        private void VisitorsBtn_Click(object sender, RoutedEventArgs e)
        {
            _visitorsWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _museumContext.SaveChanges();
        }
    }
}