using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestSubscriber
{
    public sealed class HttpClientInstance : HttpClient
    {
        private static volatile HttpClientInstance instance;
        private static object syncRoot = new object();

        private HttpClientInstance()
        { }

        public static HttpClientInstance GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new HttpClientInstance();

                    instance.BaseAddress = new Uri("http://localhost:56045");
                    instance.DefaultRequestHeaders.Accept.Clear();
                    instance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            return instance;
        }

    }
}
