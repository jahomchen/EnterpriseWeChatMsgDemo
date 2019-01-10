using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DingTalkMsgDemo
{
	public class DingTalkMsgHelper
	{
		static string GetToken(string appId, string secrect)
		{
			string accessToken = "";
			string respText = "";

			var url = string.Format("https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}", appId, secrect);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			using (Stream resStream = response.GetResponseStream())
			{
				StreamReader reader = new StreamReader(resStream, Encoding.Default);
				respText = reader.ReadToEnd();
				resStream.Close();
			}

			Dictionary<string, object> respDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(respText);

			//通过键access_token获取值
			accessToken = respDic["access_token"].ToString();

			return accessToken;
		}

		/// <summary>
		/// Post数据接口
		/// </summary>
		/// <param name="postUrl">接口地址</param>
		/// <param name="paramData">提交json数据</param>
		/// <param name="dataEncode">编码方式</param>
		/// <returns></returns>
		static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
		{
			string ret = string.Empty;
			
				byte[] byteArray = dataEncode.GetBytes(paramData); //转化
				HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
				webReq.Method = "POST";
				webReq.ContentType = "application/json";//Content-Type:application/json

			webReq.ContentLength = byteArray.Length;
				Stream newStream = webReq.GetRequestStream();
				newStream.Write(byteArray, 0, byteArray.Length);//写入参数
				newStream.Close();
				HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
				StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
				ret = sr.ReadToEnd();
				sr.Close();
				response.Close();
				newStream.Close();
			
			return ret;
		}

		public static void SendText(string appId, string secrect,  string toUser, string agentId, string title, string description, string mediaUrl = null)
		{
			var accessToken = GetToken(appId, secrect);
			var postUrl = string.Format("https://oapi.dingtalk.com/message/send?access_token={0}", accessToken);
			var data = new
			{
				touser = toUser,
				agentid = agentId,
				msgtype = "text",
				toparty="",
				text = new
				{
					content = description
				}
			};

			 PostWebRequest(postUrl, JsonConvert.SerializeObject(data), Encoding.UTF8);
		}


		/// <summary>
		/// 获取部门信息
		/// </summary>
		/// <param name="appId">企业ID</param>
		/// <param name="secrect">企业应用的凭证密钥</param>
		/// <returns></returns>
		public static DepartmentResult GetDepartmentInfo(string appId, string secrect)
		{
			////获取access_token
			string access_token = GetToken(appId, secrect);
			string url = "https://oapi.dingtalk.com/department/list?access_token=" + access_token;
			string str = GetResponseDataForGet(url);
			DepartmentResult departmentResult = JsonConvert.DeserializeObject<DepartmentResult>(str);
			return departmentResult;
		}

		/// <summary>
		/// 根据企业ID和企业应用的凭证密钥获取企业下所有人员的信息
		/// </summary>
		/// <param name="appId">企业ID</param>
		/// <param name="secrect">企业应用的凭证密钥</param>
		/// <returns></returns>
		public static List<string> GetUserInfoList(string appId, string secrect)
		{
			//获取access_token
			string access_token = GetToken(appId, secrect);
			if (string.IsNullOrEmpty(access_token)) return null;
			DepartmentResult departmentResult = GetDepartmentInfo(appId, secrect);

			List<string> userList = new List<string>();
			foreach (Department item in departmentResult.Department)
			{
				//根据部门ID获取部门人员详情
				string url = "https://oapi.dingtalk.com/user/getDeptMember?access_token=" + access_token + "&deptId=" + item.ID;
				string str = GetResponseDataForGet(url);
				UserResult userResult = JsonConvert.DeserializeObject<UserResult>(str);
				if (userResult.UserIds.Length > 0)
				{
					userList.AddRange(userResult.UserIds);//添加人员信息到集合中
				}
			}


			return userList;

		}

		static string GetResponseDataForGet(string Url)
		{
			WebRequest request = WebRequest.Create(Url);
			request.Method = "GET";
			WebResponse response = request.GetResponse();
			Stream stream = response.GetResponseStream();
			Encoding encode = Encoding.UTF8;
			StreamReader reader = new StreamReader(stream, encode);
			string strResult = reader.ReadToEnd();
			stream.Dispose();
			reader.Dispose();
			return strResult;
		}


		public class DepartmentResult
		{
			public List<Department> Department { get; set; }
		}

		public class Department
		{
			public int ID;
			public string Name;
			public int Parentid;
			public int Order;
		}

		public class UserResult
		{
			public string[] UserIds { get; set; }
		}
	}
}
