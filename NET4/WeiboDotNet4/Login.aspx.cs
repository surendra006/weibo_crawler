﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetDimension.Web;//这个命名空间的代码在App_Code里面，不要再跑了群里找我要了哈
using NetDimension.Weibo;
using System.Configuration;
using System.Text;


public partial class Login : System.Web.UI.Page
{
	Cookie cookie = new Cookie("WeiboDemo", 24, TimeUnit.Hour);

	Client Sina = null;
	OAuth oauth = new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], ConfigurationManager.AppSettings["CallbackUrl"]);

    protected void Page_Load(object sender, EventArgs e)
    {
		Sina = new Client(oauth); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
		if (!IsPostBack)
		{

			if (!string.IsNullOrEmpty(Request.QueryString["code"]))
			{
				var token = oauth.GetAccessTokenByAuthorizationCode(Request.QueryString["code"]);
				string accessToken = token.Token;

				cookie["AccessToken"] = accessToken;

				Response.Redirect("Default.aspx");
			}
			else
			{
				string url = oauth.GetAuthorizeURL();
				authUrl.NavigateUrl = url;
			}

		}

		try
		{
			LoadPublicTimeline();
		}
		catch (WeiboException)
		{ 
			//这里追踪下错误，不知道不审核的AppKey能否调用PublicTimeline，好像还是会抛“未审核的应用使用人数达到上限”的错误，自己试试吧，正常的话也没右边是应该有微博数据的
		}
    }

	/// <summary>
	/// 加载微博列表，这里没有授权也是可以用的，具体哪些接口不授权可以用我也不知道，自己看官方API去
	/// </summary>
	private void LoadPublicTimeline()
	{

		StringBuilder statusBuilder = new StringBuilder();
		string imageParttern = @"<img src=""{0}"" alt=""图片"" class=""inner-pic"" />";
		string statusPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-cotent""><span class=""name"">{1}</span>：{2}</p>
		{3}
	</div>
";
		string repostPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-cotent""><span class=""name"">{1}</span>：{2}</p>
		<div class=""repost"">
			<p class=""repost-cotent""><span class=""name"">@{3}</span>：{4}</p>
			{5}
		</div>
	</div>
";

		var json = Sina.API.Dynamic.Statuses.PublicTimeline(count: 10);
		if (json.IsDefined("statuses"))
		{
			foreach (var status in json.statuses)
			{
				if (!status.IsDefined("user"))
					continue;

				if (status.IsDefined("retweeted_status") && status["retweeted_status"].IsDefined("user"))
				{
					statusBuilder.AppendFormat(repostPattern,
						status.user.profile_image_url,
						status.user.screen_name,
						status.text,
						status.retweeted_status.user.screen_name,
						status.retweeted_status.text,
						status.retweeted_status.IsDefined("thumbnail_pic") ?
						string.Format(imageParttern, status.retweeted_status.thumbnail_pic) : "");

				}
				else
				{
					statusBuilder.AppendFormat(statusPattern,
						status.user.profile_image_url,
						status.user.screen_name,
						status.text,
						status.IsDefined("thumbnail_pic") ?
						string.Format(imageParttern, status.thumbnail_pic) : "");
				}

			}
		}

		statusesHtml.Text = statusBuilder.ToString();
	}


}