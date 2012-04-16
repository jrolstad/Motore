using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.SQS;
using Amazon.SQS.Model;
using Motore.Library.Configuration;
using Motore.Library.Logging;
using Motore.Library.Queuing;
using Motore.Library.Utils.Serialization;

namespace Motore.Library.MarketData
{
    public class MarketDataRequestQueue : AwsSqsQueueBase
    {
        public virtual AddQueueResponse Add(InstrumentMarketDataRequest request)
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

        public virtual List<InstrumentMarketDataRequest> GetRequests()
        {
            var client = this.GetClient();
            var queueReceiveRequest = new ReceiveMessageRequest
                                          {
                                              MaxNumberOfMessages = 10,
                                          };
            var response = client.ReceiveMessage(queueReceiveRequest);
            return Convert(response);
        }

        #region Protected Methods
        
        protected internal virtual List<InstrumentMarketDataRequest> Convert(ReceiveMessageResponse response)
        {
            var list = new List<InstrumentMarketDataRequest>();
            if ((response != null)
                && (response.IsSetReceiveMessageResult()))
            {
                var result = response.ReceiveMessageResult;
                if (result.IsSetMessage())
                {
                    var queueMessages = result.Message;
                    list.AddRange(queueMessages
                        .Select(this.Convert)
                        .Where(marketDataRequest => marketDataRequest != null));
                }
            }

            return list;
        }
    
        protected internal virtual string GetMessageBody(Message message)
        {
            string body = null;
            if (message.IsSetBody())
            {
                body = message.Body;
            }
            if (String.IsNullOrWhiteSpace(body))
            {
                if (message.IsSetMessageId())
                {
                    throw new Exception(
                        String.Format("The body of the message with MessageId '{0}' was unexpectedly blank.",
                                      message.MessageId));
                }
                else
                {
                    throw new Exception("This message has no body and no message ID.");
                }
            }
            return body;
        }

        protected internal virtual InstrumentMarketDataRequest Convert(Message message)
        {
            InstrumentMarketDataRequest marketDataRequest = null;
            try
            {
                var body = this.GetMessageBody(message);
                try
                {
                    marketDataRequest = body.DeserializeFromJson<InstrumentMarketDataRequest>();
                }
                catch (Exception exc)
                {
                    var msg =
                        String.Format(
                            "The message with ID '{0}' could not be deserialized into a MarketDataRequest object.",
                            message.MessageId);
                    Log.LogException(msg);
                    msg = String.Format("Raw body of the message with ID '{0}':\r\n\t{1}", message.MessageId, body);
                    Log.LogException(msg);
                }
            }
            catch (Exception exc)
            {
                Log.LogException(exc);
            }

            return marketDataRequest;
        }

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
        protected internal virtual string GetMessageBody(InstrumentMarketDataRequest request)
        {
            var serialized = request.SerializeToJson<InstrumentMarketDataRequest>();
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
