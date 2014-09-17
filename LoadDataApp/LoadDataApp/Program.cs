using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace LoadDataApp
{
    class Program
    {
        private static DataTable _dataTable;

        [STAThread]
        static void Main(string[] args)
        {
            ReadExcel();
            DataLayer.SaveData(_dataTable);
        }

        private static void ReadExcel()
        {
            try
            {
                string fileName = ConfigurationManager.AppSettings["FileName"];
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook theWorkbook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, XlPlatform.xlWindows,
                    "\t", false, false, 0, true);
                Sheets sheets = theWorkbook.Worksheets;
                Worksheet worksheet = (Worksheet)sheets.Item[1];
                CreateDataTable();
                for (int i = 2; i <= worksheet.UsedRange.Rows.Count; i++)
                {
                    Range range = worksheet.Range["A" + i, "C" + i];
                    Array myvalues = (Array)range.Cells.Value;
                    _dataTable.Rows.Add(new[] {myvalues.GetValue(1, 1), myvalues.GetValue(1, 2), myvalues.GetValue(1, 3)});
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(ex.ToString());
            }
        }

        private static void CreateDataTable()
        {
            _dataTable = new System.Data.DataTable();
            _dataTable.Columns.Add("BusinessName");
            _dataTable.Columns.Add("GroupName");
            _dataTable.Columns.Add("PhoneNumber");
        }
    }
}
