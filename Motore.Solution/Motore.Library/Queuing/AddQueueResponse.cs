using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Queuing
{
    public class AddQueueResponse
    {
        public virtual string RequestId { get; set; }
        public virtual string MessageId { get; set; }
    }
}
