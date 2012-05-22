using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Library.Exceptions;
using Motore.Library.Providers;

namespace Motore.Library.Files
{
    public class UserFilesProvider
    {
        private SimpleDbClient _simpleDbClient = null;
        private FileProvider _fileProvider = null;

        public virtual RawFileModel GetRawFileContent(string id)
        {
            var userFile = this.SimpleDbClient.Get<UserFile>(id, true);
            var fst = userFile.FileSystemType;
            var location = userFile.Location;

            var model = new RawFileModel
                            {
                                UserFileId = id,
                                FileSystemType = fst,
                                Location = location,
                            };

            try
            {
                var contents = this.FileProvider.GetLinesFromFile(fst, location);
                model.Lines = contents;

                return model;
            }
            catch (InvalidFileLocationException)
            {
                userFile.Status = UserFileStatus.InvalidLocation;
                this.SimpleDbClient.SaveEntity<UserFile>(userFile);
                throw;
            }

        }

        public virtual UserFiles GetAll(string nextToken = null)
        {
            var list = this.SimpleDbClient.Get<UserFile>(100, ref nextToken);
            var userFiles = new UserFiles
                                {
                                    NextToken = nextToken
                                };
            userFiles.AddRange(list);

            return userFiles;
        }

        public virtual UserFile Get(string id)
        {
            return this.SimpleDbClient.Get<UserFile>(id, true);
        }

        #region Protected Properties

        protected internal virtual SimpleDbClient SimpleDbClient
        {
            get { return _simpleDbClient ?? (_simpleDbClient = AwsClientFactory.CreateSimpleDbClient()); }
        }

        protected internal virtual FileProvider FileProvider
        {
            get { return _fileProvider ?? (_fileProvider = new FileProvider()); }
        }

        #endregion

    }
}
