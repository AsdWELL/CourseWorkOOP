using CourseWork.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using static CourseWork.MainWindow;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для ExhibitsWindow.xaml
    /// </summary>
    public partial class ExhibitsWindow : Window
    {
        private Predicate<object>? _searchExhibitsCondition = null;
        private List<Predicate<object>> _priceFilters;
        
        public ExhibitsWindow()
        {
            InitializeComponent();

            _priceFilters = [];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _museumContext.Exhibits.Load();

            DataContext = _museumContext.Exhibits;

            ExhibitsDataGrid.ItemsSource = _museumContext.Exhibits.Local.ToObservableCollection();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
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
                exhibit.Author = exhibitWindow.NewExhibit.Author;
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
            _museumContext.Exhibits.RemoveRange(_museumContext.Exhibits.ToList());

            _museumContext.SaveChanges();
        }

        private void SearchExhibitsBtn_Click(object sender, RoutedEventArgs e)
        {
            _searchExhibitsCondition = e =>
            ((Exhibit)e).IsFieldEqulsValue((ExhibitFields)SearchFieldComboBox.SelectedIndex,
                SearchValueTextBox.Text);

            SetFilter();
        }

        private void CancelSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchValueTextBox.Clear();

            _searchExhibitsCondition = null;

            SetFilter();
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
                _priceFilters.Add(e => filterWindow.ComparisonValues.Any(value =>
                        value == ((Exhibit)e).Price.CompareTo(filterWindow.PriceValue)));

                SetFilter();
            }
        }

        private void ClearFiltersBtn_Click(object sender, RoutedEventArgs e)
        {   
            _priceFilters.Clear();
            ExhibitsDataGrid.Items.Filter = _searchExhibitsCondition;
        }

        private void SetFilter()
        {
            if (_priceFilters.Count == 0 && _searchExhibitsCondition == null)
            {
                ExhibitsDataGrid.Items.Filter = null;
                return;
            }
            
            ExhibitsDataGrid.Items.Filter = e =>
            {
                bool isValid = true;
                _priceFilters.ForEach(filter => isValid = isValid && filter.Invoke(e));
                return (_searchExhibitsCondition?.Invoke(e) ?? true) && isValid;
            };
        }
    }
}