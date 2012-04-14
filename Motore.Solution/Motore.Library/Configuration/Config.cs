using System.Configuration;

namespace Motore.Library.Configuration
{
    public class Config
    {
        public static string AwsAccessKey
        {
            get
            {
                var configValue = System.Configuration.ConfigurationManager.AppSettings["AwsAccessKey"];
                return configValue;
            }
        }

        public static string AwsSecretKey
        {
            get
            {
                var configValue = ConfigurationManager.AppSettings["AwsSecretKey"];
                return configValue;
            }
        }

        public static string MarketDataRequestQueueUrl
        {
            get
            {
                var configValue = ConfigurationManager.AppSettings["MarketDataRequestQueueUrl"];
                return configValue;
            }
        }
    }
}