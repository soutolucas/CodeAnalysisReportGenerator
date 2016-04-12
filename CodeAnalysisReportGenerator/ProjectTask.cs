using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Build.Evaluation;

namespace CodeAnalysisReportGenerator
{
    public delegate void MaximumStatusValueEventHandler(object sender, EventArgs e);
    public delegate void StatusEventHandler(object sender, EventArgs e);
    public delegate void MessageInformationEventHandler(object sender, EventArgs e);

    public class ProjectTask
    {
        private List<DirectoryInfo> listDirectoryInfo;
        private List<FileInfo> listFileInfo;
        private ReportGenerator reportGenerator;
        public List<ReportGenerator> listReportGenerator { get; private set; }

        public static int maximunStatusValue { get; private set; } = 0;
        public static int statusValue { get; private set; }
        public static string messageInformation { get; private set; }

        public event MaximumStatusValueEventHandler UpdateMaximumStatusValueEventHandler;
        public event StatusEventHandler UpdateStatusEventHandler;
        public event MessageInformationEventHandler SetMessageInformationEventHandler;

        private void OnUpdateMaximumValueEventHandler(EventArgs e)
        {
            if (UpdateMaximumStatusValueEventHandler != null)
                UpdateMaximumStatusValueEventHandler(this, e);
        }

        private void OnUpdateStatusEventHandler(EventArgs e)
        {
            if (UpdateStatusEventHandler != null)
                UpdateStatusEventHandler(this, e);
        }

        private void OnSetInformationMessageEventHandler(EventArgs e)
        {
            if (SetMessageInformationEventHandler != null)
                SetMessageInformationEventHandler(this, e);
        }

        private void UpdateMaximumStatusValue()
        {
            maximunStatusValue += listFileInfo.Count;
            OnUpdateMaximumValueEventHandler(new EventArgs());
        }

        private void UpdateStatusValue()
        {
            statusValue++;
            OnUpdateStatusEventHandler(new EventArgs());
        }

        private void SetMessageInformation(string message)
        {
            messageInformation = message;
            OnSetInformationMessageEventHandler(new EventArgs());
        }

        public ProjectTask(DirectoryInfo directoryInfo)
        {
            SetMessageInformation("Getting directories...");
            listDirectoryInfo = new List<DirectoryInfo>();
            GetDirectory(directoryInfo);
            SetMessageInformation("Get directories completed!");
        }

        public async Task FindProject()
        {
            await Task.Run(() =>
             {
                 SetMessageInformation("Find projects...");
                 if (GetFile("*.csproj"))
                 {
                     UpdateMaximumStatusValue();

                     SetMessageInformation("Building projects...");
                     BuildProject();
                     SetMessageInformation("Build completed!");
                 }
                 else
                     SetMessageInformation("There were not found csproj files in the designated directory!");
             });
        }

        private void BuildProject()
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

                    reportGenerator = new ReportGenerator(file.Name);
                    UpdateStatusValue();
                }
            }
        }

        public async Task FindProjectLog()
        {
            await Task.Run(() =>
            {
                SetMessageInformation("Find code analysis log...");
                if (GetFile("*CodeAnalysisLog*"))
                {
                    UpdateMaximumStatusValue();

                    SetMessageInformation("Getting code analysis log...");
                    GetProjectLog();
                    SetMessageInformation("Get code analysis log completed!");
                }
                else
                    SetMessageInformation("There were not found Code Analysis Log files in the designated directory!");
            });
        }

        private void GetProjectLog()
        {
            listReportGenerator = new List<ReportGenerator>();

            foreach (var file in listFileInfo)
            {
                reportGenerator?.GetCodeAnalysisLog(file.FullName);
                listReportGenerator.Add(reportGenerator);

                UpdateStatusValue();
            }
        }

        private bool GetFile(string fileExtension)
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

        private void GetDirectory(DirectoryInfo d)
        {
            foreach (var item in d.GetDirectories())
            {
                listDirectoryInfo.Add(item);
                GetDirectory(item);
            }
        }
    }
}
