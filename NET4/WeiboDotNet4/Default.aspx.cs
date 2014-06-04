using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetDimension.Web;//这个命名空间的代码在App_Code里面，不要再跑了群里找我要了哈
using NetDimension.Weibo;
using System.Configuration;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
	Cookie cookie = new Cookie("WeiboDemo", 24, TimeUnit.Hour);
	Client Sina = null;
	string UserID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
		if (string.IsNullOrEmpty(cookie["AccessToken"]))
		{
			Response.RedirectPermanent("Login.aspx");
		}
		else
		{
			Sina = new Client(new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], cookie["AccessToken"],null)); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
		}

		UserID = Sina.API.Entity.Account.GetUID();

		BindList();
		LoadFriendTimeline();
        feedDB();
    }

	/// <summary>
	/// 获取用户信息，我们来个直接把JSON写到页面的方法和下面的方法区别下
	/// </summary>
	/// <returns>JSON</returns>
	public string LoadUserInfo()
	{
		var user = Sina.API.Dynamic.Users.Show(UserID);
		return string.Format("{0}", user);
	}


	/// <summary>
	/// 再来个数据绑定的例子
	/// </summary>
	public void BindList()
	{

		var json = Sina.API.Dynamic.Suggestions.HotUsers();
		List<object> datasource = new List<object>();
		int i = 0;
		foreach (var user in json)
		{
			var ds = new
			{
				name = user.screen_name,
				desc = user.description.Length >= 20 ? string.Format("{0}", user.description).Substring(0, 20) : user.description,
				pic = user.profile_image_url
			};

			datasource.Add(ds);

			if (++i > 1)	//官方的HotUsers接口居然不能设定返回数量？？悲剧了嘛
				break;
		}

		rtpFamous.DataSource = datasource;
		rtpFamous.DataBind();

	}

	protected void btnSend_Click(object sender, EventArgs e)
	{
		string status = string.Empty;
		if (txtStatusBody.Text.Length == 0)
		{
			status = "我很懒，所以我直接点了发布按钮。";
		}
		else
		{
			status = txtStatusBody.Text;
		}


		if (fileUpload1.HasFile)
		{
			dynamic result = Sina.API.Dynamic.Statuses.Upload(status, fileUpload1.FileBytes);
		}
		else
		{
			dynamic result = Sina.API.Dynamic.Statuses.Update(status);
		}

		Response.RedirectPermanent("Default.aspx");
	}

	/// <summary>
	/// 加载微博列表
	/// </summary>
	private void LoadFriendTimeline()
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

		var json = Sina.API.Dynamic.Statuses.Show("3717373360356171");
       // var json = Sina.API.Dynamic.Search.Topics("oscar", 10, 1);
        //var statuses = Sina.API.Dynamic.Search.Topics("china_chow", 10);
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
    public void feedDB()
    {
       // var json = Sina.API.Dynamic.Statuses.MentionIDs("11488058246", "", 50, 1, 1, 1);
        var json = Sina.API.Dynamic.Statuses.Show("3717043683980322");
       // var json = Sina.API.Dynamic.Search.Topics("oscar", 10, 1);
        //var statuses = Sina.API.Dynamic.Search.Topics("china_chow", 10);
        if (json.IsDefined("statuses"))
        {
            foreach (var status in json.statuses)
            {
                System.Diagnostics.Trace.WriteLine(status.text);
            }
        }       
    }
}