using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
namespace JWTDataAccess
{
    public class clsCommon
    {
        //
        public const string stralert = "Không tìm thấy khách hàng";
        public const string strAuthSucc = "Xác thực thành công";
        public const string strPassStatus = "Mật khẩu mới đã được cập nhật vào hệ thống";
        public const string strOldPass = "Chưa đúng mật khẩu cũ";
        public const string strCaptCha = "Gõ chưa đúng";
        public const string strLoithaotac = "Thao tác không hợp lệ";
        public const string strLoiquatg = "Thời gian thực hiện vượt quá cho phép";
        public const string strLoiDb = "Lỗi thao tác Database";
        public const string strLoiCm = "Lỗi không xác định";
        public const string strLoikhongKN = "Không kết nối được Database";
        public const string sMatkhaurong = "CF83E1357EEFB8BDF1542850D66D807D620E45B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E";
        public const string sCong = "Cộng";
        public const string sTongCong = "Tổng cộng";

        clsTripleDES oDes;
        public clsTripleDES PDes
        {
            get { return oDes; }
        }

        public string MD5Encode(string sInp)
        {
            string strpass = null;
            MD5CryptoServiceProvider shaM = new MD5CryptoServiceProvider();
            byte[] resb = null;
            ASCIIEncoding enc = new ASCIIEncoding();
            int i = 0;
            resb = shaM.ComputeHash(enc.GetBytes(sInp));
            for (i = 0; i <= resb.Length - 1; i++)
            {
                strpass += resb[i].ToString("x2");
                //chuyển thành số hexa có 2 chữ số
            }
            return strpass;
        }

        public string Sha512Encode(string sInp)
        {
            string strpass = null;
            try
            {
                SHA512 shaM = SHA512.Create();
                ASCIIEncoding enc = new ASCIIEncoding();
                int i = 0;
                shaM.ComputeHash(enc.GetBytes(sInp));
                for (i = 0; i <= shaM.Hash.Length - 1; i++)
                {
                    strpass += shaM.Hash[i].ToString("X");
                    //chuyển thành hexxa chữ hoa
                }
            }
            catch (Exception ex)
            {
                string loi = ex.Message;
                return null;
            }
            return strpass;
        }

        public static string Sha256Encode(string sInp)
        {
            string strpass = null;
            SHA256Managed shaM = new SHA256Managed();
            byte[] resb = null;
            ASCIIEncoding enc = new ASCIIEncoding();
            int i = 0;
            resb = shaM.ComputeHash(enc.GetBytes(sInp));
            for (i = 0; i <= resb.Length - 1; i++)
            {
                strpass += resb[i].ToString("X");
            }
            return strpass;
        }
        public static string RipEncode(string sInp)
        {
            string strpass = "";
            RIPEMD160 rip = default(RIPEMD160);
            byte[] resb = null;
            ASCIIEncoding enc = new ASCIIEncoding();
            int i = 0;
            rip = RIPEMD160.Create();
            resb = rip.ComputeHash(enc.GetBytes(sInp));
            for (i = 0; i <= resb.Length - 1; i++)
            {
                strpass += resb[i].ToString("X");
            }
            return strpass;
        }
        public clsCommon(string sKey, string sIv)
        {
            Byte[] Key = Encoding.ASCII.GetBytes(sKey);
            Byte[] Iv = Encoding.ASCII.GetBytes(sIv);
            oDes = new clsTripleDES(Key, Iv);
        }
        public clsCommon()
        {
            Byte[] Key = Encoding.ASCII.GetBytes("abcdefghikABCDEFGHIK1234");
            Byte[] Iv = Encoding.ASCII.GetBytes("12345678");
            oDes = new clsTripleDES(Key, Iv);
        }
        //
        public static void SendMailBySMPT(string mailFrom, string passmailform, string mailTo, string mailCC, string Subject, string Body, int Port, string mailServer)
        {
            // smpt cong bang 25
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            try
            {
                smtpClient.Host = mailServer;
                smtpClient.Port = Port;
                System.Net.NetworkCredential credential = new NetworkCredential(mailFrom, passmailform);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credential;
                mailMessage.From = new MailAddress(mailFrom);
                mailMessage.To.Add(new MailAddress(mailTo));
                if (mailCC.Trim() != "")
                {
                    mailMessage.CC.Add(mailCC);
                }
                mailMessage.Subject = Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.UTF8Encoding.UTF8;
                mailMessage.Body = string.Format(Body);
                smtpClient.Send(mailMessage);
            }
            catch { }
        }
        public static void SendByGMail(string mailFrom, string passmailfrom, string mailTo, string mailCC, string Subject, string Body, int Port, string mailServer)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Host = mailServer;//smtp.gmail.com
            smtpClient.Port = Port;//587
            System.Net.NetworkCredential credential = new NetworkCredential(mailFrom, passmailfrom);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailFrom);
            mailMessage.To.Add(new MailAddress(mailTo));
            if (mailCC.Trim() != "")
            {
                mailMessage.CC.Add(mailCC);
            }
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = string.Format(Body);
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch { }
        }
        //
    }
}
