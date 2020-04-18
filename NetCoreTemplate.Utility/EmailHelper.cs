using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace NetCoreTemplate.Utility
{
    public class EmailHelper
    {
        /// <summary>
        /// 阿里企业邮件发送
        /// </summary>
        /// <param name="serverHost"></param>
        /// <param name="serverEmail"></param>
        /// <param name="serverPwd"></param>
        /// <param name="serverNickname"></param>
        /// <param name="toEmail"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="paths"></param>
        public static void AliEmailSend(string serverHost, string serverEmail, string serverPwd, string serverNickname, string toEmail, string title, string content, string paths)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.To.Add(toEmail);
            mailMsg.From = new MailAddress(serverEmail, serverNickname);
            mailMsg.Subject = title;//邮件标题    
            mailMsg.Body = content;//邮件内容      
            mailMsg.IsBodyHtml = true;//是否是HTML邮件    
            mailMsg.Priority = MailPriority.Normal;//邮件优先级    

            if (!string.IsNullOrWhiteSpace(paths))
            {
                foreach (string path in paths.Split(','))
                {
                    if (string.IsNullOrWhiteSpace(path))
                        continue;
                    Attachment data = new Attachment(path, MediaTypeNames.Application.Octet);
                    mailMsg.Attachments.Add(data);
                }
            }

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(serverEmail, serverPwd);
            client.Port = 80;
            client.Host = serverHost; //"smtp.mxhichina.com";
            client.EnableSsl = true;//经过ssl加密
            client.Send(mailMsg);
        }

        public static bool IsEmail(string email)
        {
            String strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex r = new Regex(strExp);
            Match m = r.Match(email);
            return m.Success;
        }

    }
}
