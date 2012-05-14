using System.Configuration;

namespace Motore.Library.Configuration
{
    public class Config
    {
        public static string S3ServiceUrl
        {
            get
            {
                var configValue = System.Configuration.ConfigurationManager.AppSettings["S3.ServiceUrl"];
                return configValue;
            }
        }

        public static string S3Scheme
        {
            get
            {
                var configValue = System.Configuration.ConfigurationManager.AppSettings["S3.Scheme"];
                return configValue;
            }
        }

        public static string PortfolioBucketName
        {
            get
            {
                var configValue = System.Configuration.ConfigurationManager.AppSettings["S3.PortfolioBucketName"];
                return configValue;
            }
        }
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

        public static long MaximumPortfolioFileUploadSizeInBytes
        {
            get
            {
                var configValue = ConfigurationManager.AppSettings["MaximumPortfolioFileUploadSizeInBytes"];
                return long.Parse(configValue);
            }
        }

        public static string SimpleDbServiceUrl
        {
            get
            {
                var configValue = ConfigurationManager.AppSettings["SimpleDbServiceUrl"];
                return configValue;
            }
        }
    }
}