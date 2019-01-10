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
    public class EnterpriseDingtalkHelper
    {
        static string AppKey = "dingp0xzn7wzpu0vsegd";
        static string AppSecret = "FffA9Q9KPQLRWtH8860Tt4T2kTErfPBFyXfTlsmEkqTCkXlXRsdi1nB-_WhPoQPS";

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
        public static string URL_DEPARTMENT_USERLIST = "https://oapi.dingtalk.com/user/list?access_token={0}&department_id={1}";

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
        public static string GetDingTalkAccessToken()
        {
            //用GetDingTalkAccessToken获取，并且缓存7200s,防止请求过多
            return "acc5862d72d1341cac795d551781c3af";


            //string accessToken = "";
            //string respText = "";
            //string url = string.Format(URL_GET_TOKKEN, AppKey, AppSecret);
            //respText = GetHttpResponse(url);
            //Dictionary<string, object> respDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(respText);
            ////通过键access_token获取值
            //accessToken = respDic["access_token"].ToString();
            //return accessToken;
        }

        #endregion 

        #region 获取部门列表
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public static string GetDepartmentList()
        {
            string respText = "";
            string token = GetDingTalkAccessToken();

            string url = string.Format(URL_DEPARTMENT_LIST, token);

            respText = GetHttpResponse(url);
            return respText;
        }

        #endregion

        #region 获取部门成员

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <returns></returns>
        public static string GetDepartMentUserList(string departmentId)
        {
            string respText = "";
            string token = GetDingTalkAccessToken();

            string url = string.Format(URL_DEPARTMENT_USERLIST, token, departmentId);
            respText = GetHttpResponse(url);
            //结果参考
            //{"userlist":[{"department":[1],"unionid":"HqmiPVr5gUeCsB0FL66yEiPgiEiE","userid":"113555363836561435","isBoss":false,"order":180014712326902660,"name":"郑爱涛","isLeader":true,"avatar":"","active":true,"isAdmin":true,"openId":"HqmiPVr5gUeCsB0FL66yEiPgiEiE","mobile":"13861306792","isHide":false}],"errmsg":"ok","errcode":0}
            return respText;
        }

        #endregion

        #region 获取个人相信信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static string GetUserInfo(string userid)
        {
            string respText = "";
            string token = GetDingTalkAccessToken();

            string url = string.Format(URL_USER_INFO, token, userid);
            respText = GetHttpResponse(url);
            //结果参考

            return respText;
        }
        #endregion

        #region 发送个人信息

        public static void SendDingTalkMessage()
        {
            var token = GetDingTalkAccessToken();

            var url = string.Format(URL_MESSAGE_SEND, token);
            var json_req = new
            {
                touser = "113555363836561435",  //接受推送userid，不同用户用|分割
                toparty = "1",   //接受推送部门id
                agentid = "216882406",
                msgtype = "text", //推送类型
                text = new
                {
                    content = "欢迎来吃饭，哈哈"
                }
            };
            var rep = PostWebRequest(url, JsonConvert.SerializeObject(json_req));
        }


        #endregion

        private static string GetHttpResponse(string url)
        {
            string respText = "";
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
        static string PostWebRequest(string postUrl, string paramData)
        {
            string ret = string.Empty;

            byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType = "application/json";

            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();

            return ret;
        }
    }
}
