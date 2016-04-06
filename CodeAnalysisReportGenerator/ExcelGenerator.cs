using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CodeAnalysisReportGenerator
{
    public class ExcelGenerator
    {
        private String stringConection = String.Format(@Properties.Settings.Default.ConnectionStringExcel, Properties.Settings.Default.DirectoryExcelTemplate);
        private OleDbCommand oleCommand;
        private readonly List<ReportGenerator> listReportGenerator;

        public ExcelGenerator(List<ReportGenerator> listReportGenerator)
        {
            this.listReportGenerator = listReportGenerator;
        }

        public async Task Insert()
        {
            await Task.Run(() =>
            {
                using (OleDbConnection oleConnection = new OleDbConnection(stringConection))
                {
                    oleConnection.Open();
                    oleCommand = new OleDbCommand();
                    oleCommand.Connection = oleConnection;
                    oleCommand.CommandType = CommandType.Text;

                    string query = "INSERT INTO [Export$] ([Erro]) VALUES (@Erro)";

                    oleCommand.CommandText = query;

                    foreach (var itemReport in listReportGenerator)
                    {
                        if (CancelExecution.cancelExecution)
                            break;
                    }
                }
            });
        }

    }
}
