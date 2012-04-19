using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.MarketData;
using Motore.MarketData;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using Motore.Library.Factories;

namespace Motore.Library.Tests.TestCases.Factories
{
    [TestFixture]
    public class ProviderFactoryTests
    {
        [Test]
        public void GetMarketDataProvider_no_constructor_returns_default_provider()
        {
            // arrange
            var defaultProvider = MockRepository.GenerateStub<IMarketDataProvider>();
            var providerFactory = MockRepository.GenerateMock<ProviderFactory>();
            providerFactory.Expect(x => x.DefaultMarketDataProvider).Return(defaultProvider);
            providerFactory.Expect(x => x.GetMarketDataProvider()).CallOriginalMethod(
                OriginalCallOptions.CreateExpectation);

            // act
            providerFactory.GetMarketDataProvider();

            // assert
            providerFactory.VerifyAllExpectations();

        }
    }
}
