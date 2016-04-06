using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAnalysisReportGenerator
{
    public class ReportGenerator
    {
        public static List<string> listRulesError;

        public readonly string projectName;
        public int errors { get; private set; }
        public int warnings { get; private set; }
        public string rule { get; private set; }
        public string category { get; private set; }
        public string errorReportGenerator { get; private set; }

        public ReportGenerator(string projectName)
        {
            if (listRulesError == null)
                throw new Exception("O arquivo RuleSet está vazio!");

            this.projectName = projectName;
        }

        public void GetCodeAnalysisLog(string codeAnalysisLogPath)
        {
            if (File.Exists(codeAnalysisLogPath))
                CountIssues(codeAnalysisLogPath);
            else
                errorReportGenerator = "CodeAnalysisLog não existe";
        }

        private async void CountIssues(string codeAnalysisLogPath)
        {
            string line;
            string patternRule = @"\bCA\w*[0-9]\b";
            string patternCategory = @"\bMicrosoft\.\w*\b";

            using (StreamReader sr = new StreamReader(codeAnalysisLogPath))
            {
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (CancelExecution.cancelExecution)
                        break;

                    if (line.Contains("<Message") && line.Contains("CheckId"))
                    {
                        rule = Regex.Match(line, patternRule, RegexOptions.IgnoreCase).Value;
                        category = Regex.Match(line, patternCategory, RegexOptions.IgnoreCase).Value;
                    }
                    else if (line.Contains("<Issue") && line.Contains("</Issue>"))
                    {
                        if (listRulesError.Where(l => l.Equals(rule)).Count() > 0)
                            errors++;
                        else
                            warnings++;
                    }
                }
            }
        }

    }
}