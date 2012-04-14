using System;
using Amazon.SQS;
using Amazon.SQS.Model;
using Motore.Library.Configuration;
using Motore.Library.Queuing;
using Motore.Library.Utils.Serialization;

namespace Motore.Library.MarketData
{
    public class MarketDataRequestQueue : AwsSqsQueueBase
    {
        public virtual AddQueueResponse Add(MarketDataRequest request)
        {
            var client = this.GetClient();
            var messageBody = this.GetMessageBody(request);
            var sendMessageRequest = new SendMessageRequest
                                         {
                                             MessageBody = messageBody,
                                         };
            var response = client.SendMessage(sendMessageRequest);
            return this.Convert(response);
        }

        #region Protected Methods
        
        protected internal virtual AddQueueResponse Convert(Amazon.SQS.Model.SendMessageResponse response)
        {
            var addQueueResponse = new AddQueueResponse();
            if (response.IsSetSendMessageResult())
            {
                var sendMessageResult = response.SendMessageResult;
                if (sendMessageResult.IsSetMessageId())
                {
                    addQueueResponse.MessageId = sendMessageResult.MessageId;
                }
            }
            if (response.IsSetResponseMetadata())
            {
                var metadata = response.ResponseMetadata;
                if (metadata.IsSetRequestId())
                {
                    addQueueResponse.RequestId = metadata.RequestId;
                }
            }

            return addQueueResponse;
        }
        protected internal virtual string GetMessageBody(MarketDataRequest request)
        {
            var serialized = request.SerializeToJson<MarketDataRequest>();
            return serialized;
        }

        #endregion

        #region Protected Properties

        protected internal override string ServiceUrl
        {
            get { return Config.MarketDataRequestQueueUrl; }
        }

        #endregion
    }
}
