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

        protected internal virtual string GetPrimaryKeyValueOfEntity<T>(ISimpleDbEntity entity)
        {
            string primaryKeyValue = null;
            string primaryKeyPropertyName = null;

            var columnProps = typeof(T).GetProperties().Where(
                prop => System.Attribute.IsDefined(prop, typeof(SimpleDbColumnAttribute)));

            var numberOfProperties = 0;

            foreach (var prop in columnProps)
            {
                var primaryKeyAttribute =
                    ((SimpleDbColumnAttribute[]) prop.GetCustomAttributes(typeof (SimpleDbColumnAttribute), false))
                        .ToList()
                        .FirstOrDefault(x => x.IsPrimaryKey);
                if (primaryKeyAttribute != null)
                {
                    primaryKeyPropertyName = prop.Name;
                    primaryKeyValue = prop.GetValue(entity, (BindingFlags.GetProperty | BindingFlags.Instance), null,
                                                    null,
                                                    CultureInfo.InvariantCulture) as string;


                }
                numberOfProperties++;
            }

            if (numberOfProperties <= 0)
            {
                var msg =
                String.Format(
                    "The ISimpleDbEntity of type '{0}' does not contain any properties decorated with the SimpleDbColumnAttribute.",
                    (typeof(T)).ToString());
                throw new Exception(msg);
            }

            if (String.IsNullOrWhiteSpace(primaryKeyValue))
            {
                var msg =
                String.Format(
                    "The ISimpleDbEntity of type '{0}' has a property '{1}' decorated with a SimpleDbColumnAttribute of type IsPrimaryKey=true, but the value is null or blank.",
                    (typeof(T)).ToString(), primaryKeyPropertyName);
                throw new Exception(msg);
            }

            return primaryKeyValue;
        }

        protected internal virtual string GetDomainNameOfEntity<T>(ISimpleDbEntity entity)
        {
            var domainAttribute =
                (SimpleDbDomainAttribute)typeof (T).GetCustomAttributes(typeof (SimpleDbDomainAttribute), false).FirstOrDefault();
            if (domainAttribute == null)
            {
                var pkValue = this.GetPrimaryKeyValueOfEntity<T>(entity);
                var msg =
                    String.Format(
                        "The ISimpleDbEntity of type '{0}' identified by the primary key '{1}' is not decorated with a SimpleDbDomain attribute.",
                        (typeof (T)).ToString(), pkValue);
                throw new Exception(msg);
            }
            return domainAttribute.Domain;
        }

        protected internal virtual PutAttributesRequest CreatePutAttributesRequest<T>(ISimpleDbEntity entity) where T:ISimpleDbEntity
        {
            var putAttributeRequest = new PutAttributesRequest();
            
            var domain = this.GetDomainNameOfEntity<T>(entity);

            putAttributeRequest.DomainName = domain;
                
            var props = typeof(T).GetProperties().Where(
                prop => System.Attribute.IsDefined(prop, typeof(SimpleDbColumnAttribute)));

            foreach (var prop in props)
            {
                var attributes = (SimpleDbColumnAttribute[])prop.GetCustomAttributes(typeof(SimpleDbColumnAttribute), false);
                var columnName = attributes.First().Name;

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

            return putAttributeRequest;
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
