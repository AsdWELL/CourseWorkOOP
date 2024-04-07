using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using static CourseWork.MainWindow;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для VisitorsWindow.xaml
    /// </summary>
    public partial class VisitorsWindow : Window
    {        
        public VisitorsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _museumContext.Visitors.Load();
            VisitorsDataGrid.ItemsSource = _museumContext.Visitors.Local.ToObservableCollection();
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
            {
                _museumContext.Visitors.Add(visitorWindow.NewVisitor);
                _museumContext.SaveChanges();
            }                
        }

        private void EditVisitorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (VisitorsDataGrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Выберите одного посетителя для редактирования", "Внимание");
                return;
            }

            Visitor selectedVisitor;
            try
            {
                selectedVisitor = _museumContext.Visitors.ElementAt(VisitorsDataGrid.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при выборе посетителя", "Внимание");
                return;
            }

            CreateVisitorWindow visitorWindow = new CreateVisitorWindow();
            bool? result = visitorWindow.ShowDialog(selectedVisitor);

            if (result == true)
            {
                Visitor visitor = _museumContext.Visitors.Find(selectedVisitor.Id);

                visitor.Name = visitorWindow.NewVisitor.Name;
                visitor.Surname = visitorWindow.NewVisitor.Surname;
                visitor.VisitDate = visitorWindow.NewVisitor.VisitDate;

                _museumContext.SaveChanges();
                VisitorsDataGrid.Items.Refresh();
            }
        }

        private void DeleteSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                while (VisitorsDataGrid.SelectedItems.Count > 0)
                {
                    _museumContext.Visitors.Remove(_museumContext.Visitors.ElementAt(VisitorsDataGrid.SelectedIndex));
                    _museumContext.SaveChanges();
                }
                    
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при удалении посетителя", "Внимание");
                return;
            }
        }

        private void UnselectAll_Click(object sender, RoutedEventArgs e)
        {
            VisitorsDataGrid.UnselectAll();
        }

        private void DeleteAllVisitors_Click(object sender, RoutedEventArgs e)
        {
            _museumContext.Visitors.ExecuteDelete();
        }

        private void SearchVisitorsBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitorsDataGrid.Items.Filter = v =>
            (v as Visitor).IsFieldEqulsValue((VisitorFields)SearchFieldComboBox.SelectedIndex,
                SearchValueTextBox.Text);

            if (VisitorsDataGrid.Items.Count == 0)
                MessageBox.Show("Не найдено ни одного посетителя", "Внмиание");
        }

        private void CancelSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchValueTextBox.Clear();
            VisitorsDataGrid.Items.Filter = null;
        }


        private void SearchValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchVisitorsBtn.IsEnabled = !string.IsNullOrWhiteSpace(SearchValueTextBox.Text);
        }
    }
}
