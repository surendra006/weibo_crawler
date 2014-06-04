using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WeiboDesktopDotNet4
{
	public partial class frmLogin : Form
	{
		private NetDimension.Weibo.OAuth oauth;


		public frmLogin(NetDimension.Weibo.OAuth oauth)
		{
			InitializeComponent();
			this.oauth = oauth;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				int CS_DROPSHADOW = 0x20000;
				CreateParams parameters = base.CreateParams;
				parameters.ClassStyle |= CS_DROPSHADOW;
				return parameters;
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x0201) //鼠标左键按下去的消息
			{
				m.Msg = 0x00A1;//更改消息为非客户区按下鼠标
				m.LParam = IntPtr.Zero;//默认值
				m.WParam = new IntPtr(2);//鼠标放在标题栏内
			}
			base.WndProc(ref m);
		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Properties.Settings.Default.Passport))
			{
				txtPassport.Text = Properties.Settings.Default.Passport;
				txtPasswd.Text = Properties.Settings.Default.Password;
				chkRemember.Checked = true;
			}

		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtPassport.Text))
			{
				MessageBox.Show(this, "请输入微博账号。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (string.IsNullOrEmpty(txtPasswd.Text))
			{
				MessageBox.Show(this, "请输入登录密码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}


			string passport = txtPassport.Text;
			string passwd = txtPasswd.Text;

			mask.Visible = true;

			//好吧，上一版的SDK在网络环境差的情况下经常会卡着不动，这次的我们来把操作界面线程和操作线程分开吧
			//对比下2.0和4.0的写法，你会发现4.0真的很方便
			Thread thLogin = new Thread(()=>
			{

				try
				{
					bool result = oauth.ClientLogin(passport, passwd);
					var tr = oauth.VerifierAccessToken();

					if (tr == NetDimension.Weibo.TokenResult.Success)
					{
						UILoginComplete(result, "登录成功。");
					}
					else
					{
						UILoginComplete(result, tr.ToString());
					}

				}
				catch (Exception ex)
				{
					UILoginComplete(false, ex.Message);
				}



			});

			thLogin.Start();

		}

		private void UILoginComplete(bool success, string msg)
		{
			//自己写的时候不要来找我问“哎呀，怎么我的没有UIInvoke啊”，因为UIInvoke是我自己写的。不懂的自己谷歌“扩展方法”。
			this.UIInvoke(() => {
				mask.Visible = false;
				if (success)
				{
					if (chkRemember.Checked)
					{
						Properties.Settings.Default.Passport = txtPassport.Text;
						Properties.Settings.Default.Password = txtPasswd.Text;
					}
					else
					{
						Properties.Settings.Default.Passport = "";
						Properties.Settings.Default.Password = "";
					}

					Properties.Settings.Default.Save();
					this.DialogResult = System.Windows.Forms.DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageBox.Show(this, "没有登录成功，请确保账号密码正确。\r\n错误提示：" + msg + "\r\n\r\n当然还要确定Settings里你的AppKey和回调地址是对的。不懂看视频去，不解释。如果出现未审核应用人数达到上限那就去新浪后台把测试账号的UID填到网站测试账号栏目里。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			});
			
		}


		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}
}
