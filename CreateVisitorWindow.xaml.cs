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
            VisitorVisitDate.SelectedDate = DateTime.Now;
            if (NewVisitor != null)
            {
                VisitorNameTextBox.Text = NewVisitor.Name;
                VisitorSurnameTextBox.Text = NewVisitor.Surname;
                VisitorVisitDate.SelectedDate = NewVisitor.VisitDate;
            }
        }

        public Visitor NewVisitor { get; private set; }

        public bool? ShowDialog(Visitor visitor)
        {
            NewVisitor = visitor;
            return ShowDialog();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveVisitorBtn_Click(object sender, RoutedEventArgs e)
        {
            Visitor newVisitor;
            try
            {
                newVisitor = new Visitor(
                VisitorNameTextBox.Text,
                VisitorSurnameTextBox.Text,
                (DateTime)VisitorVisitDate.SelectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание");
                return;
            }
            NewVisitor = newVisitor;
            DialogResult = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SaveVisitorBtn_Click(sender, e);
            else if (e.Key == Key.Escape)
                CancelBtn_Click(sender, e);
        }
    }
}
