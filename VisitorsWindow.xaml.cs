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
            _visitors.OnChange += (i, v) => VisitorsDataGrid.Items[i] = v;

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

        private void VisitorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isEnabled = VisitorsDataGrid.SelectedItems.Count > 0;

            EditVisitorBtn.IsEnabled = isEnabled;
            DeleteSelectedBtn.IsEnabled = isEnabled;
        }

        private void AddNewVisitorBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateVisitorWindow visitorWindow = new CreateVisitorWindow();
            bool? result = visitorWindow.ShowDialog();
            if (result == true)
                _visitors.Add(visitorWindow.NewVisitor);
        }

        private void EditVisitorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (VisitorsDataGrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Выберите одного посетителя для редактирования", "Внимание");
                return;
            }

            int selectedIndex = VisitorsDataGrid.SelectedIndex;
            CreateVisitorWindow visitorWindow = new CreateVisitorWindow();
            bool? result = visitorWindow.ShowDialog(_visitors[selectedIndex]);

            try
            {
                if (result == true)
                    _visitors[selectedIndex] = visitorWindow.NewVisitor;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Внимание");
            }
        }

        private void DeleteSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                while (VisitorsDataGrid.SelectedItems.Count > 0)
                    _visitors.RemoveAt(VisitorsDataGrid.SelectedIndex);
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Внимание");
            }
        }

        private void UnselectAll_Click(object sender, RoutedEventArgs e)
        {
            VisitorsDataGrid.UnselectAll();
        }

        private void DeleteAllVisitors_Click(object sender, RoutedEventArgs e)
        {
            _visitors.Clear();
            VisitorsDataGrid.Items.Clear();
        }
    }
}
