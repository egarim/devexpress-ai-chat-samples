using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;

namespace DevExpress.AI.Samples.WinBlazor {
    partial class SkChat
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
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.textInput = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.blazorWebView1 = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textInput.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 54.53F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5.47F)});
            this.tablePanel1.Controls.Add(this.blazorWebView1);
            this.tablePanel1.Controls.Add(this.textInput);
            this.tablePanel1.Controls.Add(this.simpleButton1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 760.5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1827, 894);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // textInput
            // 
            this.tablePanel1.SetColumn(this.textInput, 0);
            this.textInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textInput.Location = new System.Drawing.Point(26, 785);
            this.textInput.Name = "textInput";
            this.textInput.Properties.NullText = "Ask AI Assistant...";
            this.textInput.Properties.NullValuePrompt = "Ask AI Assistant...";
            this.textInput.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.tablePanel1.SetRow(this.textInput, 1);
            this.textInput.Size = new System.Drawing.Size(1612, 83);
            this.textInput.TabIndex = 1;
            this.textInput.KeyDown += textEdit1_KeyDown;
            this.textInput.KeyUp += textEdit1_KeyUp;
            // 
            // simpleButton1
            // 
            this.tablePanel1.SetColumn(this.simpleButton1, 1);
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButton1.Location = new System.Drawing.Point(1646, 785);
            this.simpleButton1.Name = "simpleButton1";
            this.tablePanel1.SetRow(this.simpleButton1, 1);
            this.simpleButton1.Size = new System.Drawing.Size(155, 83);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Send";
            this.simpleButton1.Click += SimpleButton1_Click;
            // 
            // blazorWebView1
            // 
            this.tablePanel1.SetColumn(this.blazorWebView1, 0);
            this.tablePanel1.SetColumnSpan(this.blazorWebView1, 2);
            this.blazorWebView1.Dock = DockStyle.Fill;
            this.blazorWebView1.Location = new Point(12, 12);
            this.blazorWebView1.Name = "blazorWebView1";
            this.tablePanel1.SetRow(this.blazorWebView1, 0);
            this.blazorWebView1.Size = new Size(776, 386);
            this.blazorWebView1.StartPath = "/";
            this.blazorWebView1.TabIndex = 2;
            this.blazorWebView1.Text = "blazorWebView1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1827, 894);
            this.Controls.Add(this.tablePanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textInput.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.MemoEdit textInput;
        private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorWebView1;
    }
}
