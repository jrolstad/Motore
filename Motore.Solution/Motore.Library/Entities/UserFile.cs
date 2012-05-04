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
        Ok = 1,
        Error = 2
    }

    public enum UserFileType
    {
        Portfolio = 1,
    }

    public enum FileSystemType
    {
        S3 = 1,
    }

    public class UserFile : ISimpleDbEntity
    {
        private DateTime _uploadDate = SystemTime.Now();
        private string _id = Guid.NewGuid().ToString();

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Id")]
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "UserFileType")]
        protected internal virtual string UserFileTypeString
        {
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
