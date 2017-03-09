using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Common
{
    public class InvokeRequest
    {
        private readonly string _url;
        private HttpClient client;
        HttpResponseMessage response;
        //Chamada
        //InvokeRequest request = new InvokeRequest("www");
        //var action = "api/person/get";

        ////Post
        //dynamic obj = new object();
        //request.Post<Double>(action, obj);


        //Get
        //Dictionary<string, string> parameters = new Dictionary<string, string>();
        //parameters.Add("param1", "value1");
        //request.Get<Double>(action, parameters);

        public InvokeRequest(string url)
        {
            _url = url;
            client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = null;
        }

        private T GetReturn<T>(string json)
        {
            T retorno = default(T);
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(json));
                retorno = (T)js.ReadObject(ms);
            }
            catch (Exception ex)
            {

            }
            return retorno;
        }

        public T Post<T>(string action, object objeto)
        {
            T retorno = default(T);
            try
            {
                response = client.PostAsync(action,
                                    new StringContent(JsonConvert.SerializeObject(objeto).ToString(),
                                        Encoding.UTF8, "application/json"))
                                        .Result;
                retorno = GetReturn<T>(response.Content.ReadAsStringAsync().Result);
            }
            catch (WebException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return retorno;
        }

        public T Get<T>(string action, Dictionary<string, string> parameters)
        {
            T retorno = default(T);
            try
            {
                string param = string.Empty;
                foreach (var par in parameters)
                    param += string.Format("{0}={1}&", par.Key, par.Value);

                string actionSend = string.Format("{0}?{1}", action, param);
                HttpResponseMessage response = null;

                response = client.GetAsync(actionSend).Result;

                retorno = GetReturn<T>(response.Content.ReadAsStringAsync().Result);
            }
            catch (WebException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return retorno;
        }

    }
}
