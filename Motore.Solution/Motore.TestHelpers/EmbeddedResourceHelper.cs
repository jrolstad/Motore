using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Motore.TestHelpers
{
    public class EmbeddedResourceHelper
    {
        public static Stream GetStream(Assembly assembly, string path)
        {
            var stream = assembly.GetManifestResourceStream(path);
            if (stream == null)
            {
                throw new Exception(String.Format("The path '{0}' could not be found.", path));
            }
            return stream;
        }

        public static string GetText(Assembly assembly, string path)
        {
            Stream inFile = null;
            string text = null;
            try
            {
                inFile = assembly.GetManifestResourceStream(path);
                if (inFile == null)
                {
                    throw new Exception(String.Format("The path '{0}' could not be found.", path));
                }
                var reader = new StreamReader(inFile);
                text = reader.ReadToEnd();
            }
            finally
            {
                if (inFile != null)
                {
                    inFile.Close();
                    inFile.Dispose();
                }
            }

            return text;

        }
    }
}
