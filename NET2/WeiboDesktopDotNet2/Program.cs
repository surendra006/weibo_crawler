using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NetDimension.Weibo;

namespace WeiboDesktopDotNet2
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			OAuth oauth = new OAuth(Properties.Settings.Default.AppKey, Properties.Settings.Default.AppSecret, Properties.Settings.Default.CallbackUrl);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			frmLogin LoginForm = new frmLogin(oauth);
			if (LoginForm.ShowDialog() == DialogResult.OK)
			{
				Application.Run(new frmChat(oauth));
			}
		}
	}
}
