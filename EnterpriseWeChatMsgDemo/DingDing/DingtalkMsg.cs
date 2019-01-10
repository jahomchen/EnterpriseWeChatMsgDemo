using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWeChatMsgDemo
{
	public class DingtalkMsg
	{
		public static string AppKey = "dingp0xzn7wzpu0vsegd";
		public static string AppSecret = "FffA9Q9KPQLRWtH8860Tt4T2kTErfPBFyXfTlsmEkqTCkXlXRsdi1nB-_WhPoQPS";
		public static string agentid = "216882406";


		/// <summary>
		/// 钉钉网关gettoken地址
		/// </summary>
		public static string URL_GET_TOKKEN = "https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}";

		/// <summary>
		/// 钉钉获取部门列表
		/// </summary>
		public static string URL_DEPARTMENT_LIST = "https://oapi.dingtalk.com/department/list?access_token={0}";

		/// <summary>
		/// 获取部门用户
		/// </summary>
		public static string URL_DEPARTMENT_USERLIST = "https://oapi.dingtalk.com/user/getDeptMember?access_token={0}&deptId={1}";

		/// <summary>
		/// 获取用户信息
		/// </summary>
		public static string URL_USER_INFO = "https://oapi.dingtalk.com/user/get?access_token={0}&userid={1} ";

		/// <summary>
		/// 发送消息
		/// </summary>
		public static string URL_MESSAGE_SEND = "https://oapi.dingtalk.com/message/send?access_token={0}";

		#region 获取Token

		/// <summary>
		/// 获取钉钉的accessToken
		/// </summary>
		/// <returns></returns>
		public static string GetDingTalkAccessToken(string appKey, string appSecret)
		{
			var accessTokenObj = System.Web.HttpRuntime.Cache.Get("dingding:access_token");
			if (accessTokenObj == null)
			{
				string url = string.Format(URL_GET_TOKKEN, appKey, appSecret);
				var respText = HttpHelper.GetHttpResponse(url);
				Dictionary<string, object> respDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(respText);
				//通过键access_token获取值
				var accessToken = respDic["access_token"].ToString();
				System.Web.HttpRuntime.Cache.Insert("dingding:access_token", accessToken, null, DateTime.Now.AddHours(2), TimeSpan.Zero);

				return accessToken;
			}

			return accessTokenObj.ToString();
		}

		#endregion

		#region 获取部门列表
		/// <summary>
		/// 获取部门列表
		/// </summary>
		/// <returns></returns>
		public static DepartmentResult GetDepartmentList(string appKey, string appSecret)
		{
			string token = GetDingTalkAccessToken(appKey, appSecret);
			string url = string.Format(URL_DEPARTMENT_LIST, token);

			var respText = HttpHelper.GetHttpResponse(url);
			return JsonConvert.DeserializeObject<DepartmentResult>(respText);
		}

		#endregion

		#region 获取部门成员

		/// <summary>
		/// 获取部门成员
		/// </summary>
		/// <param name="departmentId">部门id</param>
		/// <returns></returns>
		public static UserReslutByDept GetDepartMentUserList(string appKey, string appSecret, string departmentId)
		{
			string token = GetDingTalkAccessToken(appKey, appSecret);
			string url = string.Format(URL_DEPARTMENT_USERLIST, token, departmentId);
			var respText = HttpHelper.GetHttpResponse(url);
			return JsonConvert.DeserializeObject<UserReslutByDept>(respText);
		}

		#endregion

		#region 获取个人相信信息
		/// <summary>
		/// 获取用户信息
		/// </summary>
		public static string GetUserInfo(string appKey, string appSecret, string userid)
		{
			string token = GetDingTalkAccessToken(appKey, appSecret);
			string url = string.Format(URL_USER_INFO, token, userid);
			var respText = HttpHelper.GetHttpResponse(url);
			//结果参考

			return respText;
		}
		#endregion

		#region 发送个人信息

		public static void SendDingTalkText(string appKey, string appSecret, string agentId, string touser,string content)
		{
			var token = GetDingTalkAccessToken(appKey, appSecret);

			var url = string.Format(URL_MESSAGE_SEND, token);

			var json_req = new
			{
				touser = touser,
				toparty = "",
				agentid = agentId,
				msgtype = "text",
				text = new
				{
					content = content
				}
			};

			var rep = HttpHelper.PostWebRequest(url, JsonConvert.SerializeObject(json_req), Encoding.UTF8);
		}

		#endregion

	}
}
