using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeiboDesktopDotNet4
{
	//扩展下所有Control类，把线程操作Invoke提出来。不然像2.0哪样个个线程方法都得if else，我就又开始蛋疼了。具体不要问题，自己谷歌。
	public static class ControlExtended
	{
		public delegate void InvokeHandler();  
        public static void UIInvoke(this Control control, InvokeHandler handler)  
        {  
            if (control.InvokeRequired)  
            {  
                control.Invoke(handler);  
            }  
            else  
            {  
                handler();  
            }  
       }  
	}
}
