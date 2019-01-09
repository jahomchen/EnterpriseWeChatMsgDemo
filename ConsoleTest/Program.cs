using EnterpriseWeChatMsgDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var templateMsg = "测评中心邀请信息<br/><br/>你好，<br/>请于"
				+ DateTime.Now.AddDays(5) +
				"之前完成测试,请点击www.baidu.com开始答题。";

			EnterpriseWeChatHelper.SendText("OuDaAoTuManDeXiaoGuaiShou", templateMsg);

			Console.ReadKey();
		}
	}
}
