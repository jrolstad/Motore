using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Utils.Dates;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Entities
{
    [TestFixture]
    public class UserFileTests
    {
        [TearDown]
        public void TearDown()
        {
            SystemTime.Now = () => DateTime.Now;
        }

        [Test]
        public void Id_is_not_null_or_blank_for_new_object()
        {
            // arrange
            // act
            var file = new UserFile();

            // assert
            Assert.False(String.IsNullOrWhiteSpace(file.Id));
        }

        [Test]
        public void Default_constructor_sets_UploadDate_to_now()
        {
            // arrange
            var dt = new DateTime(2008, 3, 4, 2, 1, 3);
            SystemTime.Now = () => dt;

            // act
            var file = new UserFile();

            // assert
            Assert.That(file.UploadDate, Is.EqualTo(dt));
        }

        [Test]
        public void SimpleDbEntityHelper_reports_primary_key_is_Id()
        {
            var guid = Guid.NewGuid().ToString();
            var request = new UserFile { Id = guid };
            var helper = new SimpleDbEntityHelper();

            // act
            var actual = helper.GetPrimaryKeyValueOfEntity<UserFile>(request);

            // assert
            Assert.That(actual, Is.EqualTo(guid));
        }

        [Test]
        public void SimpleDbEntityHelper_reports_Domain_is_UserFile()
        {
            // arrange
            var expected = "UserFile";
            var helper = new SimpleDbEntityHelper();

            // act
            var actual = helper.GetDomainNameOfEntity<UserFile>();

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
