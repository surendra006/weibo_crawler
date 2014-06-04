using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NetDimension.Weibo;
using System.IO;

namespace WeiboDesktopDotNet2
{
	public partial class frmChat : Form
	{
		#region 微博列表的模板
		const string htmlPattern = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
	<title></title>
	<style type=""text/css"">
		html, body {
			font-size: 12px;
			cursor: default;
			padding: 5px;
			margin: 0;
			font-family:微软雅黑,Tahoma;
		}

		div.status {
			padding-left: 60px;
			position: relative;
			margin-bottom: 10px;
			min-height:80px;
			_height:80px;
		}

			div.status p {
				margin: 0 0 5px 0;
				line-height: 1.5;
				padding: 0;
			}

				div.status p span.name {
					color: #369;
				}

				div.status p.status-content {
					color: #333;
				}

				div.status p.status-count {
					color:#999;
				}

			div.status .face {
				position: absolute;
				left: 0;
				top: 0;
			}

			div.status div.repost {
				border: solid 1px #ACD;
				background: #F0FAFF;
				padding: 10px 10px 0 10px;
			}

		div.repost p.repost-content {
			color: #666 !important;
		}
	</style>
</head>
<body>
<!--StatusesList-->
</body>
</html>";
		const string imageParttern = @"<img src=""{0}"" alt=""图片"" class=""inner-pic"" />";
		const string statusPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-content""><span class=""name"">{1}</span>：{2}</p>
		{3}
		<p class=""status-count"">转发({4}) 评论({5})</p>
	</div>
";
		const string repostPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-content""><span class=""name"">{1}</span>：{2}</p>
		<div class=""repost"">
			<p class=""repost-cotent""><span class=""name"">@{3}</span>：{4}</p>
			{5}
			<p class=""status-count"">转发({6}) 评论({7})</p>
		</div>
		<p class=""status-count"">转发({8}) 评论({9})</p>
	</div>
";
		#endregion

		private NetDimension.Weibo.OAuth oauth;
		private NetDimension.Weibo.Client Sina;
		private string UserID = string.Empty;

		private byte[] imgBuff = null;

		public frmChat(NetDimension.Weibo.OAuth oauth)
		{
			InitializeComponent();
			this.oauth = oauth;
			Sina = new NetDimension.Weibo.Client(oauth);
		}

		private void frmChat_Load(object sender, EventArgs e)
		{
			LoadUserInformation();
			LoadFriendTimeline();
		}

		private void txtStatusBody_TextChanged(object sender, EventArgs e)
		{
			int remainCount = 140 - txtStatusBody.TextLength;
			lblCharCount.Text = string.Format("还可以输入{0}个字", remainCount);
		}

