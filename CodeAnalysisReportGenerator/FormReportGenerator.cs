using System;
using System.Windows.Forms;
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
                pbGeneratorReport.Value = 0;

                ValidateField();
                SaveConfiguration();

                lblInformation.Text = "Load ruleset file...";
                await GetRules();

                GetProjects();

                btnReportGenerator.Enabled = false;
                btnCancel.Enabled = true;

                EndExecution();
            }
            catch (Exception ex)
            {
                ControlException(ex.Message);
            }
        }

        private async void GetProjects()
        {
            var directoryInfo = new DirectoryInfo(properties.DirectoryProject);

            ProjectTask projectTask = new ProjectTask(directoryInfo);
            projectTask.UpdateMaximumStatusValueEventHandler += new MaximumStatusValueEventHandler(UpdateMaximumProgressValue);
            projectTask.UpdateStatusEventHandler += new StatusEventHandler(UpdateProgressValue);
            projectTask.SetMessageInformationEventHandler += new MessageInformationEventHandler(UpdateMessage);

            await projectTask.FindProject();
           
            await projectTask.FindProjectLog();
        }

        private void UpdateMaximumProgressValue(object sender, EventArgs e)
        {
            SetControlPropertyValue(pbGeneratorReport, "maximum", ProjectTask.maximunStatusValue);
        }

        private void UpdateProgressValue(object sender, EventArgs e)
        {
            SetControlPropertyValue(pbGeneratorReport, "value", ProjectTask.statusValue);
        }

        private void UpdateMessage(object sender, EventArgs e)
        {
            SetControlPropertyValue(lblInformation, "text", ProjectTask.messageInformation);
        }

        

        private void EndExecution()
        {
            if (CancelExecution.cancelExecution)
            {
                pbGeneratorReport.Value = 0;
                lblInformation.Text = "";
                CancelExecution.cancelExecution = false;
            }

            btnReportGenerator.Enabled = true;
            btnCancel.Enabled = false;
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
                MessageBox.Show("The ruleset file does not have \"error\" type rules", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ControlException("All fields must be filled!");
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
                ControlException(ex.Message);
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
                ControlException(ex.Message);
            }
        }

        private void ControlException(string message)
        {
            CancelExecution.cancelExecution = true;
            EndExecution();

            MessageBox.Show($"There was a problem extracting Code Analysis! \n\n {message} \n\n", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
