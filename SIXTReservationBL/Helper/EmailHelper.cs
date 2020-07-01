using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SIXTReservationBL.Helper
{
    public class EmailHelper
    {
        private string _smtpServer;

        private int _smtpPort;

        private string _fromAddress;

        private string _fromAddressTitle;

        private string _username;

        private string _password;

        private bool _enableSsl;

        private bool _useDefaultCredentials;

        public EmailHelper(IConfiguration configuration)
        {
            _smtpServer = configuration["Email:SmtpServer"];
            _smtpPort = int.Parse(configuration["Email:SmtpPort"]);
            _smtpPort = _smtpPort == 0 ? 25 : _smtpPort;
            _fromAddress = configuration["Email:FromAddress"];
            _fromAddressTitle = configuration["Email:FromAddressTitle"];
            _username = configuration["Email:SmtpUsername"];
            _password = configuration["Email:SmtpPassword"];
            _enableSsl = bool.Parse(configuration["Email:EnableSsl"]);
            _useDefaultCredentials = bool.Parse(configuration["Email:UseDefaultCredentials"]);
        }

        public bool SendEmail(string to , string body, bool isBodyHtml = true)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                string msg = string.Empty;
                MailAddress fromAddress = new MailAddress(this._fromAddress);
                message.From = fromAddress;
                var toList = to.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < toList.Length; i++)
                {
                    message.To.Add(toList[i]);
                }
                message.Subject = _fromAddressTitle;
                message.IsBodyHtml = isBodyHtml;
                message.Body = body;
                smtpClient.Host = this._smtpServer;
                smtpClient.Port = this._smtpPort;
                smtpClient.EnableSsl = this._enableSsl;
                smtpClient.UseDefaultCredentials = this._useDefaultCredentials;
                smtpClient.Credentials = new System.Net.NetworkCredential(this._fromAddress, this._password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}
