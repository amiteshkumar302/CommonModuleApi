using MindITCommonModulesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindITCommonModulesAPI.Service.Interface
{
     public interface IModuleService 
     {
        List<Modules> GetModules();
    }
         
    
}
