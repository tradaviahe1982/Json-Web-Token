using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace JWTDataAccess
{
    public class clsTripleDES
    {
        //
        private TripleDESCryptoServiceProvider m_des = new TripleDESCryptoServiceProvider();
        private UTF8Encoding m_utf8 = new UTF8Encoding();
        private byte[] m_key;
        private byte[] m_iv;

        static clsTripleDES TriDes;
        public static clsTripleDES Create()
        {
            if (TriDes == null)
            {
                TriDes = new clsTripleDES();
            }
            return TriDes;
        }

        public clsTripleDES(byte[] key, byte[] iv)
        {
            m_key = key;
            m_iv = iv;
        }

        public clsTripleDES(string sKey, string sIv)
        {
            m_key = Encoding.ASCII.GetBytes(sKey);
            m_iv = Encoding.ASCII.GetBytes(sIv);
        }

        public clsTripleDES()
        {
            m_key = Encoding.ASCII.GetBytes("abcdefghikABCDEFGHIK1234");
            m_iv = Encoding.ASCII.GetBytes("12345678");
        }

        public byte[] Encrypt(byte[] input)
        {
            return Transform(input, m_des.CreateEncryptor(m_key, m_iv));
        }

        public byte[] Decrypt(byte[] input)
        {
            return Transform(input, m_des.CreateDecryptor(m_key, m_iv));
        }

        public string Encrypt(string text)
        {
            try
            {
                byte[] input = m_utf8.GetBytes(text);
                byte[] output = Transform(input, m_des.CreateEncryptor(m_key, m_iv));
                return Convert.ToBase64String(output);
            }
            catch (Exception ex)
            {
                string loi = ex.Message;
                return null;
            }
        }

        public string Decrypt(string text)
        {
            try
            {
                byte[] input = Convert.FromBase64String(text);
                byte[] output = Transform(input, m_des.CreateDecryptor(m_key, m_iv));
                return m_utf8.GetString(output);
            }
            catch (Exception ex)
            {
                string loi = ex.Message;
                return null;
            }
        }

        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();
            memStream.Position = 0;
            byte[] result = new byte[Convert.ToInt32(memStream.Length - 1) + 1];
            memStream.Read(result, 0, Convert.ToInt32(result.Length));
            memStream.Close();
            cryptStream.Close();
            return result;
        }
        //
    }
}
