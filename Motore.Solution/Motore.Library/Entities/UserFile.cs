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
        InvalidLocation=98,
        Error = 99
    }

    public enum UserFileType
    {   
        Portfolio = 1,
        Test = 123,
    }

    public enum Custodian
    {
        Unknown = 0,
        Fidelity = 10,
        RBC = 20,
        Schwab = 30
    }

    public enum FileSystemType
    {
        Unknown = 0,
        S3 = 1,
    }

    [SimpleDbDomain(Domain="UserFile")]
    public class UserFile : ISimpleDbEntity
    {
        private string _id = Guid.NewGuid().ToString();
        private string _requestId = null;
        private long _createTimestamp = SystemTime.Now().ToTimestamp();
        private long _modifyTimestamp = SystemTime.Now().ToTimestamp();
        private long _uploadTimestamp = SystemTime.Now().ToTimestamp();
        private FileSystemType _fileSystemType = FileSystemType.Unknown;
        private string _location = null;
        private long _contentLength = -1;
        private Custodian _custodian = Custodian.Unknown;
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
                if (this.ContentLength >= 0)
                {
                    ret = this.ContentLength.ToString();
                }
                return ret;
            }
            set
            {
                long parsed;
                if (long.TryParse(value, out parsed))
                {
                    this.ContentLength = parsed;
                }
            }
        }

        public virtual long ContentLength
        {
            get { return _contentLength; }
            set { _contentLength = value; }
        }

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

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Custodian")]
        protected internal virtual string CustodianString
        {
            get
            {
                var value = "";
                if (this.Custodian != Custodian.Unknown)
                {
                    value = this.Custodian.ToString();
                }
                return value;
            }
            set
            {
                var custodian = Custodian.Unknown;
                if (!String.IsNullOrWhiteSpace(value))
                {
                    custodian = (Custodian) Enum.Parse(typeof (Custodian), value, true);
                }
                this.Custodian = custodian;
            }
        }

        public virtual Custodian Custodian
        {
            get { return _custodian; }
            set { _custodian = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "FileSystemType")]
        protected internal virtual string FileSystemTypeString
        {
            get { return this.FileSystemType.ToString(); }
            set
            {
                FileSystemType fst;
                if (Enum.TryParse(value, true, out fst))
                {
                    this.FileSystemType = fst;
                }
            }
        }

        public virtual FileSystemType FileSystemType
        {
            get { return _fileSystemType; }
            set { _fileSystemType = value; }
        }
        
        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Location")]
        public virtual string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        
        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "UploadTimestamp")]
        protected internal virtual string UploadTimestampString
        {
            get { return this.UploadTimestamp.ToString(); }
            set
            {
                long timestamp;
                if (long.TryParse(value, out timestamp))
                {
                    this.UploadTimestamp = timestamp;
                }
            }
        }

        public virtual long UploadTimestamp
        {
            get { return _uploadTimestamp; }
            set { _uploadTimestamp = value; }
        }

        public virtual string UploadDateString
        {
            get
            {
                var result = "ERROR";
                if (this.UploadTimestamp > 0)
                {
                    result = DateUtils.FromTimestamp(this.UploadTimestamp).ToString("R");
                }
                return result;
            }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Status")]
        protected internal virtual string StatusString
        {
            get { return this.Status.ToString(); }
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
            get { return this.CreateTimestamp.ToString(); }
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

        public virtual long CreateTimestamp
        {
            get { return _createTimestamp; }
            set { _createTimestamp = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifyTimestamp")]
        protected internal virtual string ModifyTimestampString
        {
            get { return this.ModifyTimestamp.ToString(); }
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

        public virtual long ModifyTimestamp
        {
            get { return _modifyTimestamp; }
            set { _modifyTimestamp = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreatedBy")]
        public virtual string CreatedBy { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifiedBy")]
        public virtual string ModifiedBy { get; set; }

    }
}
