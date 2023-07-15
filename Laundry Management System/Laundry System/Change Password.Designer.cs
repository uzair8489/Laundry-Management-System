namespace Laundry_System
{
    partial class Change_Password
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtold = new MetroFramework.Controls.MetroTextBox();
            this.txtnew = new MetroFramework.Controls.MetroTextBox();
            this.txtconfm = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(212)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 43);
            this.panel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "CHANGE PASSWORD";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(212)))));
            this.button3.BackgroundImage = global::Laundry_System.Properties.Resources._21118240861541068760_128;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI Black", 7.2F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(212)))));
            this.button3.Location = new System.Drawing.Point(409, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 31);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 8.7F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(26, 199);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(389, 38);
            this.button2.TabIndex = 34;
            this.button2.Text = "SAVE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtold
            // 
            // 
            // 
            // 
            this.txtold.CustomButton.Image = null;
            this.txtold.CustomButton.Location = new System.Drawing.Point(359, 2);
            this.txtold.CustomButton.Name = "";
            this.txtold.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtold.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtold.CustomButton.TabIndex = 1;
            this.txtold.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtold.CustomButton.UseSelectable = true;
            this.txtold.CustomButton.Visible = false;
            this.txtold.DisplayIcon = true;
            this.txtold.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtold.Icon = global::Laundry_System.Properties.Resources.icons8_password_23;
            this.txtold.Lines = new string[0];
            this.txtold.Location = new System.Drawing.Point(26, 85);
            this.txtold.MaxLength = 32767;
            this.txtold.Name = "txtold";
            this.txtold.PasswordChar = '●';
            this.txtold.PromptText = "Old Password";
            this.txtold.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtold.SelectedText = "";
            this.txtold.SelectionLength = 0;
            this.txtold.SelectionStart = 0;
            this.txtold.ShortcutsEnabled = true;
            this.txtold.Size = new System.Drawing.Size(389, 32);
            this.txtold.TabIndex = 35;
            this.txtold.UseSelectable = true;
            this.txtold.UseSystemPasswordChar = true;
            this.txtold.WaterMark = "Old Password";
            this.txtold.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtold.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtnew
            // 
            // 
            // 
            // 
            this.txtnew.CustomButton.Image = null;
            this.txtnew.CustomButton.Location = new System.Drawing.Point(359, 2);
            this.txtnew.CustomButton.Name = "";
            this.txtnew.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtnew.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtnew.CustomButton.TabIndex = 1;
            this.txtnew.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtnew.CustomButton.UseSelectable = true;
            this.txtnew.CustomButton.Visible = false;
            this.txtnew.DisplayIcon = true;
            this.txtnew.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtnew.Icon = global::Laundry_System.Properties.Resources.icons8_password_23;
            this.txtnew.Lines = new string[0];
            this.txtnew.Location = new System.Drawing.Point(26, 123);
            this.txtnew.MaxLength = 32767;
            this.txtnew.Name = "txtnew";
            this.txtnew.PasswordChar = '●';
            this.txtnew.PromptText = "New Password";
            this.txtnew.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtnew.SelectedText = "";
            this.txtnew.SelectionLength = 0;
            this.txtnew.SelectionStart = 0;
            this.txtnew.ShortcutsEnabled = true;
            this.txtnew.Size = new System.Drawing.Size(389, 32);
            this.txtnew.TabIndex = 36;
            this.txtnew.UseSelectable = true;
            this.txtnew.UseSystemPasswordChar = true;
            this.txtnew.WaterMark = "New Password";
            this.txtnew.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtnew.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtconfm
            // 
            // 
            // 
            // 
            this.txtconfm.CustomButton.Image = null;
            this.txtconfm.CustomButton.Location = new System.Drawing.Point(359, 2);
            this.txtconfm.CustomButton.Name = "";
            this.txtconfm.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtconfm.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtconfm.CustomButton.TabIndex = 1;
            this.txtconfm.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtconfm.CustomButton.UseSelectable = true;
            this.txtconfm.CustomButton.Visible = false;
            this.txtconfm.DisplayIcon = true;
            this.txtconfm.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtconfm.Icon = global::Laundry_System.Properties.Resources.icons8_password_23;
            this.txtconfm.Lines = new string[0];
            this.txtconfm.Location = new System.Drawing.Point(26, 161);
            this.txtconfm.MaxLength = 32767;
            this.txtconfm.Name = "txtconfm";
            this.txtconfm.PasswordChar = '●';
            this.txtconfm.PromptText = "Confirm New Password";
            this.txtconfm.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtconfm.SelectedText = "";
            this.txtconfm.SelectionLength = 0;
            this.txtconfm.SelectionStart = 0;
            this.txtconfm.ShortcutsEnabled = true;
            this.txtconfm.Size = new System.Drawing.Size(389, 32);
            this.txtconfm.TabIndex = 37;
            this.txtconfm.UseSelectable = true;
            this.txtconfm.UseSystemPasswordChar = true;
            this.txtconfm.WaterMark = "Confirm New Password";
            this.txtconfm.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtconfm.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Change_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 269);
            this.ControlBox = false;
            this.Controls.Add(this.txtconfm);
            this.Controls.Add(this.txtnew);
            this.Controls.Add(this.txtold);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Change_Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button button2;
        private MetroFramework.Controls.MetroTextBox txtold;
        private MetroFramework.Controls.MetroTextBox txtnew;
        private MetroFramework.Controls.MetroTextBox txtconfm;
    }
}