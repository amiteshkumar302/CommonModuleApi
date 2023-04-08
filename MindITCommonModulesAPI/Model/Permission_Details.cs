using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindITCommonModulesAPI.Model
{
    public class Permission_Details
    {
        public int moduleID { get; set; }
        public int EntityID { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModuleName { get; set; }
    }
}
