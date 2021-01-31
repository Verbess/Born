using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace JackUtil {

    [Serializable]
    public class HttpHelper {

        HttpClient client;

        ///<summary>
        ///uri = "http://xxx.xxx.xxx.xxx:port"
        ///</summary>
        public HttpHelper(string uri) {

            client = new HttpClient();
            client.BaseAddress = new Uri(uri);

        }

        // Get
        public async Task<string> GetAsync(string uri, Dictionary<string, string> paramData, Action<string> errCallBack = null) {

            if (paramData != null) {

                uri += "?";

                foreach (var kv in paramData) {
                    string key = kv.Key;
                    string value = kv.Value;
                    uri += key + "=" + value + "&";
                }

            }
            
            string r = string.Empty;

            await client.GetStringAsync(uri).ContinueWith(async t => {

                r = await t;
               
                if (t.IsFaulted) {

                    errCallBack?.Invoke("错误");
                    
                }

            });

            return r;

        }

        // Post
        public async Task<string> PostAsync(string uri, Dictionary<string, string> paramData, Action<string> errCodeCallBack = null) {

            if (paramData == null) {
                paramData = new Dictionary<string, string>();
            }

            string r = string.Empty;

            await client.PostAsync(uri, new FormUrlEncodedContent(paramData)).ContinueWith(async t => {


                if (t.IsFaulted) {

                    HttpResponseMessage res = await t;
                    errCodeCallBack?.Invoke(res.StatusCode.ToString());

                } else {

                    HttpResponseMessage res = await t;
                    r = await res.Content.ReadAsStringAsync();

                }

            });

            return r;

        }

    }
}