using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для VisitorsWindow.xaml
    /// </summary>
    public partial class VisitorsWindow : Window
    {
        private static VisitorList _visitors;
        
        public VisitorsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _visitors = [];
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            if (_visitors.Count == 0)
                return;
            for (int i = 0; i < _visitors.Count; i++)
                VisitorsDataGrid.Items.Add(new 
                {
                    Number = i + 1,
                    _visitors[i].Name,
                    _visitors[i].Surname,
                    VisitDate = _visitors[i].VisitDate.ToString("d") 
                });
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
            _visitors.SaveToJson();
            e.Cancel = true;
            Hide();
        }
    }
}
