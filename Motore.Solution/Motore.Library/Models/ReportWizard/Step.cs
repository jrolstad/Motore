using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils;

namespace Motore.Library.Models.ReportWizard
{
    public class Step
    {
        protected string _token = null;
        protected int _stepNumber = 1;
        
        public Step()
        {
            _token = GuidUtils.NewToken();
        }

        public Step(int stepNumber) : this()
        {
            _stepNumber = stepNumber;
        }

        public Step(int stepNumber, string token) : this(stepNumber)
        {
            _token = token;
        }

        #region Public Properties

        public virtual string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public virtual int StepNumber
        {
            get { return _stepNumber; }
            set { _stepNumber = value; }
        }

        #endregion
    }
}
