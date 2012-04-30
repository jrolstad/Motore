using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.Auth;
using Amazon.Runtime;
using Amazon.SimpleDB;

using Motore.Library.Configuration;

namespace Motore.Library.Aws
{
    public abstract class AwsClient
    {
        protected internal AWSCredentials _credentials = null;

        protected AwsClient()
        {
            
        }

        protected AwsClient(string accessKey, string secretKey)
        {
            _credentials = new BasicAWSCredentials(accessKey, secretKey);
        }

        protected internal virtual AWSCredentials AwsCredentials
        {
            get
            {
                if (_credentials == null)
                {
                    var accessKey = Config.AwsAccessKey;
                    var secretKey = Config.AwsSecretKey;
                    _credentials = new BasicAWSCredentials(accessKey, secretKey);
                }
                return _credentials;
            }
        }
    }
}
