using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Models.ReportWizard;

namespace Motore.Library.Factories
{
    public class ReportWizardStepFactory
    {
        public virtual Step GetStep(int stepNumber, string token)
        {
            return new Step(stepNumber, token);
        }
    }
}
