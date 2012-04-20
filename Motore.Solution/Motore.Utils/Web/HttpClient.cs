using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Motore.Utils.Web
{
    public class HttpClient
    {   
        public virtual IEnumerable<string> GetCsv(string url)
        {
            var results = new List<string>();

            var mimeType = MimeType.CSV;

            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = null;
            try
            {
                responseStream = response.GetResponseStream();
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(responseStream);
                    var line = reader.ReadLine();
                    results.Add(line);
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream.Dispose();
                }
            }

            return results;
        }
    }
}
