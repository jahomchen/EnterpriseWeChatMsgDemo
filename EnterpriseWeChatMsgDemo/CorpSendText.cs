using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWeChatMsgDemo
{
	class Text
	{
		string _content;
		/// <summary>
		/// 要发送的文本内容字段，必须小写，企业微信API不识别大写。
		/// </summary>
		public string content
		{
			get { return _content; }
			set { _content = value; }
		}

	}

	class CorpSendText : CorpSendBase
	{
		Text _text;
		/// <summary>
		/// 要发送的文本，必须小写，企业微信API不识别大写。
		/// </summary>
		public Text text
		{
			get { return _text; }
			set { _text = value; }
		}


		public CorpSendText(string content)
		{
			msgtype = "text";
			text = new Text
			{
				content = content
			};
		}
	}

}
