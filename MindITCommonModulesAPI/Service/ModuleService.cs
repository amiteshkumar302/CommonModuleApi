using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service.Interface;
using MindITCommonModulesAPI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindITCommonModulesAPI.Service
{

    public class ModuleService : IModuleService
    {
        private IConfiguration configuration;

        public ModuleService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Modules> GetModules()         //call method define in IAuthService
        {
            try
            {
                ModuleDA moduleDA = new ModuleDA(configuration);
                List<Modules> moduledetail = moduleDA.getModuleDetail();
                return moduledetail;             // return module list
            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }
    }
}

