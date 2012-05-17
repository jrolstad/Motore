using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;

namespace Motore.Library.Files
{
    public class UserFilesProvider
    {
        private SimpleDbClient _simpleDbClient = null;

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

        #endregion

    }
}
