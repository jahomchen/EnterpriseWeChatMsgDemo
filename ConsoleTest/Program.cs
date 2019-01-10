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


			//EnterpriseWeChatHelper.SendText("@all", templateMsg);



			DingTalkMsgDemo.DingTalkMsgHelper.SendText("dingp0xzn7wzpu0vsegd", "FffA9Q9KPQLRWtH8860Tt4T2kTErfPBFyXfTlsmEkqTCkXlXRsdi1nB-_WhPoQPS", string.Join("|", DingTalkMsgDemo.DingTalkMsgHelper.GetUserInfoList("dingp0xzn7wzpu0vsegd", "FffA9Q9KPQLRWtH8860Tt4T2kTErfPBFyXfTlsmEkqTCkXlXRsdi1nB-_WhPoQPS")), "216882406", "测评中心测试", templateMsg);

			Console.ReadKey();
		}
	}
}
