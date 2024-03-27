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
            _visitors.OnAdd += v => VisitorsDataGrid.Items.Add(v);
            _visitors.OnRemove += VisitorsDataGrid.Items.RemoveAt;

            FillDataGrid(_visitors);
        }

        private void FillDataGrid(VisitorList visitors)
        {
            for (int i = 0; i < visitors.Count; i++)
                VisitorsDataGrid.Items.Add(_visitors[i]);
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

        private void AddNewVisitorBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateVisitorWindow visitorWindow = new CreateVisitorWindow();
            bool? result = visitorWindow.ShowDialog();
            if (result == true)
                _visitors.Add(visitorWindow.NewVisitor);
        }

        private void DeleteSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            while (VisitorsDataGrid.SelectedItems.Count > 0)
                _visitors.RemoveAt(VisitorsDataGrid.SelectedIndex);
        }

        private void UnselectAll(object sender, RoutedEventArgs e)
        {
            VisitorsDataGrid.UnselectAll();
        }
    }
}
