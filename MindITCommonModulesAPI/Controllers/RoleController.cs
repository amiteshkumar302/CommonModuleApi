using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Model.Role;
using MindITCommonModulesAPI.Service;
using MindITCommonModulesAPI.Service.Interface;

namespace MindITCommonModulesAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        readonly IConfiguration configuration;
        readonly CustomExceptionHandler customExceptionHandler;
        public RoleController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.customExceptionHandler = new CustomExceptionHandler();
        }


        /// <summary>
        /// This  method used for return the details of the all role types
        /// </summary>
        /// <returns> This will return the all role type will details</returns>

        [HttpGet]
        [Route("roles")]
        public IActionResult GetRoles()
        {
            try
            {
                IRoleServices authService = new RoleService(configuration);
                return (Ok(authService.GetAllRoles()));
            }


            // If any error occur from database, throw to the custom API Exception
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };

            }
        }

        /// <summary>
        /// This method is used for get the role detail of the particular role ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> This will return the Detail of the specific role ID</returns>

        [HttpGet]
        [Route("role/{id}")]
        public IActionResult GetRole(int id)
        {
            try
            {
                IRoleServices authService = new RoleService(configuration);
                return (Ok(authService.GetRole(id)));
            }
            catch(CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };
            }
        }
        /// <summary>
        /// This method is used for Create new Role Type
        /// </summary>
        /// <param name="role(roleType)"></param>
        /// <returns> Object of RoleDetailsByType Model </returns>

        [HttpPost]
        [Route("role")]
        public IActionResult CreateRole([FromBody] CreateRole role)
        {
            try
            {
                // This will check if the role model is not null
                if (role == null)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.BAD_REQUEST);
                }

                // This will check if the role Type is not null or white Space
                else if (string.IsNullOrWhiteSpace(role.RoleType))
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.BAD_REQUEST);
                }

                // Call the method
                else
                {
                    try
                    {

                        IRoleServices authService = new RoleService(configuration);
                        return Created("",authService.CreateNewRole(role));
                    }

                    // If any error occur from database, throw to the custom API Exception
                    catch (CustomAPIExcpetion ex)
                    {
                        CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());
                        return new ObjectResult(exceptionResponse.errorResponse)
                        {
                            StatusCode = (int)exceptionResponse.httpStatusCode
                        };

                    }
                }
            }

            // If any error occur from request body, throw the the custom API exception 
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };
            }


        }


        /// <summary>
        /// This method will update the Role Type.
        /// </summary>
        /// <param name="role(roleType)"></param>
        /// <returns>This will return the details of updated role type.</returns> 

        [HttpPut]
        [Route("role/{id}")]
        public IActionResult UpdateRoleType(int id,[FromBody] UpdateRole role)
        {
            try
            {
                if (id == role.RoleID)
                {
                    // Check if the requested roleType is not null or whiteSpace
                    if (string.IsNullOrEmpty(role.RoleType))
                    {

                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.BAD_REQUEST);
                    }
                    else
                    {
                        try
                        {
                            IRoleServices authService = new RoleService(configuration);
                            return Ok(authService.UpdateRoleType(role));

                        }

                        // If any error occur from database, throw to the custom API Exception
                        catch (CustomAPIExcpetion ex)
                        {
                            CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                            return new ObjectResult(exceptionResponse.errorResponse)
                            {
                                StatusCode = (int)exceptionResponse.httpStatusCode
                            };

                        }
                    }
                }
                else
                {
                    throw new CustomAPIExcpetion (CustomErrorCodeEnum.MISMATCH);
                }
            }

            // If any error occur from request body, throw the the custom API exception 
            catch (CustomAPIExcpetion ex)
            {

                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };


            }
        }




        /// <summary>
        /// This method will update the Role Active/Inactive Status.
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns>This will return the details of role type with updated IsActive status.</returns> 

        [HttpPut]
        [Route("role/{id}/status")]
        public IActionResult UpdateIsActiveStatus(int id,[FromBody] UpdateIsActive isActive)
        {
            try
            {
                if (id == isActive.RoleID)
                {
                    //Check if the status is either 0 or 1
                    if (isActive.IsActive == 0 || isActive.IsActive == 1)
                    {
                        try
                        {
                            IRoleServices authService = new RoleService(configuration);
                            return Ok(authService.UpdateRoleStatus(isActive));
                        }

                        // If any error occur from database, throw to the custom API Exception
                        catch (CustomAPIExcpetion ex)
                        {
                            CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                            return new ObjectResult(exceptionResponse.errorResponse)
                            {
                                StatusCode = (int)exceptionResponse.httpStatusCode
                            };

                        }

                    }


                    // if the status is either 0 or 1, the
                    else
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.BAD_STATUS_REQUEST);
                    }
                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.MISMATCH);
                }
            }

            // If any error occur from request body, throw the the custom API exception 
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };
            }

        }


        /// <summary>
        /// This method is used for Delete the role type.
        /// </summary>
        /// <param name="roleId"></param>

        [HttpDelete]
        [Route("role/{id}")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                IRoleServices authService = new RoleService(configuration);
                authService.DeleteRole(id);
                return Ok();

            }

            // If any error occur from database, throw to the custom API Exception
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());
                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };
            }
        }



    }
}
