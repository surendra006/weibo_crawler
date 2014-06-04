﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登了个录的</title>
		<link rel="stylesheet" type="text/css" href="css/reset.css" />
	<link rel="stylesheet" type="text/css" href="css/stylesheet.css" />
	<script type="text/javascript" src="scripts/jquery-1.7.2.js"></script>

</head>
<body>
    <form id="form1" runat="server">
	<div id="header">
		<div id="header_wrap" class="row">
			<p id="logo">
				<a href="#">Weibo<span>DotNet</span>4.0</a>
			</p>
			<ul id="page-links">
				<li><a href="#" class="selected">主页</a></li>
				<li><a href="#">联系</a></li>
				<li><a href="#">评论</a></li>
			</ul>
			<div id="search-box" class="row">
				<input type="text" id="search-box-text" />
				<span id="search-box-button"><a href="#" class="magnify"></a></span><span id="search-box-tips">搜索微博内容</span>
				<script type="text/javascript">
					var searchTips = $("#search-box-tips");
					var searchText = $("#search-box-text");
					searchText.focus(function () {
						searchTips.hide();
						$(this).addClass("focus");
					}).blur(function () {
						if ($(this).val().length == 0) {
							$(this).val("");
							searchTips.show();
						}
						$(this).removeClass("focus");
					});
					searchTips.click(function () {
						searchTips.hide();
						searchText.trigger("focus");
					});
				</script>
			</div>
		</div>
	</div>
	<div id="content_wrap" class="row">
		<div id="content_left_wrap">
			<div class="panel">
				<h2 class="title">登录</h2>
				<div class="box">
				<asp:HyperLink id="authUrl" runat="server">
				<img src="images/240.png" alt="点击此处进行授权"/>
			</asp:HyperLink>
				</div>
			</div>
			<div class="panel">
				<h2 class="title">关于新浪微博SDK for .Net</h2>
				<div class="box">
					<ul>
						<li>发布地址：<a href="http://www.cnblogs.com/linxuanchen/">cnblogs</a></li>
						<li>项目地址：<a href="http://weibosdk.codeplex.com">http://weibosdk.codeplex.com</a></li>
						<li>开发者：林选臣</li>
						<li>公司：<a href="http://qcyn.sina.com.cn">新浪网云南频道</a></li>
					</ul>
				</div>
			</div>
		</div>
		<div id="content_right_wrap">
			<h2 class="title">//最新微博/ Code for fun! </h2>
			<asp:Literal runat="server" ID="statusesHtml"></asp:Literal>
		</div>
	</div>
    </form>
</body>
</html>
