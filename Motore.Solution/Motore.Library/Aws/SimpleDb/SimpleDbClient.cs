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

        public virtual T Get<T>(string primaryKey, bool consistentRead = false) where T: ISimpleDbEntity
        {
            var getAttributesRequest = this.CreateGetAttributesRequest<T>(primaryKey, consistentRead);
            var response = this.GetAttributesResponse(getAttributesRequest);
            var item = CreateItemFromResponse<T>(response);

            return item;
    
        }

        public virtual List<T> Get<T>(int max, ref string nextToken) where T: ISimpleDbEntity
        {
            var selectRequest = this.CreateSelectRequest<T>(max, nextToken);
            var response = this.GetSelectResponse(selectRequest);
            var list = CreateListFromResponse<T>(response);
            
            return list;

        }
        
        public virtual DomainAction CreateDomain(string name)
        {
            var request = new CreateDomainRequest
                              {
                                  DomainName = name,
                              };
            this.GetClient().CreateDomain(request);
            return new DomainAction { Action = DomainActionType.Created, DomainName = name };
        }

        public virtual SaveEntityInfo SaveEntity<T>(ISimpleDbEntity entity) where T: ISimpleDbEntity
        {
            var request = this.CreatePutAttributesRequest<T>(entity);
            var response = this.PutAttributes(request);

            return new SaveEntityInfo
                       {
                           EntityType = typeof (T),
                           PrimaryKey = request.ItemName,
                       };
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

            var response = this.GetClient().ListDomains(request);
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

        protected internal virtual PutAttributesResponse PutAttributes(PutAttributesRequest request)
        {
            return this.GetClient().PutAttributes(request);
        }

        protected internal virtual SelectResponse GetSelectResponse(SelectRequest request)
        {
            return this.GetClient().Select(request);
        }

        protected internal virtual GetAttributesResponse GetAttributesResponse(GetAttributesRequest request)
        {
            return this.GetClient().GetAttributes(request);
        }

        protected internal virtual List<T> CreateListFromResponse<T>(SelectResponse response) where T: ISimpleDbEntity
        {
            var list = new List<T>();

            if (response.IsSetSelectResult())
            {
                var selectResult = response.SelectResult;
                if (selectResult.IsSetItem())
                {
                    var items = selectResult.Item;
                    foreach (var item in items)
                    {
                        if (item.IsSetAttribute())
                        {
                            var attributes = item.Attribute;
                            var obj = Activator.CreateInstance<T>();
                            LoadFromSimpleDbAttributes(ref obj, attributes);
                            list.Add(obj);
                        }
                    }
                }
            }

            return list;
        }

        protected internal virtual T CreateItemFromResponse<T>(GetAttributesResponse response) where T : ISimpleDbEntity
        {
            T item = default(T);

            if (response.IsSetGetAttributesResult())
            {
                var getAttributesResult = response.GetAttributesResult;
                if (getAttributesResult.IsSetAttribute())
                {
                    var attributes = getAttributesResult.Attribute;
                    var obj = Activator.CreateInstance<T>();
                    LoadFromSimpleDbAttributes(ref obj, attributes);
                    item = obj;
                }
            }

            return item;
        }

        protected internal virtual GetAttributesRequest CreateGetAttributesRequest<T>(string primaryKey, bool consistentRead = false)
        {
            var domainName = this.EntityHelper.GetDomainNameOfEntity<T>();
            var getAttributesRequest = new GetAttributesRequest
            {
                DomainName = domainName,
                ItemName = primaryKey,
                ConsistentRead = consistentRead,
            };

            return getAttributesRequest;

        }

        protected internal virtual SelectRequest CreateSelectRequest<T>(int max, string nextToken)
        {
            var selectStatement = this.CreateSelectStatement<T>(max);
            var selectRequest = new SelectRequest
            {
                SelectExpression = selectStatement,
            };
            if (!String.IsNullOrWhiteSpace(nextToken))
            {
                selectRequest.NextToken = nextToken;
            }

            return selectRequest;

        }

        protected internal virtual void LoadFromSimpleDbAttributes<T>(ref T obj, List<Amazon.SimpleDB.Model.Attribute> attributes) where T: ISimpleDbEntity
        {
            var type = typeof (T);
            var props = typeof(T)
                .GetProperties()
                .Where(prop => System.Attribute.IsDefined(prop, typeof(SimpleDbColumnAttribute)));
            
            foreach (var prop in props)
            {
                var attr =
                    (SimpleDbColumnAttribute)
                    ((prop.GetCustomAttributes(typeof (SimpleDbColumnAttribute), false)).FirstOrDefault());

                if (attr != null)
                {
                    var columnName = attr.Name;
                    var simpleDbColumn =
                        attributes.FirstOrDefault(
                            c => c.Name.Equals(columnName, StringComparison.CurrentCultureIgnoreCase));
                    if (simpleDbColumn != null)
                    {
                        var value = simpleDbColumn.IsSetValue() ? simpleDbColumn.Value : null;
                        prop.SetValue(obj, value, (BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.IgnoreCase), null, null, CultureInfo.CurrentCulture);
                    }
                }
            }
        }

        protected internal virtual string CreateSelectStatement<T>(int max)
        {
            var domainName = this.EntityHelper.GetDomainNameOfEntity<T>();
            var statementFmt = "SELECT * FROM {0} LIMIT {1}";
            var statement = String.Format(statementFmt, domainName, max);
            return statement;
        }

        protected internal virtual PutAttributesRequest CreatePutAttributesRequest<T>(ISimpleDbEntity entity) where T:ISimpleDbEntity
        {
            var putAttributeRequest = new PutAttributesRequest();
            
            var domain = this.EntityHelper.GetDomainNameOfEntity<T>();
            var pk = this.EntityHelper.GetPrimaryKeyValueOfEntity<T>(entity);

            putAttributeRequest.DomainName = domain;
            putAttributeRequest.ItemName = pk;
                
            var props = typeof(T)
                .GetProperties()
                .Where(prop => System.Attribute.IsDefined(prop, typeof(SimpleDbColumnAttribute)));

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

        protected internal virtual AmazonSimpleDBClient GetClient()
        {
            return (_client ?? (_client = new AmazonSimpleDBClient(_credentials, _config)));
        }

        protected internal virtual SimpleDbEntityHelper EntityHelper
        {
            get { return _entityHelper ?? (_entityHelper = new SimpleDbEntityHelper()); }
        }

        #endregion

        
    }
}
