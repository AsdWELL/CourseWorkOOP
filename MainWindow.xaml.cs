using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ExhibitsWindow _exhibitsWindow;
        private static VisitorsWindow _visitorsWindow;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Годов Дмитрий 22ВП1\nТема: Музей");
            _exhibitsWindow = new ExhibitsWindow();
            _exhibitsWindow.Owner = this;
            _visitorsWindow = new VisitorsWindow();
            _visitorsWindow.Owner = this;
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
    }
}