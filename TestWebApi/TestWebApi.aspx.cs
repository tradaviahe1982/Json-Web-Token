using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//
using System.Net;
using System.Text;
using System.Threading.Tasks;
//
using Newtonsoft.Json;
//
namespace TestWebApi
{
    public partial class TestWebApi : System.Web.UI.Page
    {
        //
        string base_url = "http://localhost:8080/api";
        //
        public string AuthenticateUser(string username, string password)
        {
            string endpoint = base_url + "/login";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                Username = username,
                Password = password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<string>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            //
            string token = AuthenticateUser("vietanh", "admin");
            string endpoint = base_url + "/values";
            //
            /*WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = token;
            try
            {
                string response = wc.DownloadString(endpoint);
                string ketqua = JsonConvert.DeserializeObject<string>(response);
                txtKQ.Text = ketqua;
            }
            catch (Exception)
            {
            }*/
            using (WebClient client = new WebClient())
            {
                client.Headers.Clear();
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Bearer " + token);
                //
                try
                {
                    string response = client.DownloadString(endpoint);
                    string ketqua = JsonConvert.DeserializeObject<string>(response);
                    txtKQ.Text = ketqua.ToString();
                }
                catch (Exception ex)
                {
                    Trace.Write(ex.Message);
                }
                //
            }
            //
        }
    }
}