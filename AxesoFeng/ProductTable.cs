using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace AxesoFeng
{
    public partial class ProductTable : DataTable
    {
        public ProductTable()
        {
            // Create a new DataTable.
            //System.Data.DataTable table = new DataTable("ParentTable");
            DataTable table = this;
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;            

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "NOMBRE";
            column.AutoIncrement = false;
            
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);
        }

        public void addRow(String upc, String productName)
        {
            DataRow row;
            row = this.NewRow();
            row["ID"] = upc;
            row["NOMBRE"] = productName;
            this.Rows.Add(row);
        }


        /// 

        public DataGridTableStyle getStyle()
        {
            DataGridTableStyle tableStyle = new DataGridTableStyle();
            tableStyle.MappingName = this.TableName;

            DataColumn item;
            DataGridTextBoxColumn tbcName;
            item = this.Columns[0];
        
            tbcName = new DataGridTextBoxColumn();
            tbcName.Width = 120;
            tbcName.MappingName = item.ColumnName;
            tbcName.HeaderText = item.ColumnName;
            tableStyle.GridColumnStyles.Add(tbcName);

            item = this.Columns[1];
            tbcName = new DataGridTextBoxColumn();
            tbcName.Width = 200;
            tbcName.MappingName = item.ColumnName;
            tbcName.HeaderText = item.ColumnName;
            tableStyle.GridColumnStyles.Add(tbcName);
            ///
            return tableStyle;
        }

        public static void sortGrid(object sender, MouseEventArgs e)
        {
            DataGrid.HitTestInfo hitTest;
            //DataTable dataTable;
            DataView dataView;
            string columnName;
            DataGrid dataGrid;

            // Use only left mouse button clicks
            if (e.Button == MouseButtons.Left)
            {
                // Set dataGrid equal to the object that called this event handler
                dataGrid = (DataGrid)sender;

                // Perform a hit test to determine where the mousedown event occurred
                hitTest = dataGrid.HitTest(e.X, e.Y);

                // If the MouseDown event occurred on a column header,
                // then perform the sorting operation.
                if (hitTest.Type == DataGrid.HitTestType.ColumnHeader)
                {
                    // Get the DataTable associated with this DataGrid.
                    /*dataTable = (DataTable)dataGrid.DataSource;

                    // Get the DataView associated with the DataTable.
                    dataView = dataTable.DefaultView;*/
                    dataView = (DataView)dataGrid.DataSource;

                    // Get the name of the column that was clicked.
                    //if (dataGrid.TableStyles.Count != 0)
                    columnName = dataGrid.TableStyles[0].GridColumnStyles[hitTest.Column].MappingName;
                    //else
                    //columnName = dataTable.Columns[hitTest.Column].ColumnName;

                    // If the sort property of the DataView is already the current
                    // column name, sort that column in descending order.
                    // Otherwise, sort on the column name.
                    if (dataView.Sort == columnName)
                        dataView.Sort = columnName + " DESC";
                    else
                        dataView.Sort = columnName;
                }
            }
        }
    }
}
