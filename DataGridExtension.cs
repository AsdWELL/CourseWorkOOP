using System.Windows.Controls;

namespace CourseWork
{
    public static class DataGridExtension
    {
        public static void ClearSort(this DataGrid dataGrid)
        {
            dataGrid.Items.SortDescriptions.Clear();

            foreach (var col in dataGrid.Columns)
                col.SortDirection = null;
        }
    }
}
