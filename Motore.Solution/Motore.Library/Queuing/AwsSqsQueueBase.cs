using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SQS;
using Motore.Library.Aws;
using Motore.Library.Configuration;

namespace Motore.Library.Queuing
{
    public abstract class AwsSqsQueueBase : AwsClient
    {
        /// <summary>
        /// Each queue provider needs to figure out its own service url
        /// </summary>
        protected internal abstract string ServiceUrl { get; }

        protected internal virtual AmazonSQSClient GetClient()
        {
            var sqsConfig = new AmazonSQSConfig
                                {
                                    ServiceURL = this.ServiceUrl
                                };
            var client = new AmazonSQSClient(this.AwsCredentials, sqsConfig);
            return client;
        }

    }
}