		private void btnPublish_Click(object sender, EventArgs e)
		{

			if (txtStatusBody.TextLength == 0)
			{
				MessageBox.Show(this, "说点什么新鲜事儿呗。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			string status = txtStatusBody.Text;
			PublishStatus(status);
		}


		private void btnInsertPicture_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				FileInfo imageFile = new FileInfo(dlg.FileName);
				if (imageFile.Exists)
				{
					using (FileStream stream = imageFile.OpenRead())
					using (BinaryReader reader = new BinaryReader(stream))
					{
						try
						{
							stream.Seek(0, SeekOrigin.Begin);
							imgBuff = reader.ReadBytes((int)stream.Length);
						}
						finally
						{
							reader.Close();
							stream.Close();
						}
					}

					btnInsertPicture.Enabled = false;

					MessageBox.Show(this, "图片已附加，发条微博看看吧～", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}


		/// <summary>
		/// 线程方法：发微博
		/// </summary>
		/// <param name="status"></param>
		private void PublishStatus(string status)
		{
			btnPublish.Enabled = false;
			btnPublish.Text = "请稍等..";
			txtStatusBody.ReadOnly = true;

			Thread t = new Thread(new ThreadStart(delegate()
			{
				if (imgBuff == null)
				{
					try
					{
						Sina.API.Statuses.Update(status, 0, 0, null);
					}
					catch (Exception ex)
					{
						UIShowInfoMsgBox("哟？！出错了!", ex.Message);
					}
				}
				else
				{
					try
					{
						Sina.API.Statuses.Upload(status, imgBuff, 0, 0, null);
					}
					catch (Exception ex)
					{
						UIShowInfoMsgBox("哟？！出错了!", ex.Message);
					}
					finally
					{
						imgBuff = null;

					}
				}

				UIUpdateOperationInterface();

			}));
			t.IsBackground = true;
			t.Start();
		}

		/// <summary>
		/// 线程方法：加载用户信息
		/// </summary>
		private void LoadUserInformation()
		{
			Thread thUserInfo = new Thread(new ThreadStart(delegate()
			{
				string userID = Sina.API.Account.GetUID();
				NetDimension.Weibo.Entities.user.Entity userInfo = Sina.API.Users.Show(userID, null);
				UIUpdateUserInfo(userInfo);
			}));


			thUserInfo.IsBackground = true;
			thUserInfo.Start();
		}

		/// <summary>
		/// 线程方法：加载用户微博
		/// </summary>
		private void LoadFriendTimeline()
		{
			Thread thLoad = new Thread(new ThreadStart(delegate()
			{
				StringBuilder statusBuilder = new StringBuilder();
				NetDimension.Weibo.Entities.status.Collection json = Sina.API.Statuses.FriendsTimeline("0", "0", 20, 1, false, 0);
				if (json.Statuses != null)
				{
					foreach (NetDimension.Weibo.Entities.status.Entity status in json.Statuses)
					{
						if (status.User == null)
							continue;

						if (status.RetweetedStatus != null && status.RetweetedStatus.User != null)
						{
							statusBuilder.AppendFormat(repostPattern,
								status.User.ProfileImageUrl,
								status.User.ScreenName,
								status.Text,
								status.RetweetedStatus.User.ScreenName,
								status.RetweetedStatus.Text,
								string.IsNullOrEmpty(status.RetweetedStatus.ThumbnailPictureUrl) ? "" :
								string.Format(imageParttern, status.RetweetedStatus.ThumbnailPictureUrl),
								status.RetweetedStatus.RepostsCount,
								status.RetweetedStatus.CommentsCount,
								status.RepostsCount, status.CommentsCount);

						}
						else
						{
							statusBuilder.AppendFormat(statusPattern,
								status.User.ProfileImageUrl,
								status.User.ScreenName,
								status.Text,
								string.IsNullOrEmpty(status.ThumbnailPictureUrl) ? "" :
								string.Format(imageParttern, status.ThumbnailPictureUrl),
								status.RepostsCount, status.CommentsCount);
						}

					}
				}

				string html = htmlPattern.Replace("<!--StatusesList-->", statusBuilder.ToString());

				UIUpdateContent(html);
			}));

			thLoad.IsBackground = true;
			thLoad.Start();

		}

		#region 界面线程方法
		/// <summary>
		/// 刷新微博列表
		/// </summary>
		/// <param name="html"></param>
		private void UIUpdateContent(string html)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate()
				{
					UIUpdateContent(html);
				}));
			}
			else
			{
				wbStatuses.DocumentText = html;
			}
		}

		/// <summary>
		/// 刷新用户信息界面
		/// </summary>
		/// <param name="userInfo"></param>
		private void UIUpdateUserInfo(NetDimension.Weibo.Entities.user.Entity userInfo)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate()
				{
					UIUpdateUserInfo(userInfo);
				}));
			}
			else
			{

				lblScreenName.Text = userInfo.ScreenName;
				lblDesc.Text = userInfo.Description;
				imgProfile.ImageLocation = userInfo.ProfileImageUrl;
				lblUserStatus.Text = string.Format("关注({0}) 粉丝({1}) 微博({2})", userInfo.FriendsCount, userInfo.FollowersCount, userInfo.StatusesCount);
			}
		}

		/// <summary>
		/// 显示个MsgBox
		/// </summary>
		/// <param name="title"></param>
		/// <param name="txt"></param>
		private void UIShowInfoMsgBox(string title, string txt)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate()
				{
					UIShowInfoMsgBox(title, txt);
				}));
			}
			else
			{
				MessageBox.Show(this, txt, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 刷新操作界面
		/// </summary>
		private void UIUpdateOperationInterface()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate()
				{
					UIUpdateOperationInterface();
				}));
			}
			else
			{
				btnPublish.Enabled = true;
				btnPublish.Text = "发布微博";
				txtStatusBody.ReadOnly = false;
				txtStatusBody.Text = string.Empty;
				btnInsertPicture.Enabled = true;
				LoadFriendTimeline();
			}
		}
		#endregion

	}
}
