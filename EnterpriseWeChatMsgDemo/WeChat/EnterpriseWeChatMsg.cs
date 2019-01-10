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
	public class EnterpriseWeChatMsg
	{
		public static string corpid = "ww37b11e33bc9c0501";
		public static string corpsecret = "CceoQ5J4fVkTJfjRyVr9md2bimpCgxVBU2lAOoTTNhg";
		public static string agentid = "1000002";




		static string messageSendURI = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";
		static string getAccessTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";


		/// <summary>
		/// 获取企业号的accessToken
		/// </summary>
		/// <param name="corpid">企业号ID</param>
		/// <param name="corpsecret">管理组密钥</param>
		/// <returns></returns>
		static string GetQYAccessToken(string corpid, string corpsecret)
		{
			string respText = string.Empty;

			//获取josn数据
			string url = string.Format(getAccessTokenUrl, corpid, corpsecret);

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
			var accessToken = respDic["access_token"].ToString();

			return accessToken;
		}



		/// <summary>
		/// 推送信息
		/// </summary>
		/// <param name="corpid">企业号ID</param>
		/// <param name="corpsecret">管理组密钥</param>
		/// <param name="paramData">提交的数据json</param>
		/// <param name="dataEncode">编码方式</param>
		/// <returns></returns>
		public static string SendText(string corpid, string corpsecret,string agentId, string touser, string content)
		{
			var accessToken = GetQYAccessToken(corpid, corpsecret);
			var postUrl = string.Format(messageSendURI, accessToken);
			CorpSendText paramData = new CorpSendText(agentId, content, touser);
			var param = JsonConvert.SerializeObject(paramData);
			var postResult = HttpHelper.PostWebRequest(postUrl, param, Encoding.UTF8);

			return postResult;
		}

	}
}
