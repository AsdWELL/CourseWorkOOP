using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace CourseWork.Windows
{
    /// <summary>
    /// Логика взаимодействия для ExhibitPriceFilterWindow.xaml
    /// </summary>
    public partial class ExhibitPriceFilterWindow : Window
    {
        public ExhibitPriceFilterWindow()
        {
            InitializeComponent();
        }

        public List<int> ComparisonValues { get; private set; } = new List<int>(2);
        public double? PriceValue { get; private set; } = null;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AcceptFilterBtn_Click(sender, e);
            if (e.Key == Key.Escape)
                CancelBtn_Click(sender, e);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AcceptFilterBtn_Click(object sender, RoutedEventArgs e)
        {         
            try
            {
                PriceValue = Convert.ToDouble(PriceValueTextBox.Text.Replace(".", ","));
            }
            catch
            {
                MessageBox.Show("Значение фильтра для цены должно быть числом", "Внимание");
                return;
            }

            if (PriceValue < 0)
            {
                MessageBox.Show("Значение фильтра для цены не может быть меньше 0", "Внимание");
                PriceValue = null;
                return;
            }

            DialogResult = true;
        }

        private void OperationCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComparisonValues.Clear();

            switch (OperationCombobox.SelectedIndex)
            {
                case 0:
                    ComparisonValues.Add(-1);
                    break;
                case 1:
                    ComparisonValues.AddRange([-1, 0]);
                    break;
                case 2:
                    ComparisonValues.Add(1);
                    break;
                case 3:
                    ComparisonValues.AddRange([1, 0]);
                    break;
            }
        }

        private void PriceValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptFilterBtn.IsEnabled = !string.IsNullOrWhiteSpace(PriceValueTextBox.Text);
        }
    }
}
