using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeBazi.Store.Domain
{
    public class BanerConstants
    {
        public const string ValidatError_Id = "command.Id > 0";
        public const string ValidatError_Name = "command.Name.IsNullOrEmpty()";
        public const string ValidatError_Description = "command.Description.IsNullOrEmpty()";
        public const string ValidatError_LogoUrl = "command.LogoUrl.IsNullOrEmpty()";
    }
}
