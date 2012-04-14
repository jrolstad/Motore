using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Models.MarketData;
using Motore.Library.Queuing;

namespace Motore.Library.Models
{
    public class ModelFactory
    {
        public static MarketDataRequestResponse Convert(AddQueueResponse input)
        {
            return new MarketDataRequestResponse
                       {
                           MessageId = input.MessageId,
                           RequestId = input.RequestId
                       };
        }
    }
}
