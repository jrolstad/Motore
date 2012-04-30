using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.Runtime;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Motore.Library.Configuration;
using Motore.Utils.Assertions;

namespace Motore.Library.Aws.SimpleDb
{
    public class SimpleDbClient : AwsClient
    {
        private Amazon.SimpleDB.AmazonSimpleDBClient _client = null;
        private Amazon.SimpleDB.AmazonSimpleDBConfig _config = null;

        protected internal SimpleDbClient(AWSCredentials credentials, AmazonSimpleDBConfig config)
        {
            _credentials = credentials;
            _config = config;
        }

        public virtual void SaveEntity<T>(ISimpleDbEntity entity) where T: ISimpleDbEntity
        {
            var request = this.CreatePutAttributesRequest<T>(entity);
            this.Client.PutAttributes(request);

            throw new NotImplementedException();
        }

        public virtual Domains ListDomains(int maxNumberToRetrive = 100, string nextToken = null)
        {
            var results = new Domains();

            var request = new ListDomainsRequest
                              {
                                  MaxNumberOfDomains = maxNumberToRetrive,
                              };
            
            if (!String.IsNullOrWhiteSpace(nextToken))
            {
                request.NextToken = nextToken;
            }

            var response = this.Client.ListDomains(request);
            if (response.IsSetListDomainsResult())
            {
                var listDomainsResult = response.ListDomainsResult;
                if (listDomainsResult.IsSetDomainName())
                {
                    var domainNames = listDomainsResult.DomainName;
                    foreach (var name in domainNames)
                    {
                        results.Add(new Domain {Name = name});
                    }
                }
            }

            return results;
        }

        #region Protected Properties
        
        protected internal virtual PutAttributesRequest CreatePutAttributesRequest<T>(ISimpleDbEntity entity) where T:ISimpleDbEntity
        {
            throw new NotImplementedException();
        }

        protected internal virtual AmazonSimpleDBClient Client
        {
            get
            {
                return (_client ?? (_client = new AmazonSimpleDBClient(_credentials, _config)));
            }
        }

        #endregion

    }
}
