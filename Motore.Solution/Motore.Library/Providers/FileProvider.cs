using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.S3;
using Motore.Library.Entities;
using Motore.Library.Exceptions;
using Motore.Utils.Assertions;
using Motore.Utils.Logging;
using Motore.Utils.Text;

namespace Motore.Library.Providers
{
    public class FileProvider
    {
        private S3Client _s3Client = null;

        public virtual List<string> GetLinesFromFile(FileSystemType fst, string location)
        {
            try
            {

                List<string> content = null;
                if (fst == FileSystemType.S3)
                {
                    try
                    {
                        Assert.Fail(() => (Regex.IsUri(location)), String.Format("The location value '{0}' is not a URI and can not be parsed into an Amazon S3 location.", location));
                    }
                    catch (Exception ife)
                    {
                        throw new InvalidFileLocationException(ife.Message);
                    }

                    Stream stream = null;
                    try
                    {
                        stream = this.S3Client.GetStream(location);
                        var reader = new StreamReader(stream);
                        var line = reader.ReadLine();
                        while (!String.IsNullOrWhiteSpace(line))
                        {
                            if (content == null)
                            {
                                content = new List<string>();
                            }
                            content.Add(line);
                            line = reader.ReadLine();
                        }
                    
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                            stream.Dispose();
                        }
                    }
               
                }
                else
                {
                    var msg = String.Format("The file system type '{0}' is not yet supported.", fst);
                    throw new Exception(msg);
                }

                return content;

            }
            catch (Exception exc)
            {
                Log.LogException(exc);
                throw;
            }
        }

        #region Protected Properties

        protected internal virtual S3Client S3Client
        {
            get { return _s3Client ?? (_s3Client = AwsClientFactory.CreateS3Client()); }
        }

        #endregion

    }
}
