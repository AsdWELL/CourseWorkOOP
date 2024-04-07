using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                ExhibitDescriptionTextBox.Text = NewExhibit.Description;
                ExhibitPriceTextBox.Text = NewExhibit.Price.ToString();
            }
        }

        public Exhibit NewExhibit { get; private set; }

        public bool? ShowDialog(Exhibit exhibit)
        {
            Title = "Редактирование экспоната";
            NewExhibit = exhibit;
            return ShowDialog();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

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

            try
            {
                NewExhibit = new Exhibit(ExhibitTitleTextBox.Text,
                ExhibitDescriptionTextBox.Text,
                ExhibitEpochTextBox.Text,
                price);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание");
                return;
            }

            DialogResult = true;
        }

        private void CheckInput(object sender, TextChangedEventArgs e)
        {
            SaveExhibitBtn.IsEnabled = !string.IsNullOrWhiteSpace(ExhibitTitleTextBox.Text)
                && !string.IsNullOrWhiteSpace(ExhibitEpochTextBox.Text)
                && !string.IsNullOrWhiteSpace(ExhibitDescriptionTextBox.Text)
                && !string.IsNullOrWhiteSpace(ExhibitPriceTextBox.Text);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SaveExhibitBtn_Click(sender, e);
            else if (e.Key == Key.Escape)
                CancelBtn_Click(sender, e);
        }
    }
}
