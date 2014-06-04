namespace WeiboDesktopDotNet4
{
	partial class frmChat
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChat));
			this.imgProfile = new System.Windows.Forms.PictureBox();
			this.lblScreenName = new System.Windows.Forms.Label();
			this.txtStatusBody = new System.Windows.Forms.TextBox();
			this.lblUserStatus = new System.Windows.Forms.Label();
			this.btnInsertPicture = new System.Windows.Forms.Button();
			this.btnPublish = new System.Windows.Forms.Button();
			this.lblCharCount = new System.Windows.Forms.Label();
			this.wbStatuses = new System.Windows.Forms.WebBrowser();
			this.lblDesc = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.imgProfile)).BeginInit();
			this.SuspendLayout();
			// 
			// imgProfile
			// 
			this.imgProfile.BackColor = System.Drawing.Color.Transparent;
			this.imgProfile.BackgroundImage = global::WeiboDesktopDotNet4.Properties.Resources.faceborder;
			this.imgProfile.ErrorImage = global::WeiboDesktopDotNet4.Properties.Resources.faceloading;
			this.imgProfile.ImageLocation = "";
			this.imgProfile.InitialImage = global::WeiboDesktopDotNet4.Properties.Resources.faceloading;
			this.imgProfile.Location = new System.Drawing.Point(5, 5);
			this.imgProfile.Margin = new System.Windows.Forms.Padding(5);
			this.imgProfile.Name = "imgProfile";
			this.imgProfile.Size = new System.Drawing.Size(60, 60);
			this.imgProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imgProfile.TabIndex = 0;
			this.imgProfile.TabStop = false;
			// 
			// lblScreenName
			// 
			this.lblScreenName.AutoSize = true;
			this.lblScreenName.BackColor = System.Drawing.Color.Transparent;
			this.lblScreenName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
			this.lblScreenName.Location = new System.Drawing.Point(70, 5);
			this.lblScreenName.Margin = new System.Windows.Forms.Padding(0);
			this.lblScreenName.Name = "lblScreenName";
			this.lblScreenName.Size = new System.Drawing.Size(63, 19);
			this.lblScreenName.TabIndex = 1;
			this.lblScreenName.Text = "加载中...";
			// 
			// txtStatusBody
			// 
			this.txtStatusBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStatusBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtStatusBody.Location = new System.Drawing.Point(5, 73);
			this.txtStatusBody.MaxLength = 140;
			this.txtStatusBody.Multiline = true;
			this.txtStatusBody.Name = "txtStatusBody";
			this.txtStatusBody.Size = new System.Drawing.Size(294, 80);
			this.txtStatusBody.TabIndex = 2;
			this.txtStatusBody.TextChanged += new System.EventHandler(this.txtStatusBody_TextChanged);
			// 
			// lblUserStatus
			// 
			this.lblUserStatus.AutoSize = true;
			this.lblUserStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblUserStatus.Location = new System.Drawing.Point(73, 48);
			this.lblUserStatus.Name = "lblUserStatus";
			this.lblUserStatus.Size = new System.Drawing.Size(133, 17);
			this.lblUserStatus.TabIndex = 3;
			this.lblUserStatus.Text = "粉丝(0) 关注(0) 微博(0)";
			// 
			// btnInsertPicture
			// 
			this.btnInsertPicture.Image = global::WeiboDesktopDotNet4.Properties.Resources.picture;
			this.btnInsertPicture.Location = new System.Drawing.Point(5, 159);
			this.btnInsertPicture.Name = "btnInsertPicture";
			this.btnInsertPicture.Size = new System.Drawing.Size(26, 26);
			this.btnInsertPicture.TabIndex = 4;
			this.btnInsertPicture.TabStop = false;
			this.btnInsertPicture.UseVisualStyleBackColor = true;
			this.btnInsertPicture.Click += new System.EventHandler(this.btnInsertPicture_Click);
			// 
			// btnPublish
			// 
			this.btnPublish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPublish.Image = global::WeiboDesktopDotNet4.Properties.Resources.comment;
			this.btnPublish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPublish.Location = new System.Drawing.Point(214, 159);
			this.btnPublish.Name = "btnPublish";
			this.btnPublish.Size = new System.Drawing.Size(85, 26);
			this.btnPublish.TabIndex = 5;
			this.btnPublish.Text = "发布微博";
			this.btnPublish.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnPublish.UseVisualStyleBackColor = true;
			this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
			// 
			// lblCharCount
			// 
			this.lblCharCount.AutoSize = true;
			this.lblCharCount.BackColor = System.Drawing.Color.Transparent;
			this.lblCharCount.ForeColor = System.Drawing.Color.White;
			this.lblCharCount.Location = new System.Drawing.Point(37, 164);
			this.lblCharCount.Name = "lblCharCount";
			this.lblCharCount.Size = new System.Drawing.Size(113, 17);
			this.lblCharCount.TabIndex = 6;
			this.lblCharCount.Text = "还可以输入140个字";
			// 
			// wbStatuses
			// 
			this.wbStatuses.AllowWebBrowserDrop = false;
			this.wbStatuses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wbStatuses.IsWebBrowserContextMenuEnabled = false;
			this.wbStatuses.Location = new System.Drawing.Point(0, 195);
			this.wbStatuses.Margin = new System.Windows.Forms.Padding(0);
			this.wbStatuses.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbStatuses.Name = "wbStatuses";
			this.wbStatuses.Size = new System.Drawing.Size(304, 406);
			this.wbStatuses.TabIndex = 7;
			this.wbStatuses.Url = new System.Uri("", System.UriKind.Relative);
			this.wbStatuses.WebBrowserShortcutsEnabled = false;
			// 
			// lblDesc
			// 
			this.lblDesc.AutoSize = true;
			this.lblDesc.BackColor = System.Drawing.Color.Transparent;
			this.lblDesc.ForeColor = System.Drawing.Color.DimGray;
			this.lblDesc.Location = new System.Drawing.Point(73, 24);
			this.lblDesc.Name = "lblDesc";
			this.lblDesc.Size = new System.Drawing.Size(53, 17);
			this.lblDesc.TabIndex = 8;
			this.lblDesc.Text = "加载中...";
			// 
			// frmChat
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = global::WeiboDesktopDotNet4.Properties.Resources.toolpanel;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(304, 602);
			this.Controls.Add(this.lblDesc);
			this.Controls.Add(this.wbStatuses);
			this.Controls.Add(this.lblCharCount);
			this.Controls.Add(this.btnPublish);
			this.Controls.Add(this.btnInsertPicture);
			this.Controls.Add(this.lblUserStatus);
			this.Controls.Add(this.txtStatusBody);
			this.Controls.Add(this.lblScreenName);
			this.Controls.Add(this.imgProfile);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(400, 10000);
			this.MinimumSize = new System.Drawing.Size(320, 640);
			this.Name = "frmChat";
			this.Text = "微博桌面2.0";
			this.Load += new System.EventHandler(this.frmChat_Load);
			((System.ComponentModel.ISupportInitialize)(this.imgProfile)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox imgProfile;
		private System.Windows.Forms.Label lblScreenName;
		private System.Windows.Forms.TextBox txtStatusBody;
		private System.Windows.Forms.Label lblUserStatus;
		private System.Windows.Forms.Button btnInsertPicture;
		private System.Windows.Forms.Button btnPublish;
		private System.Windows.Forms.Label lblCharCount;
		private System.Windows.Forms.WebBrowser wbStatuses;
		private System.Windows.Forms.Label lblDesc;
	}
}