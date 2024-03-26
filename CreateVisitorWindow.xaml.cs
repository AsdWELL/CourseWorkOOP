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
        }

        public Visitor NewVisitor { get; private set; }

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
    }
}
