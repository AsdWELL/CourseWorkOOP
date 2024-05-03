using CourseWork.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using static CourseWork.MainWindow;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для ExhibitsWindow.xaml
    /// </summary>
    public partial class ExhibitsWindow : Window
    {
        private Predicate<object>? _searchExhibitsCondition = null;
        
        public ExhibitsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _museumContext.Exhibits.Load();
            ExhibitsDataGrid.ItemsSource = _museumContext.Exhibits.Local.ToObservableCollection();
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

        private void ExhibitsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isEnabled = ExhibitsDataGrid.SelectedItems.Count > 0;

            EditExhibitBtn.IsEnabled = isEnabled;
            DeleteSelectedBtn.IsEnabled = isEnabled;
        }

        private void AddNewExhibitBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateExhibitWindow exhibitWindow = new CreateExhibitWindow();
            bool? result = exhibitWindow.ShowDialog();
            if (result == true)
            {
                _museumContext.Exhibits.Add(exhibitWindow.NewExhibit);
                _museumContext.SaveChanges();
            }
        }

        private void EditExhibitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ExhibitsDataGrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Выберите один экспонат для редактирования", "Внимание");
                return;
            }

            Exhibit selectedExhibit;
            try
            {
                selectedExhibit = (Exhibit)ExhibitsDataGrid.SelectedItem;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при выборе экспоната", "Внимание");
                return;
            }

            CreateExhibitWindow exhibitWindow = new CreateExhibitWindow();
            bool? result = exhibitWindow.ShowDialog(selectedExhibit);

            if (result == true)
            {
                Exhibit? exhibit= _museumContext.Exhibits.Find(selectedExhibit.Id);
                
                if (exhibit == null)
                {
                    MessageBox.Show("Произошла ошибка при редактировании выбранного экспоната\n" +
                        $"Экспонат {selectedExhibit.Title} не найден в базе данных", "Внимание");
                    return;
                }

                exhibit.Title = exhibitWindow.NewExhibit.Title;
                exhibit.Description = exhibitWindow.NewExhibit.Description;
                exhibit.Epoch = exhibitWindow.NewExhibit.Epoch;
                exhibit.Price = exhibitWindow.NewExhibit.Price;

                _museumContext.SaveChanges();
                ExhibitsDataGrid.Items.Refresh();
            }
        }

        private void DeleteSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                while (ExhibitsDataGrid.SelectedItems.Count > 0)
                {
                    _museumContext.Exhibits.Remove((Exhibit)ExhibitsDataGrid.SelectedItem);
                    _museumContext.SaveChanges();
                }

            }
            catch
            {
                MessageBox.Show("Произошла ошибка при удалении экспоната", "Внимание");
                return;
            }
        }

        private void UnselectAll_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsDataGrid.UnselectAll();
        }

        private void DeleteAllExhibits_Click(object sender, RoutedEventArgs e)
        {
            _museumContext.Exhibits.ExecuteDelete();
        }

        private void SearchExhibitsBtn_Click(object sender, RoutedEventArgs e)
        {
            _searchExhibitsCondition = e =>
            ((Exhibit)e).IsFieldEqulsValue((ExhibitFields)SearchFieldComboBox.SelectedIndex,
                SearchValueTextBox.Text);

            ExhibitsDataGrid.Items.Filter += _searchExhibitsCondition;

            if (ExhibitsDataGrid.Items.Count == 0)
                MessageBox.Show("Не найдено ни одного экспоната", "Внимание");
        }

        private void CancelSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchValueTextBox.Clear();

            ExhibitsDataGrid.Items.Filter -= _searchExhibitsCondition;
            _searchExhibitsCondition = null;
        }

        private void ClearSortBtn_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsDataGrid.ClearSort();
        }

        private void SearchValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchExhibitsBtn.IsEnabled = !string.IsNullOrWhiteSpace(SearchValueTextBox.Text);
        }

        private void AddPriceFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            ExhibitPriceFilterWindow filterWindow = new ExhibitPriceFilterWindow();
            bool? result = filterWindow.ShowDialog();
            
            if (result == true)
            {
                var filter = ExhibitsDataGrid.Items.Filter;

                ExhibitsDataGrid.Items.Filter = e => (filter?.Invoke(e) ?? true)
                    && filterWindow.ComparisonValues.Any(value =>
                        value == ((Exhibit)e).Price.CompareTo(filterWindow.PriceValue));

                if (ExhibitsDataGrid.Items.Count == 0)
                    MessageBox.Show("Не найдено ни одного экспоната", "Внимание");
            }
        }

        private void ClearFiltersBtn_Click(object sender, RoutedEventArgs e)
        {   
            ExhibitsDataGrid.Items.Filter = _searchExhibitsCondition;
        }
    }
}