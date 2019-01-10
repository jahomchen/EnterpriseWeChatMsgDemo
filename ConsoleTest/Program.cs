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
			//EnterpriseWeChatMsg.SendText(EnterpriseWeChatMsg.corpid, EnterpriseWeChatMsg.corpsecret, EnterpriseWeChatMsg.agentid, "@all", "测试消息");




			var userIds = new List<string>();
			var depts = DingtalkMsg.GetDepartmentList(DingtalkMsg.AppKey, DingtalkMsg.AppSecret);
			foreach (var dept in depts.Department)
			{
				var userRes = DingtalkMsg.GetDepartMentUserList(DingtalkMsg.AppKey, DingtalkMsg.AppSecret, dept.Id.ToString());
				userIds.AddRange(userRes.UserIds);
			}
			DingtalkMsg.SendDingTalkText(DingtalkMsg.AppKey, DingtalkMsg.AppSecret, DingtalkMsg.agentid, string.Join("|", userIds), "钉钉测试");



			Console.ReadKey();
		}
	}
}
