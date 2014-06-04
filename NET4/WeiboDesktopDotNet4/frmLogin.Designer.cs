namespace WeiboDesktopDotNet4
{
	partial class frmLogin
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPasswd = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPassport = new System.Windows.Forms.TextBox();
			this.chkRemember = new System.Windows.Forms.CheckBox();
			this.mask = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.mask)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(214, 176);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 26);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLogin.Location = new System.Drawing.Point(133, 176);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 26);
			this.btnLogin.TabIndex = 10;
			this.btnLogin.Text = "登录";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(32, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 9;
			this.label2.Text = "登录密码";
			// 
			// txtPasswd
			// 
			this.txtPasswd.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtPasswd.Location = new System.Drawing.Point(35, 102);
			this.txtPasswd.Name = "txtPasswd";
			this.txtPasswd.Size = new System.Drawing.Size(254, 27);
			this.txtPasswd.TabIndex = 8;
			this.txtPasswd.UseSystemPasswordChar = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(32, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 7;
			this.label1.Text = "微博账号";
			// 
			// txtPassport
			// 
			this.txtPassport.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtPassport.Location = new System.Drawing.Point(35, 46);
			this.txtPassport.Name = "txtPassport";
			this.txtPassport.Size = new System.Drawing.Size(254, 27);
			this.txtPassport.TabIndex = 6;
			// 
			// chkRemember
			// 
			this.chkRemember.AutoSize = true;
			this.chkRemember.BackColor = System.Drawing.Color.Transparent;
			this.chkRemember.Location = new System.Drawing.Point(35, 135);
			this.chkRemember.Name = "chkRemember";
			this.chkRemember.Size = new System.Drawing.Size(135, 21);
			this.chkRemember.TabIndex = 12;
			this.chkRemember.Text = "记住我的账号和密码";
			this.chkRemember.UseVisualStyleBackColor = true;
			// 
			// mask
			// 
			this.mask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.mask.BackColor = System.Drawing.Color.Transparent;
			this.mask.Image = global::WeiboDesktopDotNet4.Properties.Resources.loading;
			this.mask.Location = new System.Drawing.Point(0, 0);
			this.mask.Margin = new System.Windows.Forms.Padding(0);
			this.mask.Name = "mask";
			this.mask.Size = new System.Drawing.Size(320, 220);
			this.mask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.mask.TabIndex = 13;
			this.mask.TabStop = false;
			this.mask.Visible = false;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.ForeColor = System.Drawing.Color.DimGray;
			this.label3.Location = new System.Drawing.Point(12, 138);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(296, 35);
			this.label3.TabIndex = 14;
			this.label3.Text = "正在登录，请稍候...";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// frmLogin
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::WeiboDesktopDotNet4.Properties.Resources.bg;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(320, 220);

			this.Controls.Add(this.mask);
			this.Controls.Add(this.chkRemember);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtPasswd);
			this.Controls.Add(this.label1);
			mask.Controls.Add(this.label3);
			this.Controls.Add(this.txtPassport);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmLogin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "微博桌面2.0";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmLogin_Load);
			((System.ComponentModel.ISupportInitialize)(this.mask)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPasswd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPassport;
		private System.Windows.Forms.CheckBox chkRemember;
		private System.Windows.Forms.PictureBox mask;
		private System.Windows.Forms.Label label3;
	}
}

