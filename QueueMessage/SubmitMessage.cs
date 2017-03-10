using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestSubscriber
{
    class SubmitMessage<T> : ISubmitMessage<T>
    {
        private string BaseUri;
        private string RelativeUri;

        public SubmitMessage(string baseUri, string reltiveUri)
        {
            BaseUri = baseUri;
            RelativeUri = reltiveUri;  
        }

        public async Task Submit (T message)
        {
            var json = JsonConvert.SerializeObject(message, Formatting.Indented);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = HttpClientInstance.GetInstance();

            HttpResponseMessage response = await client.PostAsync(RelativeUri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Returned http status code {response.StatusCode}. Relative Uri {RelativeUri}. For this message {json.ToString()}");
            }

            return ;
        }
    }
}
