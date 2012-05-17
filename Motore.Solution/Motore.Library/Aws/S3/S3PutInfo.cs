using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.S3
{
    public class S3PutInfo
    {
        private string _scheme = null;
        private string _serviceUrl = null;

        public virtual string BucketName { get; set; }
        public virtual string Path { get; set; }
        public virtual string ServiceUrl
        {
            get { return (_serviceUrl ?? "").ToLower(); }
            set { _serviceUrl = value; }
        }

        public virtual string Scheme
        {
            get { return (_scheme ?? "").ToLower(); }
            set { _scheme = value; }
        }

        public virtual string ETag { get; set; }
        public virtual string VersionId { get; set; }
        public virtual string EncryptionMethod { get; set; }
        public virtual DateTime PutDate { get; set; }

        public virtual string Uri
        {
            get
            {
                const string fmt = "{0}://{1}/{2}/{3}";
                var uri = String.Format(fmt, this.Scheme, this.ServiceUrl, this.BucketName, this.Path);
                return uri;
            }
        }
    }
}
