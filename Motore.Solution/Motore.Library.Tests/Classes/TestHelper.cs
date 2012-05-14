using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Motore.TestHelpers;

namespace Motore.Library.Tests.Classes
{
    public class TestHelper
    {
        public static Stream GetEmbeddedResourceStream(string fileName)
        {
            var path = String.Format("Motore.Library.Tests.Resources.{0}", fileName);
            var stream =
                EmbeddedResourceHelper.GetStream(
                    Assembly.GetExecutingAssembly(),
                    path);

            return stream;
        }
    }
}
