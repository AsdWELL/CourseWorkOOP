﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

            DataContext = _museumContext.Visitors;

            VisitorsDataGrid.ItemsSource = _museumContext.Visitors.Local.ToObservableCollection();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {           
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
                selectedVisitor = (Visitor)VisitorsDataGrid.SelectedItem;
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
                Visitor? visitor = _museumContext.Visitors.Find(selectedVisitor.Id);

                if (visitor == null)
                {
                    MessageBox.Show("Произошла ошибка при редактировании выбранного посетителя\n" +
                        $"Пользователь {selectedVisitor.Name} не найден в базе данных", "Внимание");
                    return;
                }

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
                    _museumContext.Visitors.Remove((Visitor)VisitorsDataGrid.SelectedItem);
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
            _museumContext.Visitors.RemoveRange(_museumContext.Visitors.ToList());

            _museumContext.SaveChanges();

            VisitorsDataGrid.Items.Refresh();
        }

        private void SearchVisitorsBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitorsDataGrid.Items.Filter = v =>
            ((Visitor)v).IsFieldEqulsValue((VisitorFields)SearchFieldComboBox.SelectedIndex,
                SearchValueTextBox.Text);
        }

        private void CancelSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitorsDataGrid.Items.SortDescriptions.Clear();
            SearchValueTextBox.Clear();
            VisitorsDataGrid.Items.Filter = null;
        }

        private void ClearSortBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitorsDataGrid.ClearSort();
        }

        private void SearchValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchVisitorsBtn.IsEnabled = !string.IsNullOrWhiteSpace(SearchValueTextBox.Text);
        }
    }
}
