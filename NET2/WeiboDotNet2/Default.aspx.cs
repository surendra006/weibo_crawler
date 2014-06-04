using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetDimension.Web;//这个命名空间的代码在App_Code里面，不要再跑了群里找我要了哈
using NetDimension.Weibo;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    NetDimension.Web.Cookie cookie = new NetDimension.Web.Cookie("WeiboDemo", 24, TimeUnit.Hour);
    List<string> ids = new List<string>();
	Client Sina = null;
	string UserID = string.Empty;
    Regex rx_html = new Regex(
                   @"\b[0-9]{16}\b",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline); 

    protected void Page_Load(object sender, EventArgs e)
    {
		if (string.IsNullOrEmpty(cookie["AccessToken"]))
		{
			Response.Redirect("Login.aspx");
		}
		else
		{
			Sina = new Client(new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], cookie["AccessToken"],null)); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
		}

		UserID = Sina.API.Account.GetUID();

		//BindList();
	//	LoadFriendTimeline();
        feedDB();
    }

	/// <summary>
	/// 获取用户信息，我们来个直接把JSON写到页面的方法和下面的方法区别下
	/// </summary>
	/// <returns>JSON</returns>
	public string LoadUserInfo()
	{
		NetDimension.Weibo.Entities.user.Entity user = Sina.API.Users.Show(UserID,null);

		string result = user.ToString();

		return string.Format("{0}", result);
	}


	/// <summary>
	/// 再来个数据绑定的例子
	/// </summary>
	public void BindList()
	{

		IEnumerable<NetDimension.Weibo.Entities.user.Entity> json = Sina.API.Suggestions.HotUsers(HotUserCatagory.@default);

		List<NetDimension.Weibo.Entities.user.Entity> ds = new List<NetDimension.Weibo.Entities.user.Entity>();

		int count = 0;
		foreach (NetDimension.Weibo.Entities.user.Entity x in json)
		{

			ds.Add(x);
			if (++count == 2)
				break;
		}//本来一句Take(2)搞定的事情，2.0居然要这样，我都不知道2.0时代我怎么活过来的

		rtpFamous.DataSource = ds;
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
			Sina.API.Statuses.Upload(status, fileUpload1.FileBytes,0,0,null);
		}
		else
		{
			Sina.API.Statuses.Update(status,0,0,null);
		}

		Response.Redirect("Default.aspx");
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

		NetDimension.Weibo.Entities.status.Collection json = Sina.API.Statuses.FriendsTimeline("0","0",50,1,false,0);
		if (json.Statuses!=null)
		{
			foreach (NetDimension.Weibo.Entities.status.Entity status in json.Statuses)
			{
				if (status.User==null)
					continue;

				if (status.RetweetedStatus!=null && status.RetweetedStatus.User!=null)
				{
					statusBuilder.AppendFormat(repostPattern,
						status.User.ProfileImageUrl,
						status.User.ScreenName,
						status.Text,
						status.RetweetedStatus.User.ScreenName,
						status.RetweetedStatus.Text,
						status.RetweetedStatus.ThumbnailPictureUrl!=null ?
						string.Format(imageParttern, status.RetweetedStatus.ThumbnailPictureUrl) : "");

				}
				else
				{
					statusBuilder.AppendFormat(statusPattern,
						status.User.ProfileImageUrl,
						status.User.ScreenName,
						status.Text,
						status.ThumbnailPictureUrl!=null ?
						string.Format(imageParttern, status.ThumbnailPictureUrl) : "");
				}

			}
		}

		statusesHtml.Text = statusBuilder.ToString();
	}
    public void feedDB()
    {
        String json = "";
       // int pagenum = 0;
        string[] ip = System.IO.File.ReadAllLines(@"C:\Users\User\Desktop\plainIPs.txt");
        int ip_select=5;
        //int port = 80;
        Console.WriteLine("ip:" + ip[ip_select]);
        for (int i = 0; i < 20; i++)
        {
          //  pagenum = i;
            String url = "http://s.weibo.com/weibo/oscar" + "&nodup=1&page=" + i + "";
            WebProxy myProxy = new WebProxy(ip[ip_select]);
            myProxy.BypassProxyOnLocal = true;            
            WebRequest request = WebRequest.Create(url);
            request.Proxy = myProxy;
            WebResponse response = request.GetResponse();
            String text = new StreamReader(response.GetResponseStream()).ReadToEnd();            
            MatchCollection htmls = rx_html.Matches(text);
            if (!htmls.Count.Equals(0))
            {
            foreach (Match html in htmls)
            {
                if (!ids.Contains(html.Value))
                {
                       ids.Add(html.Value);
                       System.IO.File.WriteAllText(@"C:\Users\User\Desktop\test_web_new.txt", "," + html.Value +",");
                }
            }
            }
            else {
                Console.WriteLine("no ids found and hence changing ip");
                ip_select++;
            }
        }
        for (int i = 0; i < ids.Count; i++)
        {
            json = json + "" + Sina.API.Statuses.Show(ids[i]).Text.ToString();
            System.IO.File.WriteAllText(@"C:\Users\User\Desktop\test_web_new.txt", json);
        }
    }

}