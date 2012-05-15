using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Motore.Utils.Dates;
using Motore.Utils.Logging;

namespace Motore.Library.Aws.S3
{
    public class S3Client : AwsClient
    {
        private Amazon.S3.AmazonS3Client _client = null;
        private Amazon.S3.AmazonS3Config _config = null;
        
        protected internal S3Client(AWSCredentials credentials, AmazonS3Config config)
        {
            _credentials = credentials;
            _config = config;
        }

        #region Public Methods

        public virtual bool Exists(string bucket, string path)
        {
            var exists = false;
            var request = this.CreateGetObjectMetadataRequest(bucket, path);
            try
            {
                var metadata = this.GetClient().GetObjectMetadata(request);
                exists = true;
            }
            catch (Amazon.S3.AmazonS3Exception exception)
            {
                var msg = String.Format("'{0}/{1}': {2}", bucket, path, exception.ToString());
                this.LogS3Error(msg);
            }
            return exists;
        }

        public virtual S3PutInfo Save(Stream inputStream, string bucket, string path)
        {
            var request = this.CreatePutObjectRequest(inputStream, bucket, path);
            var response = this.GetClient().PutObject(request);
            return this.CreateS3PutInfo(response, bucket, path);
        }

        #endregion

        #region Protected Methods
        
        protected internal virtual void LogS3Error(string error)
        {
            var message = String.Format("S3: {0}", error);
            Log.LogException(message);
        }

        protected internal virtual GetObjectMetadataRequest CreateGetObjectMetadataRequest(string bucket, string path)
        {
            var request = new GetObjectMetadataRequest
                              {
                                  BucketName = bucket,
                                  Key = path,
                              };

            return request;
        }

        protected internal virtual S3PutInfo CreateS3PutInfo(PutObjectResponse response, string bucket, string path)
        {
            var eTag = response.ETag;
            var versionId = response.VersionId;
            var encryptionMethod = response.ServerSideEncryptionMethod.ToString();
            var putDate = SystemTime.Now();
#if DEBUG
            for (int i = 0; i < response.Headers.Count; i++)
            {
                var h = response.Headers[i];
                System.Diagnostics.Debug.WriteLine(String.Format("{0}: {1}", h, response.Headers[h]));
            }
#endif

    return new S3PutInfo
                       {
                           BucketName = bucket,
                           Path = path,
                           ServiceUrl = _config.ServiceURL,
                           Scheme = _config.CommunicationProtocol.ToString(),
                           ETag = eTag,
                           VersionId = versionId,
                           EncryptionMethod = encryptionMethod,
                           PutDate = putDate,
                       };
        }

        protected internal virtual int PutObjectTimeoutInMilliseconds
        {
            get { return 60000; }
        }

        protected internal virtual PutObjectRequest CreatePutObjectRequest(Stream stream, string bucket, string path)
        {
            var request = new PutObjectRequest
                              {
                                  AutoCloseStream = true,
                                  BucketName = bucket,
                                  Key = path,
                                  InputStream = stream,
                                  Timeout = this.PutObjectTimeoutInMilliseconds,
                                  CannedACL = S3CannedACL.AuthenticatedRead,

                              };

            return request;
        }

        #endregion

        #region Protected Properties

        protected internal virtual AmazonS3Client GetClient()
        {
            return (_client ?? (_client = new AmazonS3Client(_credentials, _config)));
        }

        #endregion
    }
}
