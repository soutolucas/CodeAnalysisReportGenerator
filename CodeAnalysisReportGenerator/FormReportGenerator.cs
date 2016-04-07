using System;
using System.Windows.Forms;
using Microsoft.Build.Evaluation;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace CodeAnalysisReportGenerator
{
    public struct CancelExecution
    {
        public static bool cancelExecution;
    }

    public partial class FormReportGenerator : Form
    {
        private int progressBarValue;
        private List<DirectoryInfo> listDirectoryInfo;
        private List<ReportGenerator> listReportGenerator;

        private Properties.Settings properties = Properties.Settings.Default;

        public FormReportGenerator()
        {
            InitializeComponent();
        }

        private void FormReportGenerator_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
            pbGeneratorReport.Minimum = 0;
            CancelExecution.cancelExecution = false;
        }

        private async void btnReportGenerator_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateField();
                SaveConfiguration();

                listDirectoryInfo = new List<DirectoryInfo>();

                lblInformation.Text = "Getting directories...";
                DirectoryInfo directoryInfo = new DirectoryInfo(properties.DirectoryProject);
                await GetDirectory(directoryInfo);

                listReportGenerator = new List<ReportGenerator>();

                btnReportGenerator.Enabled = false;
                btnCancel.Enabled = true;

                lblInformation.Text = "Load rule set...";
                await GetRules();

                lblInformation.Text = "Find projects...";
                FindProjects();

                EndExecution();
            }
            catch (Exception ex)
            {
                ControlException(ex);
            }
        }

        private async void FindProjects()
        {
            List<FileInfo> listFileInfo;
            progressBarValue = 0;
            if (GetFiles("*.csproj", out listFileInfo))
            {
                pbGeneratorReport.Maximum = listFileInfo.Count;
                await BuildProject(listFileInfo);

                if (GetFiles("*CodeAnalysisLog*", out listFileInfo))
                {
                    pbGeneratorReport.Maximum += listFileInfo.Count;
                    await GetProjectLog(listFileInfo);
                }
                else
                    MessageBox.Show("Não foram encontrados arquivos CodeAnalysisLog no diretório selecionado", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Não foram encontrados arquivos .csproj no diretório selecionado","", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void EndExecution()
        {
            if (CancelExecution.cancelExecution)
            {
                pbGeneratorReport.Value = 0;
                lblInformation.Text = "";
                CancelExecution.cancelExecution = false;
            }
            else
                lblInformation.Text = "Relatório gerado com sucesso!";

            btnReportGenerator.Enabled = true;
            btnCancel.Enabled = false;
        }

        private async Task BuildProject(List<FileInfo> listFileInfo)
        {
            await Task.Run(() =>
            {
                using (ProjectCollection projectCollection = new ProjectCollection())
                {
                    Project project;

                    foreach (var file in listFileInfo)
                    {
                        if (CancelExecution.cancelExecution)
                            break;

                        project = projectCollection.LoadProject(file.FullName);
                        project.Build();
                        projectCollection.UnloadProject(project);

                        //Update status of execution
                        SetControlPropertyValue(pbGeneratorReport, "value", ++progressBarValue);
                    }
                }
            });
        }

        private async Task GetProjectLog(List<FileInfo> listFileInfo)
        {
            await Task.Run(() =>
            {
                ReportGenerator reportGenerator;

                foreach (var file in listFileInfo)
                {
                    var result = file.Name.Split('.');
                    reportGenerator = new ReportGenerator(result?[0]);
                    reportGenerator.GetCodeAnalysisLog(file.FullName);
                    listReportGenerator.Add(reportGenerator);

                    //Update status of execution
                    SetControlPropertyValue(pbGeneratorReport, "value", ++progressBarValue);
                }
            });
        }

        private void SaveConfiguration()
        {
            properties.DirectoryProject = txtProjectDirectory.Text;
            properties.DirectoryRuleSet = txtDirectoryRuleSet.Text;
            properties.DirectoryExcelTemplate = txtDirectoryTemplateExcel.Text;

            properties.Save();
        }

        private void LoadConfiguration()
        {
            txtProjectDirectory.Text = properties.DirectoryProject;
            txtDirectoryRuleSet.Text = properties.DirectoryRuleSet;
            txtDirectoryTemplateExcel.Text = properties.DirectoryExcelTemplate;
        }

        private async Task GetRules()
        {
            var listRulesError = new List<string>();
            using (StreamReader sr = new StreamReader(properties.DirectoryRuleSet))
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (CancelExecution.cancelExecution)
                        break;

                    if (line.Contains("<Rule") && line.Contains("Action") && line.Contains("Error"))
                    {
                        string ruleError = Regex.Match(line, @"\bCA\w*[0-9]\b", RegexOptions.IgnoreCase).Value;
                        listRulesError.Add(ruleError);
                    }
                }
            }

            ReportGenerator.listRulesError = listRulesError;

            if (listRulesError.Count < 1 && !CancelExecution.cancelExecution)
                MessageBox.Show("O arquivo RuleSet não contém regras do tipo \"Error\"", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = SetControlPropertyValue;
                oControl.Invoke(d, new object[] { oControl, propName, propValue });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();

                PropertyInfo property = props.Where(p => p.Name.ToUpper() == propName.ToUpper()).FirstOrDefault();
                property.SetValue(oControl, propValue);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelExecution.cancelExecution = true;
            lblInformation.Text = "Cancel...";
        }

        private void ValidateField()
        {
            if (string.IsNullOrWhiteSpace(txtProjectDirectory.Text) || string.IsNullOrWhiteSpace(txtDirectoryRuleSet.Text) ||
                string.IsNullOrWhiteSpace(txtDirectoryTemplateExcel.Text))
            {
                throw new Exception("Todos os campos devem ser preenchidos!");
            }
        }

        private void btnProjectDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                folder.ShowDialog();
                txtProjectDirectory.Text = folder.SelectedPath;
            }
            catch (Exception ex)
            {
                ControlException(ex);
            }
        }

        private void OpenFileDialog_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.ShowDialog();
                txtDirectoryRuleSet.Text = file.FileName;
            }
            catch (Exception ex)
            {
                ControlException(ex);
            }
        }

        private void ControlException(Exception ex)
        {
            CancelExecution.cancelExecution = true;
            EndExecution();

            MessageBox.Show(String.Format("Houve um problema na extração do Code Analysis: \n\n {0} \n\n",
                                ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool GetFiles(string fileExtension, out List<FileInfo> listFileInfo)
        {
            listFileInfo = new List<FileInfo>();
            FileInfo file;

            foreach (var directory in listDirectoryInfo)
            {
                file = directory.GetFiles(fileExtension).FirstOrDefault();
                if (file != null)
                    listFileInfo.Add(file);
            }

            return listFileInfo.Count > 0;
        }

        private async Task GetDirectory(DirectoryInfo d)
        {
           await Task.Run(async () =>
            {
                foreach (var item in d.GetDirectories())
                {
                    listDirectoryInfo.Add(item);
                    await GetDirectory(item);
                }
            });
        }

    }
}
