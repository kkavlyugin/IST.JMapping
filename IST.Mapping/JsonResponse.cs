using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public static class JsonResponse
    {
        //public static string _url;

        public struct Method
        {
            public const string Get = "GET";
            public const string Post = "POST";
            public const string Put = "PUT";
            public const string Del = "DEL";
        };

        public static string GetJson(string url,string tableName = "")
        {
            string jsonString = String.Empty;
            try
            {
                if (tableName != "")
                {
                    string md5 = JMemoryDataSource.GetMD5(tableName);
                    if (md5 != "")
                    {
                        url = $"{url}?md5={md5}";
                    }
                }
                string token = IST.JMapping.Settings.Global.Token;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;// | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                //req.ContentType = "application/json";
                req.Method = Method.Get;
                if (token != null)
                {
                    req.Headers["Authorization"] = $"Bearer {token}";
                }
                //req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                req.Timeout = 20000;
                System.Net.WebResponse res = req.GetResponse();
                System.IO.Stream ReceiveStream = res.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    jsonString += str;
                    count = sr.Read(read, 0, 256);
                }
                res.Close();
                ReceiveStream.Close();
            }
            catch { }

            return jsonString;
        }

        public static string PostJson(string url,string json = "")
        {

            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            req.ContentType = "application/json";
            req.Method = Method.Post;
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (IST.JMapping.Settings.Global.Token != null)
            {
                req.Headers["Authorization"] = $"Bearer {IST.JMapping.Settings.Global.Token}";
            }
            req.Timeout = 20000;
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                //string json = "{\"user\":\"test\"," +
                //              "\"password\":\"bla\"}";
                streamWriter.Write("{"+ json + "}");
            }

            var httpResponse = (HttpWebResponse)req.GetResponse();
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        public static string PutJson(string url,object id, string json)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create($"{url}?id={id}");
            req.ContentType = "application/json";
            req.Method = Method.Put;
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (IST.JMapping.Settings.Global.Token != null)
            {
                req.Headers["Authorization"] = $"Bearer {IST.JMapping.Settings.Global.Token}";
            }
            req.Timeout = 20000;
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                //string json = "{\"user\":\"test\"," +
                //              "\"password\":\"bla\"}";
                streamWriter.Write("{" + json + "}");
            }

            var httpResponse = (HttpWebResponse)req.GetResponse();
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        public static string DelJson(string url, object id)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create($"{url}?id={id}");
            req.ContentType = "application/json";
            req.Method = Method.Del;
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (IST.JMapping.Settings.Global.Token != null)
            {
                req.Headers["Authorization"] = $"Bearer {IST.JMapping.Settings.Global.Token}";
            }
            req.Timeout = 20000;            

            var httpResponse = (HttpWebResponse)req.GetResponse();
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        public static string GetResponseByteJSON<T>(string url)
        {
            var client = new WebClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;// | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var json = client.DownloadString(url);
            //System.Text.Json.JsonSerializer.Deserialize<T>(json);

            //List <T> userPosts = JsonConvert.DeserializeObject<List<T>>(json);

            return json;

            //System.Net.WebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.Method = "GET";
            //webRequest.K = true;
            //webRequest.ContentType = "application/x-www-form-urlencoded";
            //webRequest.CookieContainer = cookieJar;

            //webResponse = (HttpWebResponse)webRequest.GetResponse();

            //reader = new StreamReader(webResponse.GetResponseStream());
            //string responseBody = reader.ReadToEnd();
            //reader.Close();
            //ServerInfo serverInfo = JsonConvert.DeserializeObject < ServerInfo >
            //my_label_ServerInfo.Text = serverInfo.message;
        }

        public static List<T> JSONParseObject<T>(string jsonText)
        {
            JObject jResults = JObject.Parse("{ \"Data\":" + jsonText + "}");
            List<T> list = new List<T>();
            foreach (var item in jResults["Data"])
            {
                list.Add(item.ToObject<T>());
            }
            return list;
        }

        public static T JSONDeserializeObject<T>(string jsonText)
        {
            var jObj = JObject.Parse("{ \"Data\":" + jsonText + "}");
            JObject categories = (JObject)jObj["Data"];
            return JsonConvert.DeserializeObject<T>(categories.ToString());
        }

        public static List<T> JSONParseDynamic<T>(string jsonText)
        {
            dynamic jResults = JsonConvert.DeserializeObject(jsonText);
            List<T> list = new List<T>();
            foreach (var item in jResults.Everything)
            {
                list.Add((T)item.name);
            }
            return list;
        }
        
        public static DataTable JsonToDataTable(string jsonText)
        {
            JObject jsonLinq = JObject.Parse("{ \"Data\":" + jsonText + "}");
            // Find the first array using Linq  
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types  
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }
                trgArray.Add(cleanRow);
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }
        
        public static DataTable JsonToDataTableMemory(string jsonText)
        {
            JObject jsonLinq;
            string md5 = "";
            if (jsonText.Contains("{\"md5\":\""))
            {
                jsonLinq = JObject.Parse(jsonText);
                JToken jmd5 = jsonLinq.Root.First;
                md5 = jmd5.First.ToString();
            }
            else
            {
                jsonLinq = JObject.Parse("{ \"Data\":" + jsonText + "}");
            }
            // Find the first array using Linq  
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types  
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }
                trgArray.Add(cleanRow);
            }
            DataTable table = JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
            if (md5 != "")
            {
                JMemoryDataSource.Remove(table.TableName);
                JMemoryDataSource.Add(table, md5);
            }
            return table;
        }
    }
}
