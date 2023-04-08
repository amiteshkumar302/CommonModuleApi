using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.Model.Role;
//using System.Web.Helpers;


namespace MindITCommonModulesAPI.Service.Interface
{ 
    public interface IRoleServices
    {

        // Make GetAllRoles Interface method to get all RoleType
        List<RoleDetails> GetAllRoles();

        // Make GetRole for get the detail of the role type for a specific role Id.
        RoleDetails GetRole(int id);

        // Make CreateNewRole Interface method to Craete  RoleType
        RoleDetailsByType CreateNewRole(CreateRole role);


        // Make UpdateRoleType Interface method for  RoleType
         RoleDetails UpdateRoleType(UpdateRole role);


        // Make UpdateRoleStatus Interface method for Active/Inactive RoleType
          RoleDetails UpdateRoleStatus(UpdateIsActive status);


        // Make DeleteRoleStatus Interface method for Active/Inactive RoleType
        void DeleteRole(int id);

    }
}
