using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model.Role;
using MindITCommonModulesAPI.Service.Interface;


namespace MindITCommonModulesAPI.Service
{
    public class RoleService : IRoleServices
    {
        private readonly IConfiguration configuration;
        public RoleService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// This  method used for return the details of the all role types
        /// </summary>
        /// <returns> This will return the all role type will details</returns>
        public List<RoleDetails> GetAllRoles()
        {
            try
            {
                RoleDA roleDA = new RoleDA(configuration); 
                List<RoleDetails> role = roleDA.GetRoles(); 
                return role;

            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }
        }


        /// <summary>
        /// This method is used for Create new Role Type
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns> Object of RoleDetailsByType Model </returns>
        public RoleDetailsByType CreateNewRole(CreateRole role)
        {
            try
            {
                RoleDA roleDA = new RoleDA(configuration);
                RoleDetailsByType message = roleDA.CreateRole(role);
                return message;
            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }

        }


        /// <summary>
        /// This method will update the Role Type.
        /// </summary>
        /// <param name="RoleID"></param>
        ///  <param name="ROleType"></param>
        /// <returns>This will return the details of updated role type.</returns>
        public RoleDetails UpdateRoleType(UpdateRole role)
        {
            try
            {
                RoleDA roleDA = new RoleDA(configuration);
                RoleDetails message = roleDA.UpdateRoleType(role);
                return message;
            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }

        }


        /// <summary>
        /// This method will update the Role Active/Inactive Status.
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="Status"></param> 
        /// <returns>This will return the details of role type with updated IsActive status.</returns>

        public RoleDetails UpdateRoleStatus(UpdateIsActive status)
        {
            try
            {
                RoleDA roleDA = new RoleDA(configuration);
                RoleDetails message = roleDA.UpdateIsActive(status);
                return message;
            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }
        }


        /// <summary>
        /// This method is used for Delete the role type.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRole(int id)
        {
            try
            {
                RoleDA roleDA = new RoleDA(configuration);
                roleDA.DeleteRole(id);

            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }
        }


        /// <summary>
        /// This method is used for get the role detail of the particular role ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> This will return the Detail of the specific role ID</returns>
        public RoleDetails GetRole(int id)
        {
            try
            {
                RoleDA roleDA = new RoleDA(configuration);
                RoleDetails message = roleDA.GetRole(id);
                return message;
            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }
        }
    }
}
