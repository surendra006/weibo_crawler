using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WeiboDesktopDotNet4
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
						Sina.API.Entity.Statuses.Update(status, 0, 0, null);
					}
					catch (Exception ex)
					{
						this.UIInvoke(() => {
							MessageBox.Show(this, ex.Message, "哟？！出错了!", MessageBoxButtons.OK, MessageBoxIcon.Information);
						});
						
					}
				}
				else
				{
					try
					{
						Sina.API.Entity.Statuses.Upload(status, imgBuff);
					}
					catch (Exception ex)
					{
						this.UIInvoke(() =>
						{
							MessageBox.Show(this, ex.Message, "哟？！出错了!", MessageBoxButtons.OK, MessageBoxIcon.Information);
						});
					}
					finally
					{
						imgBuff = null;

					}
				}

				this.UIInvoke(()=>{
					btnPublish.Enabled = true;
					btnPublish.Text = "发布微博";
					txtStatusBody.ReadOnly = false;
					txtStatusBody.Text = string.Empty;
					btnInsertPicture.Enabled = true;
					LoadFriendTimeline();
				});

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
				string userID = Sina.API.Entity.Account.GetUID();
				NetDimension.Weibo.Entities.user.Entity userInfo = Sina.API.Entity.Users.Show(userID);
				//UIUpdateUserInfo(userInfo); //2.0才需要这个东西

				this.UIInvoke(() => {
					lblScreenName.Text = userInfo.ScreenName;
					lblDesc.Text = userInfo.Description;
					imgProfile.ImageLocation = userInfo.ProfileImageUrl;
					lblUserStatus.Text = string.Format("关注({0}) 粉丝({1}) 微博({2})", userInfo.FriendsCount, userInfo.FollowersCount, userInfo.StatusesCount);
				});

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
				var json = Sina.API.Entity.Statuses.FriendsTimeline(count:20);
				if (json.Statuses != null)
				{
					foreach (var status in json.Statuses)
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

				var html = htmlPattern.Replace("<!--StatusesList-->", statusBuilder.ToString());

				//对比2.0，这里是不需要单独有个UI操作的方法的。
				this.UIInvoke(() => {
					wbStatuses.DocumentText = html;
				});
			}));

			thLoad.IsBackground = true;
			thLoad.Start();

		}


	}
}
