using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWeChatMsgDemo
{
	class CorpSendText : CorpSendBase
	{
		/// <summary>
		/// 要发送的文本，必须小写，企业微信API不识别大写。
		/// </summary>
		public Text text { get; set; }



		public CorpSendText(string agentid, string content, string touser)
		{
			this.agentid = agentid;
			this.touser = touser;
			msgtype = "text";
			text = new Text
			{
				content = content
			};
		}
	}


	class Text
	{
		/// <summary>
		/// 要发送的文本内容字段，必须小写，企业微信API不识别大写。
		/// </summary>
		public string content { get; set; }
	}
}
