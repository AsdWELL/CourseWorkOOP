using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static CourseWork.MainWindow;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для CreateExhibitWindow.xaml
    /// </summary>
    public partial class CreateExhibitWindow : Window
    {
        public CreateExhibitWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (NewExhibit != null)
            {
                ExhibitTitleTextBox.Text = NewExhibit.Title;
                ExhibitEpochTextBox.Text = NewExhibit.Epoch;
                ExhibitAuthorTextBox.Text = NewExhibit.Author;
                ExhibitPriceTextBox.Text = NewExhibit.Price.ToString();
            }
        }

        /// <summary>
        /// Новый экспонат, с параметрами, указанными пользователем
        /// </summary>
        public Exhibit? NewExhibit { get; private set; } = null;

        /// <summary>
        /// Открывает окно для редактирования экспоната
        /// </summary>
        /// <param name="exhibit">Редактируемый экспонат</param>
        /// <returns>True, если пользователь сохранил изменения, иначе False</returns>
        public bool? ShowDialog(Exhibit exhibit)
        {
            Title = "Редактирование экспоната";
            NewExhibit = exhibit;
            return ShowDialog();
        }

        /// <summary>
        /// Отменяет измненения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Сохраняет изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveExhibitBtn_Click(object sender, RoutedEventArgs e)
        {
            double price;

            try
            {
                price = Convert.ToDouble(ExhibitPriceTextBox.Text.Replace(".", ","));
            }
            catch
            {
                MessageBox.Show("Цена дожна быть числом", "Внимание");
                return;
            }

            if (_museumContext.Exhibits.ToList().Any(exhibit =>
                exhibit.Title != NewExhibit?.Title &&
                exhibit.Title.ToLower().Equals(ExhibitTitleTextBox.Text.Trim().ToLower())))
            {
                MessageBox.Show("Экспонат с таким названием уже есть в базе данных", "Внимание");
                return;
            }

            try
            {
                NewExhibit = new Exhibit(ExhibitTitleTextBox.Text.Trim(),
                    ExhibitAuthorTextBox.Text.Trim(),
                    ExhibitEpochTextBox.Text.Trim(),
                    price);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание");
                return;
            }

            DialogResult = true;
        }

        /// <summary>
        /// Проверяет, что все поля на форме не пустые
        /// Если хотя бы одно поле пустое блокирует возможность сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckFieldsNotEmpty(object sender, TextChangedEventArgs e)
        {
            SaveExhibitBtn.IsEnabled = !string.IsNullOrWhiteSpace(ExhibitTitleTextBox.Text)
                && !string.IsNullOrWhiteSpace(ExhibitEpochTextBox.Text)
                && !string.IsNullOrWhiteSpace(ExhibitAuthorTextBox.Text)
                && !string.IsNullOrWhiteSpace(ExhibitPriceTextBox.Text);
        }

        /// <summary>
        /// Позволяет управлять окном с клавиатуры: 
        /// Enter - сохранить, Escape - отменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SaveExhibitBtn.IsEnabled)
                SaveExhibitBtn_Click(sender, e);
            else if (e.Key == Key.Escape)
                CancelBtn_Click(sender, e);
        }
    }
}
