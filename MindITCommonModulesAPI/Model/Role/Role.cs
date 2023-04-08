//using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MindITCommonModulesAPI.Model.Role
{

    public class RoleDetails
    {

      public int RoleID { get; set; }
      public string? RoleType { get; set; }
      public int IsActive { get; set; }
      public DateTime CreatedOn { get; set; }
      public DateTime UpdatedOn { get; set; }
      
    }
    public class CreateRole
    {
        public string? RoleType { get; set; }
    }
    
    public class UpdateRole
    {
        public int RoleID { get; set; }
        public string? RoleType { get; set; }
    }

    public class UpdateIsActive
    {
        public int RoleID { get; set; }
        public int IsActive { get; set; }
    }


    public class Role
    {
        public int RoleID { get; set; }
    }

    public class RoleDetailsByType
    {

        public int RoleID { get; set; }
        public string? RoleType { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

    }


}
