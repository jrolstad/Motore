using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.S3
{
    public class S3PutInfo
    {
        public virtual string BucketName { get; set; }
        public virtual string Path { get; set; }
        public virtual string ServiceUrl { get; set; }
        public virtual string Scheme { get; set; }

        public virtual string ETag { get; set; }
        public virtual string VersionId { get; set; }
        public virtual string EncryptionMethod { get; set; }
    }
}
