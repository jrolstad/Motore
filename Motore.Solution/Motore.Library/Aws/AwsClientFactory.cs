using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SimpleDB;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Configuration;
using Amazon.Runtime;
using Motore.Utils.Assertions;

namespace Motore.Library.Aws
{
    public class AwsClientFactory
    {
        public static SimpleDbClient CreateSimpleDbClient()
        {
            var accessKey = Config.AwsAccessKey;
            var secretKey = Config.AwsSecretKey;
            var serviceUrl = Config.SimpleDbServiceUrl;

            Assert.Fail(() => (!String.IsNullOrWhiteSpace(accessKey)), "The AWS access key may not be null or blank.");
            Assert.Fail(() => (!String.IsNullOrWhiteSpace(secretKey)), "The AWS secret key may not be null or blank.");
            Assert.Fail(() => (!String.IsNullOrWhiteSpace(serviceUrl)), "The ServiceUrl may not be null or blank.");

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonSimpleDBConfig
                             {
                                 ServiceURL = serviceUrl,
                             };

            var client = new SimpleDbClient(credentials, config);
            return client;
        }
    }
}
