using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Amazon.Runtime;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Motore.Library.Aws.Attributes;
using Motore.Library.Configuration;
using Motore.Utils.Assertions;

namespace Motore.Library.Aws.SimpleDb
{
    public class SimpleDbClient : AwsClient
    {
        private Amazon.SimpleDB.AmazonSimpleDBClient _client = null;
        private Amazon.SimpleDB.AmazonSimpleDBConfig _config = null;
        private SimpleDbEntityHelper _entityHelper = null;

        protected internal SimpleDbClient(AWSCredentials credentials, AmazonSimpleDBConfig config)
        {
            _credentials = credentials;
            _config = config;
        }

        public virtual DomainAction CreateDomain(string name)
        {
            var request = new CreateDomainRequest
                              {
                                  DomainName = name,
                              };
            this.Client.CreateDomain(request);
            return new DomainAction { Action = DomainActionType.Created, DomainName = name };
        }

        public virtual void SaveEntity<T>(ISimpleDbEntity entity) where T: ISimpleDbEntity
        {
            var request = this.CreatePutAttributesRequest<T>(entity);
            var response = this.Client.PutAttributes(request);
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

        #region Protected Methods

        protected internal virtual PutAttributesRequest CreatePutAttributesRequest<T>(ISimpleDbEntity entity) where T:ISimpleDbEntity
        {
            var putAttributeRequest = new PutAttributesRequest();
            
            var domain = this.EntityHelper.GetDomainNameOfEntity<T>(entity);
            var pk = this.EntityHelper.GetPrimaryKeyValueOfEntity<T>(entity);

            putAttributeRequest.DomainName = domain;
            putAttributeRequest.ItemName = pk;
                
            var props = typeof(T).GetProperties().Where(
                prop => System.Attribute.IsDefined(prop, typeof(SimpleDbColumnAttribute)));

            foreach (var prop in props)
            {
                var attributes = (SimpleDbColumnAttribute[])prop.GetCustomAttributes(typeof(SimpleDbColumnAttribute), false);
                var attribute = attributes.First();
                if (attribute.IsPrimaryKey == false)
                {
                    var columnName = attribute.Name;

                    var value = prop.GetValue(entity, (BindingFlags.GetProperty | BindingFlags.Instance), null, null,
                                              CultureInfo.InvariantCulture);

                    var putAttribute = new ReplaceableAttribute
                                           {
                                               Name = columnName,
                                               Replace = true,
                                               Value = (value ?? "").ToString(),
                                           };

                    putAttributeRequest.Attribute.Add(putAttribute);
                }
            }

            return putAttributeRequest;
        }

        #endregion

        #region Protected Properties

        protected internal virtual AmazonSimpleDBClient Client
        {
            get
            {
                return (_client ?? (_client = new AmazonSimpleDBClient(_credentials, _config)));
            }
        }

        protected internal virtual SimpleDbEntityHelper EntityHelper
        {
            get { return _entityHelper ?? (_entityHelper = new SimpleDbEntityHelper()); }
        }

        #endregion

    }
}
