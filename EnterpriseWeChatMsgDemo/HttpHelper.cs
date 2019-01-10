using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWeChatMsgDemo
{
	public class HttpHelper
	{
		public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
		{
			string ret = string.Empty;

			byte[] byteArray = dataEncode.GetBytes(paramData); //转化
			HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
			webReq.Method = "POST";
			webReq.ContentType = "application/json";

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

		public static string GetHttpResponse(string url)
		{
			string respText = string.Empty;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			using (Stream resStream = response.GetResponseStream())
			{
				StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
				respText = reader.ReadToEnd();
				resStream.Close();
			}
			return respText;
		}
	}
}
