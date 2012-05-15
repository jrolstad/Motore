using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.SimpleDb
{
    public class DomainInitializer
    {
        private List<string> _domainNames = new List<string>
                                                {
                                                    "PortfolioCalculationRequest",
                                                    "UserFile",
                                                };

        private SimpleDbClient _simpleDbClient = null;

        public virtual List<DomainAction> Initialize()
        {
            var actions = new List<DomainAction>();

            var newDomains = this.DetermineNewDomainNames(_domainNames);

            foreach (var name in _domainNames)
            {
                var action = this.SimpleDbClient.CreateDomain(name);
                actions.Add(action);
            }

            return actions;
        }

        #region Protected Methods

        protected internal virtual List<string> DetermineNewDomainNames(List<string> proposedNew )
        {
            var actualNewDomainNames = new List<string>();
            var current = this.GetCurrentDomains();
            foreach (var name in proposedNew)
            {
                if (!current.ContainsName(name))
                {
                    actualNewDomainNames.Add(name);
                }
            }

            return actualNewDomainNames;
        }

        protected internal virtual Domains GetCurrentDomains()
        {
            var max = this.DomainMaxFetchCount;
            var currentDomains = new Domains();
            string nextToken = null;
            do
            {
                var fetched = this.SimpleDbClient.ListDomains(maxNumberToRetrive: max);
                nextToken = fetched.NextToken;
                currentDomains.AddRange(fetched);

            } while (nextToken != null);

            return currentDomains;
        }

        #endregion

        #region Protected Properties

        protected internal virtual int DomainMaxFetchCount
        {
            get { return 100; }
        }

        protected internal virtual SimpleDbClient SimpleDbClient
        {
            get { return _simpleDbClient ?? ( _simpleDbClient = AwsClientFactory.CreateSimpleDbClient()); }
        }

        #endregion

    }
}
