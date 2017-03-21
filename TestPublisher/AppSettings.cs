using System;
using System.Configuration;

namespace TestPublisher
{
    class AppSettings
    {
        public AppSettings(string rabbitUri, string rabbitUsername, string rabbitPassword)
        {
            this.RabbitUri = rabbitUri;
            this.RabbitUserName = rabbitUsername;
            this.RabbitPassword = rabbitPassword;
        }

        public string RabbitUri { get; }
        public string RabbitUserName { get; }
        public string RabbitPassword { get; }

        public static string GetAppSettings(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Missing {key} config setting. {ex.ToString()}");
            }
        }
    }
}
