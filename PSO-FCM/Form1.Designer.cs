namespace PSO_FCM
{
    partial class Form1
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
            this.btn_Create1000X = new System.Windows.Forms.Button();
            this.cb_data = new System.Windows.Forms.ComboBox();
            this.btn_Load = new System.Windows.Forms.Button();
            this.btn_FCM = new System.Windows.Forms.Button();
            this.btn_PSOFCM = new System.Windows.Forms.Button();
            this.btn_PSOFCMRR = new System.Windows.Forms.Button();
            this.btn_Pso10000 = new System.Windows.Forms.Button();
            this.btn_AutoAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Create1000X
            // 
            this.btn_Create1000X.Location = new System.Drawing.Point(12, 12);
            this.btn_Create1000X.Name = "btn_Create1000X";
            this.btn_Create1000X.Size = new System.Drawing.Size(83, 32);
            this.btn_Create1000X.TabIndex = 0;
            this.btn_Create1000X.Text = "Create 1000X";
            this.btn_Create1000X.UseVisualStyleBackColor = true;
            this.btn_Create1000X.Click += new System.EventHandler(this.btn_Create1000X_Click);
            // 
            // cb_data
            // 
            this.cb_data.FormattingEnabled = true;
            this.cb_data.Location = new System.Drawing.Point(133, 19);
            this.cb_data.Name = "cb_data";
            this.cb_data.Size = new System.Drawing.Size(121, 21);
            this.cb_data.TabIndex = 1;
            // 
            // btn_Load
            // 
            this.btn_Load.Location = new System.Drawing.Point(12, 50);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(83, 32);
            this.btn_Load.TabIndex = 2;
            this.btn_Load.Text = "LoadData";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // btn_FCM
            // 
            this.btn_FCM.Enabled = false;
            this.btn_FCM.Location = new System.Drawing.Point(12, 88);
            this.btn_FCM.Name = "btn_FCM";
            this.btn_FCM.Size = new System.Drawing.Size(83, 32);
            this.btn_FCM.TabIndex = 3;
            this.btn_FCM.Text = "FCM";
            this.btn_FCM.UseVisualStyleBackColor = true;
            this.btn_FCM.Click += new System.EventHandler(this.btn_FCM_Click);
            // 
            // btn_PSOFCM
            // 
            this.btn_PSOFCM.Enabled = false;
            this.btn_PSOFCM.Location = new System.Drawing.Point(101, 88);
            this.btn_PSOFCM.Name = "btn_PSOFCM";
            this.btn_PSOFCM.Size = new System.Drawing.Size(83, 32);
            this.btn_PSOFCM.TabIndex = 4;
            this.btn_PSOFCM.Text = "PSOFCM";
            this.btn_PSOFCM.UseVisualStyleBackColor = true;
            this.btn_PSOFCM.Click += new System.EventHandler(this.btn_PSOFCM_Click);
            // 
            // btn_PSOFCMRR
            // 
            this.btn_PSOFCMRR.Enabled = false;
            this.btn_PSOFCMRR.Location = new System.Drawing.Point(189, 88);
            this.btn_PSOFCMRR.Name = "btn_PSOFCMRR";
            this.btn_PSOFCMRR.Size = new System.Drawing.Size(83, 32);
            this.btn_PSOFCMRR.TabIndex = 4;
            this.btn_PSOFCMRR.Text = "PSOFCMRR";
            this.btn_PSOFCMRR.UseVisualStyleBackColor = true;
            this.btn_PSOFCMRR.Click += new System.EventHandler(this.btn_PSOFCMRR_Click);
            // 
            // btn_Pso10000
            // 
            this.btn_Pso10000.Enabled = false;
            this.btn_Pso10000.Location = new System.Drawing.Point(12, 126);
            this.btn_Pso10000.Name = "btn_Pso10000";
            this.btn_Pso10000.Size = new System.Drawing.Size(83, 32);
            this.btn_Pso10000.TabIndex = 5;
            this.btn_Pso10000.Text = "PSO 100000";
            this.btn_Pso10000.UseVisualStyleBackColor = true;
            this.btn_Pso10000.Click += new System.EventHandler(this.btn_Pso10000_Click);
            // 
            // btn_AutoAll
            // 
            this.btn_AutoAll.Location = new System.Drawing.Point(12, 182);
            this.btn_AutoAll.Name = "btn_AutoAll";
            this.btn_AutoAll.Size = new System.Drawing.Size(83, 32);
            this.btn_AutoAll.TabIndex = 6;
            this.btn_AutoAll.Text = "Auto All";
            this.btn_AutoAll.UseVisualStyleBackColor = true;
            this.btn_AutoAll.Click += new System.EventHandler(this.btn_AutoAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btn_AutoAll);
            this.Controls.Add(this.btn_Pso10000);
            this.Controls.Add(this.btn_PSOFCMRR);
            this.Controls.Add(this.btn_PSOFCM);
            this.Controls.Add(this.btn_FCM);
            this.Controls.Add(this.btn_Load);
            this.Controls.Add(this.cb_data);
            this.Controls.Add(this.btn_Create1000X);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Create1000X;
        private System.Windows.Forms.ComboBox cb_data;
        private System.Windows.Forms.Button btn_Load;
        private System.Windows.Forms.Button btn_FCM;
        private System.Windows.Forms.Button btn_PSOFCM;
        private System.Windows.Forms.Button btn_PSOFCMRR;
        private System.Windows.Forms.Button btn_Pso10000;
        private System.Windows.Forms.Button btn_AutoAll;
    }
}

