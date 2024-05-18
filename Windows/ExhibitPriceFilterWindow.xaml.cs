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

        /// <summary>
        /// Значения, которые метод CompareTo() должен вернуть
        /// </summary>
        public List<int> ComparisonValues { get; private set; } = new List<int>(2);
        /// <summary>
        /// Значения фильтра для цены
        /// </summary>
        public double? PriceValue { get; private set; } = null;

        /// <summary>
        /// Позволяет управлять окном с клавиатуры: 
        /// Enter - применить, Escape - отменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AcceptFilterBtn.IsEnabled)
                AcceptFilterBtn_Click(sender, e);
            if (e.Key == Key.Escape)
                CancelBtn_Click(sender, e);
        }

        /// <summary>
        /// Отменяет добавление фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Применяет фильтр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptFilterBtn_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Значение фильтра для цены не может быть отрицательным", "Внимание");
                PriceValue = null;
                return;
            }

            DialogResult = true;
        }

        /// <summary>
        /// Проверяет значение цены фильтра на пустоту. Если оно пустое, блокирует возможность применения фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptFilterBtn.IsEnabled = !string.IsNullOrWhiteSpace(PriceValueTextBox.Text);
        }
    }
}
