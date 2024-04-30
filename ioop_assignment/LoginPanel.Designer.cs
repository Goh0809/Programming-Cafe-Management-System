namespace ioop_assignment
{
    partial class LoginPanel
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
            this.login_welcome3 = new System.Windows.Forms.Label();
            this.login_welcome2 = new System.Windows.Forms.Label();
            this.login_welcome1 = new System.Windows.Forms.Label();
            this.login_apulogo = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.login_btnLogin = new System.Windows.Forms.Button();
            this.login_password_panel = new System.Windows.Forms.Panel();
            this.login_passwordbox = new System.Windows.Forms.TextBox();
            this.login_password_icon = new System.Windows.Forms.PictureBox();
            this.login_username_panel = new System.Windows.Forms.Panel();
            this.login_usernamebox = new System.Windows.Forms.TextBox();
            this.login_username_icon = new System.Windows.Forms.PictureBox();
            this.login_loginmsg = new System.Windows.Forms.Label();
            this.login_close = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_apulogo)).BeginInit();
            this.panel2.SuspendLayout();
            this.login_password_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_password_icon)).BeginInit();
            this.login_username_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_username_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel1.Controls.Add(this.login_welcome3);
            this.panel1.Controls.Add(this.login_welcome2);
            this.panel1.Controls.Add(this.login_welcome1);
            this.panel1.Controls.Add(this.login_apulogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 530);
            this.panel1.TabIndex = 0;
            // 
            // login_welcome3
            // 
            this.login_welcome3.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_welcome3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.login_welcome3.Location = new System.Drawing.Point(23, 350);
            this.login_welcome3.Name = "login_welcome3";
            this.login_welcome3.Size = new System.Drawing.Size(251, 28);
            this.login_welcome3.TabIndex = 3;
            this.login_welcome3.Text = "Management System";
            this.login_welcome3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // login_welcome2
            // 
            this.login_welcome2.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_welcome2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.login_welcome2.Location = new System.Drawing.Point(18, 316);
            this.login_welcome2.Name = "login_welcome2";
            this.login_welcome2.Size = new System.Drawing.Size(256, 34);
            this.login_welcome2.TabIndex = 2;
            this.login_welcome2.Text = "APU Programming Café ";
            this.login_welcome2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // login_welcome1
            // 
            this.login_welcome1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_welcome1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.login_welcome1.Location = new System.Drawing.Point(115, 277);
            this.login_welcome1.Name = "login_welcome1";
            this.login_welcome1.Size = new System.Drawing.Size(159, 29);
            this.login_welcome1.TabIndex = 1;
            this.login_welcome1.Text = "Welcome to";
            this.login_welcome1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // login_apulogo
            // 
            this.login_apulogo.Image = global::ioop_assignment.Properties.Resources.APU_Logo_Full_Size_removebg_preview;
            this.login_apulogo.Location = new System.Drawing.Point(75, 43);
            this.login_apulogo.Name = "login_apulogo";
            this.login_apulogo.Size = new System.Drawing.Size(142, 135);
            this.login_apulogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.login_apulogo.TabIndex = 0;
            this.login_apulogo.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.login_btnLogin);
            this.panel2.Controls.Add(this.login_password_panel);
            this.panel2.Controls.Add(this.login_username_panel);
            this.panel2.Controls.Add(this.login_loginmsg);
            this.panel2.Controls.Add(this.login_close);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.panel2.Location = new System.Drawing.Point(300, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(450, 530);
            this.panel2.TabIndex = 1;
            // 
            // login_btnLogin
            // 
            this.login_btnLogin.BackColor = System.Drawing.Color.LightSeaGreen;
            this.login_btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_btnLogin.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btnLogin.ForeColor = System.Drawing.Color.White;
            this.login_btnLogin.Location = new System.Drawing.Point(146, 315);
            this.login_btnLogin.Name = "login_btnLogin";
            this.login_btnLogin.Size = new System.Drawing.Size(148, 35);
            this.login_btnLogin.TabIndex = 4;
            this.login_btnLogin.Text = "LOGIN";
            this.login_btnLogin.UseVisualStyleBackColor = false;
            this.login_btnLogin.Click += new System.EventHandler(this.login_btnLogin_Click);
            // 
            // login_password_panel
            // 
            this.login_password_panel.BackColor = System.Drawing.Color.White;
            this.login_password_panel.Controls.Add(this.login_passwordbox);
            this.login_password_panel.Controls.Add(this.login_password_icon);
            this.login_password_panel.Location = new System.Drawing.Point(0, 235);
            this.login_password_panel.Name = "login_password_panel";
            this.login_password_panel.Size = new System.Drawing.Size(450, 45);
            this.login_password_panel.TabIndex = 3;
            // 
            // login_passwordbox
            // 
            this.login_passwordbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.login_passwordbox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_passwordbox.Location = new System.Drawing.Point(55, 10);
            this.login_passwordbox.Name = "login_passwordbox";
            this.login_passwordbox.Size = new System.Drawing.Size(370, 20);
            this.login_passwordbox.TabIndex = 2;
            this.login_passwordbox.UseSystemPasswordChar = true;
            this.login_passwordbox.Click += new System.EventHandler(this.login_passwordbox_Click);
            // 
            // login_password_icon
            // 
            this.login_password_icon.Image = global::ioop_assignment.Properties.Resources.padlock;
            this.login_password_icon.Location = new System.Drawing.Point(15, 11);
            this.login_password_icon.Name = "login_password_icon";
            this.login_password_icon.Size = new System.Drawing.Size(24, 24);
            this.login_password_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.login_password_icon.TabIndex = 0;
            this.login_password_icon.TabStop = false;
            this.login_password_icon.Click += new System.EventHandler(this.login_password_icon_Click);
            this.login_password_icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.login_password_icon_MouseDown);
            this.login_password_icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.login_password_icon_MouseUp);
            // 
            // login_username_panel
            // 
            this.login_username_panel.BackColor = System.Drawing.Color.White;
            this.login_username_panel.Controls.Add(this.login_usernamebox);
            this.login_username_panel.Controls.Add(this.login_username_icon);
            this.login_username_panel.Location = new System.Drawing.Point(0, 185);
            this.login_username_panel.Name = "login_username_panel";
            this.login_username_panel.Size = new System.Drawing.Size(450, 45);
            this.login_username_panel.TabIndex = 2;
            // 
            // login_usernamebox
            // 
            this.login_usernamebox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.login_usernamebox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_usernamebox.Location = new System.Drawing.Point(55, 10);
            this.login_usernamebox.Name = "login_usernamebox";
            this.login_usernamebox.Size = new System.Drawing.Size(370, 20);
            this.login_usernamebox.TabIndex = 1;
            this.login_usernamebox.Click += new System.EventHandler(this.login_usernamebox_Click);
            // 
            // login_username_icon
            // 
            this.login_username_icon.Image = global::ioop_assignment.Properties.Resources.user;
            this.login_username_icon.Location = new System.Drawing.Point(15, 11);
            this.login_username_icon.Name = "login_username_icon";
            this.login_username_icon.Size = new System.Drawing.Size(24, 24);
            this.login_username_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.login_username_icon.TabIndex = 0;
            this.login_username_icon.TabStop = false;
            // 
            // login_loginmsg
            // 
            this.login_loginmsg.AutoSize = true;
            this.login_loginmsg.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_loginmsg.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.login_loginmsg.Location = new System.Drawing.Point(38, 135);
            this.login_loginmsg.Name = "login_loginmsg";
            this.login_loginmsg.Size = new System.Drawing.Size(233, 25);
            this.login_loginmsg.TabIndex = 1;
            this.login_loginmsg.Text = "Login to your account";
            // 
            // login_close
            // 
            this.login_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.login_close.FlatAppearance.BorderSize = 0;
            this.login_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_close.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_close.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.login_close.Location = new System.Drawing.Point(410, 0);
            this.login_close.Name = "login_close";
            this.login_close.Size = new System.Drawing.Size(40, 40);
            this.login_close.TabIndex = 0;
            this.login_close.Text = "X";
            this.login_close.UseVisualStyleBackColor = true;
            this.login_close.Click += new System.EventHandler(this.login_close_Click);
            // 
            // LoginPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 530);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.login_apulogo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.login_password_panel.ResumeLayout(false);
            this.login_password_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_password_icon)).EndInit();
            this.login_username_panel.ResumeLayout(false);
            this.login_username_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_username_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox login_apulogo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label login_welcome3;
        private System.Windows.Forms.Label login_welcome2;
        private System.Windows.Forms.Label login_welcome1;
        private System.Windows.Forms.Button login_close;
        private System.Windows.Forms.Panel login_password_panel;
        private System.Windows.Forms.Panel login_username_panel;
        private System.Windows.Forms.PictureBox login_username_icon;
        private System.Windows.Forms.Label login_loginmsg;
        private System.Windows.Forms.PictureBox login_password_icon;
        private System.Windows.Forms.Button login_btnLogin;
        private System.Windows.Forms.TextBox login_passwordbox;
        private System.Windows.Forms.TextBox login_usernamebox;
    }
}

