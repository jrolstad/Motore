using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Library.Files;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Motore.Library.Tests.TestCases.Files
{
    [TestFixture]
    public class UserFilesProviderTests
    {
        [Test]
        public void GetAll_gets_from_simpledb_client()
        {
            // arrange
            var userFiles = new List<UserFile>();
            string nextToken = "";
            var client = MockRepository.GenerateMock<SimpleDbClient>(null, null);
            client.Expect(x => x.Get<UserFile>(100, ref nextToken)).Return(userFiles);

            var provider = MockRepository.GenerateMock<UserFilesProvider>();
            provider.Expect(p => p.SimpleDbClient).Return(client);
            provider.Expect(p => p.GetAll(nextToken)).CallOriginalMethod(OriginalCallOptions.CreateExpectation);

            // act
            var actual = provider.GetAll(nextToken);
            
            // assert
            Assert.That(actual, Is.EqualTo(userFiles));
            client.VerifyAllExpectations();
            provider.VerifyAllExpectations();
        }
    }
}
