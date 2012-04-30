using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.SimpleDb
{
    public interface ISimpleDbEntity
    {
        string SimpleDbEntityName { get; }
        string Errors { get; set; }
        long CreateTimestamp { get; set; }
        long ModifyTimestamp { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }

    }
}
