using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Motore.Utils.Exceptions.Web;

namespace Motore.Utils.Web
{
    public class HttpClient
    {   
        public virtual IEnumerable<string> GetCsv(string url)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(String.Format("Getting data at {0}", url));
#endif
            try
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
            catch (System.Net.WebException we)
            {
                var errorResponse = we.Response as HttpWebResponse;
                if ((errorResponse != null)
                    && (errorResponse.StatusCode == HttpStatusCode.NotFound))
                {
                    throw new NotFoundException(url);
                }
                throw;
            }
        }
    }
}
