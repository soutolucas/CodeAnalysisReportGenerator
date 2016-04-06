namespace CodeAnalysisReportGenerator
{
    partial class FormReportGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReportGenerator = new System.Windows.Forms.Button();
            this.txtDirectoryRuleSet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.btnExcelDirectory = new System.Windows.Forms.Button();
            this.btnRuleSetDirectory = new System.Windows.Forms.Button();
            this.lblDirectory = new System.Windows.Forms.Label();
            this.txtProjectDirectory = new System.Windows.Forms.TextBox();
            this.btnProjectDirectory = new System.Windows.Forms.Button();
            this.lblPathTemplateExcel = new System.Windows.Forms.Label();
            this.txtDirectoryTemplateExcel = new System.Windows.Forms.TextBox();
            this.pbGeneratorReport = new System.Windows.Forms.ProgressBar();
            this.lblInformation = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbConfiguration.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReportGenerator
            // 
            this.btnReportGenerator.BackColor = System.Drawing.Color.Gray;
            this.btnReportGenerator.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReportGenerator.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReportGenerator.Location = new System.Drawing.Point(479, 239);
            this.btnReportGenerator.Name = "btnReportGenerator";
            this.btnReportGenerator.Size = new System.Drawing.Size(92, 33);
            this.btnReportGenerator.TabIndex = 5;
            this.btnReportGenerator.Text = "Gerar Relatório";
            this.btnReportGenerator.UseVisualStyleBackColor = false;
            this.btnReportGenerator.Click += new System.EventHandler(this.btnReportGenerator_Click);
            // 
            // txtDirectoryRuleSet
            // 
            this.txtDirectoryRuleSet.Location = new System.Drawing.Point(16, 94);
            this.txtDirectoryRuleSet.Name = "txtDirectoryRuleSet";
            this.txtDirectoryRuleSet.Size = new System.Drawing.Size(494, 20);
            this.txtDirectoryRuleSet.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Diretório arquivo Rule Set";
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.Controls.Add(this.btnExcelDirectory);
            this.gbConfiguration.Controls.Add(this.btnRuleSetDirectory);
            this.gbConfiguration.Controls.Add(this.lblDirectory);
            this.gbConfiguration.Controls.Add(this.txtProjectDirectory);
            this.gbConfiguration.Controls.Add(this.btnProjectDirectory);
            this.gbConfiguration.Controls.Add(this.lblPathTemplateExcel);
            this.gbConfiguration.Controls.Add(this.label1);
            this.gbConfiguration.Controls.Add(this.txtDirectoryTemplateExcel);
            this.gbConfiguration.Controls.Add(this.txtDirectoryRuleSet);
            this.gbConfiguration.Location = new System.Drawing.Point(12, 12);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(559, 178);
            this.gbConfiguration.TabIndex = 9;
            this.gbConfiguration.TabStop = false;
            this.gbConfiguration.Text = "Configuração";
            // 
            // btnExcelDirectory
            // 
            this.btnExcelDirectory.Location = new System.Drawing.Point(516, 145);
            this.btnExcelDirectory.Name = "btnExcelDirectory";
            this.btnExcelDirectory.Size = new System.Drawing.Size(37, 23);
            this.btnExcelDirectory.TabIndex = 15;
            this.btnExcelDirectory.Text = "...";
            this.btnExcelDirectory.UseVisualStyleBackColor = true;
            this.btnExcelDirectory.Click += new System.EventHandler(this.OpenFileDialog_Click);
            // 
            // btnRuleSetDirectory
            // 
            this.btnRuleSetDirectory.Location = new System.Drawing.Point(516, 94);
            this.btnRuleSetDirectory.Name = "btnRuleSetDirectory";
            this.btnRuleSetDirectory.Size = new System.Drawing.Size(37, 23);
            this.btnRuleSetDirectory.TabIndex = 14;
            this.btnRuleSetDirectory.Text = "...";
            this.btnRuleSetDirectory.UseVisualStyleBackColor = true;
            this.btnRuleSetDirectory.Click += new System.EventHandler(this.OpenFileDialog_Click);
            // 
            // lblDirectory
            // 
            this.lblDirectory.AutoSize = true;
            this.lblDirectory.Location = new System.Drawing.Point(13, 30);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(146, 13);
            this.lblDirectory.TabIndex = 13;
            this.lblDirectory.Text = "Diretório dos projetos (.csproj)";
            // 
            // txtProjectDirectory
            // 
            this.txtProjectDirectory.Location = new System.Drawing.Point(16, 46);
            this.txtProjectDirectory.Name = "txtProjectDirectory";
            this.txtProjectDirectory.Size = new System.Drawing.Size(494, 20);
            this.txtProjectDirectory.TabIndex = 12;
            // 
            // btnProjectDirectory
            // 
            this.btnProjectDirectory.Location = new System.Drawing.Point(516, 44);
            this.btnProjectDirectory.Name = "btnProjectDirectory";
            this.btnProjectDirectory.Size = new System.Drawing.Size(37, 23);
            this.btnProjectDirectory.TabIndex = 11;
            this.btnProjectDirectory.Text = "...";
            this.btnProjectDirectory.UseVisualStyleBackColor = true;
            this.btnProjectDirectory.Click += new System.EventHandler(this.btnProjectDirectory_Click);
            // 
            // lblPathTemplateExcel
            // 
            this.lblPathTemplateExcel.AutoSize = true;
            this.lblPathTemplateExcel.Location = new System.Drawing.Point(13, 129);
            this.lblPathTemplateExcel.Name = "lblPathTemplateExcel";
            this.lblPathTemplateExcel.Size = new System.Drawing.Size(188, 13);
            this.lblPathTemplateExcel.TabIndex = 10;
            this.lblPathTemplateExcel.Text = "Diretório Template do Relatório (Excel)";
            // 
            // txtDirectoryTemplateExcel
            // 
            this.txtDirectoryTemplateExcel.Location = new System.Drawing.Point(16, 145);
            this.txtDirectoryTemplateExcel.Name = "txtDirectoryTemplateExcel";
            this.txtDirectoryTemplateExcel.Size = new System.Drawing.Size(494, 20);
            this.txtDirectoryTemplateExcel.TabIndex = 4;
            // 
            // pbGeneratorReport
            // 
            this.pbGeneratorReport.Location = new System.Drawing.Point(12, 210);
            this.pbGeneratorReport.Name = "pbGeneratorReport";
            this.pbGeneratorReport.Size = new System.Drawing.Size(559, 19);
            this.pbGeneratorReport.TabIndex = 10;
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Location = new System.Drawing.Point(12, 297);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(0, 13);
            this.lblInformation.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancel.Location = new System.Drawing.Point(381, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 33);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormReportGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(580, 281);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.pbGeneratorReport);
            this.Controls.Add(this.gbConfiguration);
            this.Controls.Add(this.btnReportGenerator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormReportGenerator";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extração Code Analysis";
            this.Load += new System.EventHandler(this.FormReportGenerator_Load);
            this.gbConfiguration.ResumeLayout(false);
            this.gbConfiguration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReportGenerator;
        private System.Windows.Forms.TextBox txtDirectoryRuleSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbConfiguration;
        private System.Windows.Forms.Label lblPathTemplateExcel;
        private System.Windows.Forms.TextBox txtDirectoryTemplateExcel;
        private System.Windows.Forms.ProgressBar pbGeneratorReport;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtProjectDirectory;
        private System.Windows.Forms.Button btnProjectDirectory;
        private System.Windows.Forms.Label lblDirectory;
        private System.Windows.Forms.Button btnExcelDirectory;
        private System.Windows.Forms.Button btnRuleSetDirectory;
    }
}

