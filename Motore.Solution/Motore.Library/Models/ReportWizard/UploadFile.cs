using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Motore.Library.Models.ReportWizard
{
    public class UploadFile : Step
    {
        private HttpPostedFile _postedFile = null;
        
        public virtual HttpPostedFile PostedFile
        {
            get { return _postedFile; }
            set { _postedFile = value; }
        }

    }
}
