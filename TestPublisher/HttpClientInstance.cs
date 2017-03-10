using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestPublisher
{
    class HttpClientInstance : HttpClient
    {
        private static volatile HttpClientInstance instance;
        private static object syncRoot = new Object();

        private HttpClientInstance()
        { }

        public static HttpClientInstance GtInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new HttpClientInstance();

                    instance.BaseAddress = new Uri(AppSettings.GetAppSettings("TaskEnginApiBaseUrl"));
                    instance.DefaultRequestHeaders.Accept.Clear();
                    instance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            return instance;
        }
    }
}
