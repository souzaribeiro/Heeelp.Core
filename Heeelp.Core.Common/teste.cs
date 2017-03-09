using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Common
{
    public class teste
    {
        public void Teste()
        {

           
            InvokeRequest request = new InvokeRequest("www");
            var action = "api/person/get";

            //Post
            dynamic obj = new object();
            request.Post<Double>(action, obj);


            //Get
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("param1", "value1");
            request.Get<Double>(action, parameters);


            //Dictionary<string, string> parametros = new Dictionary<string, string>();
            //parameters.Add("param1", "value1");
            //parameters.Add("param2", "value2");
            //Invoke<PersonDTO>(HttpRequestType.GET, _uri, null);
        }
    }
}