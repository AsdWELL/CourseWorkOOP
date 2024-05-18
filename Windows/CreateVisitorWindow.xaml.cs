using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для VisitorForm.xaml
    /// </summary>
    public partial class CreateVisitorWindow : Window
    {
        public CreateVisitorWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (NewVisitor != null)
            {
                VisitorNameTextBox.Text = NewVisitor.Name;
                VisitorSurnameTextBox.Text = NewVisitor.Surname;
                VisitorVisitDate.SelectedDate = NewVisitor.VisitDate;
            }
            else
                VisitorVisitDate.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Новый посетитель, с параметрами, указанными пользователем
        /// </summary>
        public Visitor? NewVisitor { get; private set; } = null;

        /// <summary>
        /// Открывает окно для редактирования посетителя
        /// </summary>
        /// <param name="visitor">Редактируемый посетитель</param>
        /// <returns>True, если пользователь сохранил изменения, иначе False</returns>
        public bool? ShowDialog(Visitor visitor)
        {
            Title = "Редактирование посетителя";
            NewVisitor = visitor;
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
        private void SaveVisitorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewVisitor = new Visitor(
                    VisitorNameTextBox.Text.Trim(),
                    VisitorSurnameTextBox.Text.Trim(),
                    (DateTime)VisitorVisitDate.SelectedDate);
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
            SaveVisitorBtn.IsEnabled = !string.IsNullOrWhiteSpace(VisitorNameTextBox.Text)
                && !string.IsNullOrWhiteSpace(VisitorSurnameTextBox.Text);
        }

        /// <summary>
        /// Позволяет управлять окном с клавиатуры: 
        /// Enter - сохранить, Escape - отменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SaveVisitorBtn.IsEnabled)
                SaveVisitorBtn_Click(sender, e);
            else if (e.Key == Key.Escape)
                CancelBtn_Click(sender, e);
        }
    }
}
