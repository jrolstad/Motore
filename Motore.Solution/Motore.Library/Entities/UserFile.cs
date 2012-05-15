using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.Attributes;
using Motore.Library.Aws.SimpleDb;
using Motore.Utils.Dates;

namespace Motore.Library.Entities
{
    public enum UserFileStatus
    {
        Unknown = 1,
        Pending = 2,
        Ok = 5,
        Error = 99
    }

    public enum UserFileType
    {
        Portfolio = 1,
    }

    public enum FileSystemType
    {
        S3 = 1,
    }

    [SimpleDbDomain(Domain="UserFile")]
    public class UserFile : ISimpleDbEntity
    {
        private DateTime _uploadDate = SystemTime.Now();
        private string _id = Guid.NewGuid().ToString();
        private string _requestId = null;

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Id", IsPrimaryKey=true)]
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "RequestId")]
        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ClientFileName")]
        public virtual string ClientFileName { get; set; }
        
        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name="ContentLength")]
        protected internal virtual string ContentLengthString
        {
            get
            {
                var ret = "";
                if (this.ContentLength > 0)
                {
                    ret = this.ContentLength.ToString();
                }
                return ret;
            }
            set { this.ContentLength = long.Parse(value); }
        }

        public virtual long ContentLength { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "UserFileType")]
        protected internal virtual string UserFileTypeString
        {
            get { return this.UserFileType.ToString(); }
            set
            {
                this.UserFileType =
                    (UserFileType)
                    Enum.Parse(typeof (UserFileType), value, true);
            }
        }
        public virtual UserFileType UserFileType { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "FileSystemType")]
        protected internal virtual string FileSystemTypeString
        {
            set
            {
                this.FileSystemType =
                    (FileSystemType)
                    Enum.Parse(typeof (FileSystemType), value, true);
            }
        }
        public virtual FileSystemType FileSystemType { get; set; }
        
        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Location")]
        public virtual string Location { get; set; }
        
        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "UploadTimestamp")]
        protected internal virtual string UploadTimestampString
        {
            set
            {
                long timestamp;
                if (long.TryParse(value, out timestamp))
                {
                    this.UploadDate = DateUtils.FromTimestamp(timestamp);
                }
            }
        }

        public virtual DateTime UploadDate
        {
            get { return _uploadDate; }
            set { _uploadDate = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Status")]
        protected internal virtual string StatusString
        {
            set
            {
                this.Status =
                    (UserFileStatus)
                    Enum.Parse(typeof (UserFileStatus), value, true);
            }
        }
        public virtual UserFileStatus Status { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreateTimestamp")]
        protected internal virtual string CreateTimestampString
        {
            set
            {
                long timestamp;
                if (!long.TryParse(value, out timestamp))
                {
                    timestamp = DateUtils.ToTimestamp(SystemTime.Now());
                }
                this.CreateTimestamp = timestamp;

            }
        }

        public virtual long CreateTimestamp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifyTimestamp")]
        protected internal virtual string ModifyTimestampString
        {
            set
            {
                long timestamp;
                if (!long.TryParse(value, out timestamp))
                {
                    timestamp = DateUtils.ToTimestamp(SystemTime.Now());
                }
                this.ModifyTimestamp = timestamp;

            }
        }
        public virtual long ModifyTimestamp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreatedBy")]
        public virtual string CreatedBy { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifiedBy")]
        public virtual string ModifiedBy { get; set; }

    }
}
