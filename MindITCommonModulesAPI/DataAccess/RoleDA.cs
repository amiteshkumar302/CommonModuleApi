using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Model.Role;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
//using System.Web.Mvc;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace MindITCommonModulesAPI.DataAccess
{
    public class RoleDA
    {
        readonly DBHelper dbHelper;
        string message = "";
        RoleDetails result;
        RoleDetailsByType output;


        MySqlDataReader? dataReader;


        // Call constructor and initialize the valuese
        public RoleDA(IConfiguration configuration)
        {
            dbHelper = new DBHelper(configuration);
        }


        /// <summary>
        /// This  method used for return the details of the all role types
        /// </summary>
        /// <returns> This will return the all role type will details</returns>
        public List<RoleDetails> GetRoles()
        {
            // Make role list to store the valuese
            List<RoleDetails> roleDetails = new List<RoleDetails>();  
            try
            {
                if (dbHelper.OpenConnection()) 
                {
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.SPGetRoles, dbHelper.GetConnection()); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        RoleDetails role = new RoleDetails();


                        // Read all the valuese and store in the List 

                         role.RoleID = Convert.ToInt32(dataReader[Column.RoleID]) as int? ?? Convert.ToInt32("");    
                         role.RoleType = dataReader[Column.RoleType].ToString() ?? "";
                         role.IsActive=Convert.ToInt32(dataReader[Column.Status])as int? ??Convert.ToInt32("");
                         role.CreatedOn = Convert.ToDateTime(dataReader[Column.CreatedOn]) as DateTime? ??Convert.ToDateTime("");
                         role.UpdatedOn = Convert.ToDateTime(dataReader[Column.UpdatedOn]) as DateTime? ?? Convert.ToDateTime(""); 
                        roleDetails.Add(role);
                    }

                    dataReader.Close();   

                    // Check if the List role is not empty or null
                    if (roleDetails.Count < 1)
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_VALUE);   
                    }
                    
                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);  
                }
            }
            catch (CustomAPIExcpetion customAPIException) 
            {
                throw customAPIException;
            }
            finally   
            {

                dbHelper.CloseConnection();
            }
            return roleDetails;  
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
                if (dbHelper.OpenConnection()) 
                {
                    Role role = new Role();
                    role.RoleID = id;
                    result = GetRoleByID(role);
                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);
                }
            }
            catch (CustomAPIExcpetion customAPIExcpetion)
            {
                throw customAPIExcpetion;
            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return result;

        }



        /// <summary>
        /// This method is used for Create new Role Type
        /// </summary>
        /// <param name="role(roleType)"></param>
        /// <returns> Object of RoleDetailsByType Model </returns>
        public RoleDetailsByType CreateRole(CreateRole role)
        {
            try
            {
                if (dbHelper.OpenConnection())  
                {
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.SPCreateRole, dbHelper.GetConnection()); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    string RoleName = role.RoleType;
                    cmd.Parameters.AddWithValue(Parameter.RoleType,RoleName?.Trim());  // Add required parameter of role type

                    MySqlParameter param_msg = new MySqlParameter(SPMessage.MSG, MySqlDbType.VarChar);  
                    param_msg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param_msg);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();      
                    message = dataReader.GetString(SPMessage.Message);
                    dataReader.Close();   

                    // Check if the role type already created or present in database
                    if (message.Equals("1"))
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.CONFLICT); // Throw to the custom exception of conflict
                    }

                    // if data is not present in the database then create new roleType
                    else if (message.Equals("0"))
                    {
                        CreateRole roleType = new CreateRole();
                        roleType.RoleType = role.RoleType;
                        output=GetRoleByRoleType(roleType);  // Call the 'Method_GetRoleByType' -> to get the update role type details

                    }

                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);  
                }
            }
            catch (CustomAPIExcpetion customAPIExcpetion)  
            {
                throw customAPIExcpetion;
            }
            finally
            {
                dbHelper.CloseConnection();   
            }
            return output; 
        }



        /// <summary>
        /// This method will update the Role Type.
        /// </summary>
        /// <param name="role(roleId,roleType)"></param>
        /// <returns>This will return the details of updated role type.</returns>

        public RoleDetails UpdateRoleType(UpdateRole role)
        {
            try
            {
                if (dbHelper.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.SPUpdateRole, dbHelper.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Parameter.RoleID, role.RoleID); // Added required parameter of role ID
                    cmd.Parameters.AddWithValue(Parameter.RoleType, role.RoleType);  // Added required parameter of updated role type

                    MySqlParameter param_msg = new MySqlParameter(SPMessage.MSG, MySqlDbType.VarChar);
                    param_msg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param_msg);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    message = dataReader.GetString(SPMessage.Message);
                    dataReader.Close();

                    // Check if the role ID id is not exists 
                    if (message.Equals("1"))
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.NOT_FOUND);
                    }

                    if (message.Equals("2"))
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.CONFLICT);
                    }

                    // Check if the role ID is exists in the database
                    else if (message.Equals("0"))
                    {
                        Role updateRole = new Role();
                        updateRole.RoleID = role.RoleID;
                        result= GetRoleByID(updateRole); // Call the 'GetRoleTypeDetails' to get the details of the updated role type
                    }
                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);
                }

            }
            catch (CustomAPIExcpetion customAPIExcpetion)
            {
                throw customAPIExcpetion;
            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return result;
        }

        /// <summary>
        /// This method will update the Role Active/Inactive Status.
        /// </summary>
        /// <param name="status(RoleID,status)"></param> 
        /// <returns>This will return the details of role type with updated IsActive status.</returns>

        public RoleDetails UpdateIsActive(UpdateIsActive status)
        {
            try
            {
                if (dbHelper.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.SPActiveStatus, dbHelper.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Parameter.RoleID, status.RoleID);      // 'Role ID' Parameter addded.
                    cmd.Parameters.AddWithValue(Parameter.IsActive, status.IsActive);  // 'Active/Inactive' Parameter status added.

                    MySqlParameter param_msg = new MySqlParameter(SPMessage.MSG, MySqlDbType.VarChar);
                    param_msg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param_msg);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    message = dataReader.GetString(SPMessage.Message);
                    dataReader.Close();

                    // This will check if the any user is active with this role type
                    if (message.Equals("2"))
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.METHOD_NOT_ALLOWED);
                    }

                    //This will check if the data is present in the database or not
                    else if (message.Equals("1"))
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.NOT_FOUND);
                    }

                    // This will check for the specified role ID and update the Active/Inactive status
                    else if (message.Equals("0"))
                    {
                        Role role = new Role();
                        role.RoleID = status.RoleID;
                        result = GetRoleByID(role);   // Call the 'GetRoleTypeDetails' to get the details of the updated role type
                    }
                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);
                }
            }
            catch (CustomAPIExcpetion customAPIExcpetion)
            {
                throw customAPIExcpetion;
            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return result;
        }

        /// <summary>
        /// This method is used for Delete the role type.
        /// </summary>
        /// <param name="roleId"></param>

        public void DeleteRole(int roleId)
        {
            try
            {

                if (dbHelper.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.SPDeleteRole, dbHelper.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Parameter.RoleID, roleId); // Add parameter of 'Role Type'

                    MySqlParameter param_msg = new MySqlParameter(SPMessage.MSG, MySqlDbType.VarChar);
                    param_msg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param_msg);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    message = dataReader.GetString(SPMessage.Message);
                    dataReader.Close();

                    // Check if the roleType is Present or not
                    if (message.Equals("0"))
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.NOT_FOUND);
                    }

                    // check for successfully delete
                    else if (message.Equals("1"))
                    {
                        // success
                    }

                    // Check if any user is present with this role type
                    else if (message.Equals("2"))
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.METHOD_NOT_ALLOWED);
                    }
                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);
                }


            }

            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;

            }

            finally
            {
                dbHelper.CloseConnection();
            }


        }

        /// <summary>
        /// This  method is common method used for fetch all details of role type by using roleID
        /// </summary>
        /// <param name="role(roleId)"></param>
        /// <returns> This will return all details of role type</returns>
        public RoleDetails GetRoleByID(Role role)
        {
            RoleDetails? roleDetails = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(ProcedureName.SPGetRole, dbHelper.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Parameter.RoleID, role.RoleID);

                dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    roleDetails = new RoleDetails();
                    roleDetails.RoleID = Convert.ToInt32(dataReader[Column.RoleID]) as int? ?? Convert.ToInt32("");
                    roleDetails.RoleType = dataReader[Column.RoleType].ToString() ?? "";
                    roleDetails.IsActive = Convert.ToInt32(dataReader[Column.Status]) as int? ?? Convert.ToInt32("");
                    roleDetails.CreatedOn = Convert.ToDateTime(dataReader[Column.CreatedOn]) as DateTime? ?? Convert.ToDateTime("");
                    roleDetails.UpdatedOn = Convert.ToDateTime(dataReader[Column.UpdatedOn]) as DateTime? ?? Convert.ToDateTime("");
                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NOT_FOUND);
                }
            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return roleDetails;
        }


        /// <summary>
        /// This  method is common method used for fetch all details of role type by using role type
        /// </summary>
        /// <param name="role(roleType)"></param>
        /// <returns>This will return all details of role type</returns>
        public RoleDetailsByType GetRoleByRoleType(CreateRole role)
        {
            RoleDetailsByType? roleDetails = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(ProcedureName.SPGetRoleDetails, dbHelper.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Parameter.RoleType, role.RoleType);

                dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    roleDetails = new RoleDetailsByType();
                    roleDetails.RoleID = Convert.ToInt32(dataReader[Column.RoleID]) as int? ?? Convert.ToInt32("");
                    roleDetails.RoleType = dataReader[Column.RoleType].ToString() ?? "";
                    roleDetails.IsActive = Convert.ToInt32(dataReader[Column.Status]) as int? ?? Convert.ToInt32("");
                    roleDetails.CreatedOn = Convert.ToDateTime(dataReader[Column.CreatedOn]) as DateTime? ?? Convert.ToDateTime("");
                }
            }
            catch (CustomAPIExcpetion customAPIException)
            {
                throw customAPIException;
            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return roleDetails;
        }
    }
}
