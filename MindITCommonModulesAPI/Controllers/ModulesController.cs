using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service;
using MindITCommonModulesAPI.Service.Interface;
using System.Reflection;


namespace MindITCommonModulesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        IConfiguration configuration;
        CustomExceptionHandler customExceptionHandler;
        public ModulesController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.customExceptionHandler = new CustomExceptionHandler();
        }

        /* Make the 'Get' method is used for fetch all module.*/
            
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IModuleService ModuleService = new ModuleService(configuration);
                return Ok(ModuleService.GetModules());  //call the method getmodules.

            }
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
