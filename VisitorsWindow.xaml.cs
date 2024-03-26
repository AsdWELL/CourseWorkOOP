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
            _visitors.OnAdd += v => AddNewVisitorToDataGrid(v, _visitors.Count);

            FillDataGrid(_visitors);
        }

        private void AddNewVisitorToDataGrid(Visitor visitor, int index)
        {
            VisitorsDataGrid.Items.Add(new
            {
                Number = index,
                visitor.Name,
                visitor.Surname,
                VisitDate = visitor.VisitDate.ToString("d")
            });
        }

        private void FillDataGrid(VisitorList visitors)
        {
            if (visitors.Count == 0)
                return;
            for (int i = 0; i < visitors.Count; i++)
                AddNewVisitorToDataGrid(_visitors[i], i + 1);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateVisitorWindow visitorWindow = new CreateVisitorWindow();
            bool? result = visitorWindow.ShowDialog();
            if (result == true)
                _visitors.Add(visitorWindow.NewVisitor);
        }
    }
}
