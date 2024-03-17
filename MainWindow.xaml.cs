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
        private static List<Visitor> _visitors;
        private static List<Exhibit> _exhibits;
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}