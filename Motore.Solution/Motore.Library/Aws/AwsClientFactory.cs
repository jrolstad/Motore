using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.S3;
using Amazon.SimpleDB;
using Motore.Library.Aws.S3;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Configuration;
using Amazon.Runtime;
using Motore.Utils.Assertions;

namespace Motore.Library.Aws
{
    public class AwsClientFactory
    {
        public static S3Client CreateS3Client()
        {
            var accessKey = Config.AwsAccessKey;
            var secretKey = Config.AwsSecretKey;
            var serviceUrl = Config.S3ServiceUrl;
            var scheme = Config.S3Scheme;
            Amazon.S3.Model.Protocol protocol;

// ReSharper disable AccessToStaticMemberViaDerivedType
            if (!(Amazon.S3.Model.Protocol.TryParse(scheme, true, out protocol)))
// ReSharper restore AccessToStaticMemberViaDerivedType
            {
                var msg =
                    String.Format(
                        "The protocol value '{0}' could not be converted into a type of Amazon.S3.Model.Protocol",
                        scheme);
                throw new ArgumentOutOfRangeException("protocol", msg);
            }

            Assert.Fail(() => (!String.IsNullOrWhiteSpace(accessKey)), "The AWS access key may not be null or blank.");
            Assert.Fail(() => (!String.IsNullOrWhiteSpace(secretKey)), "The AWS secret key may not be null or blank.");
            Assert.Fail(() => (!String.IsNullOrWhiteSpace(serviceUrl)), "The S3ServiceUrl may not be null or blank.");

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config
            {
                ServiceURL = serviceUrl,
                CommunicationProtocol = protocol,
            };

            var client = new S3Client(credentials, config);
            return client;
        }

        public static SimpleDbClient CreateSimpleDbClient()
        {
            var accessKey = Config.AwsAccessKey;
            var secretKey = Config.AwsSecretKey;
            var serviceUrl = Config.SimpleDbServiceUrl;

            Assert.Fail(() => (!String.IsNullOrWhiteSpace(accessKey)), "The AWS access key may not be null or blank.");
            Assert.Fail(() => (!String.IsNullOrWhiteSpace(secretKey)), "The AWS secret key may not be null or blank.");
            Assert.Fail(() => (!String.IsNullOrWhiteSpace(serviceUrl)), "The SimpleDbServiceUrl may not be null or blank.");

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
