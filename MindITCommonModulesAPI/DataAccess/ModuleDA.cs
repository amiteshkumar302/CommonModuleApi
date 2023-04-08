using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util.Constant;
using MySql.Data.MySqlClient;


namespace MindITCommonModulesAPI.DataAccess
{
    public class ModuleDA
    {
        DBHelper dbHelper;
        public ModuleDA(IConfiguration configuration)
        {
            dbHelper = new DBHelper(configuration);
        }
        ///<summary>
        /// 'getModuleDetail' method is used for fetch all module.
        ///Required parameter:null
        /// Response body: it will return all module.
        ///<summary>
        public List<Modules> getModuleDetail()
        {
            List<Modules> module = new List<Modules>();
            try
            {
               
                if (dbHelper.OpenConnection())    // check connection open
                {
                    //Create a data reader and Execute the command
                    MySqlCommand cmd = new MySqlCommand(Sp_contant.GetModuledetail, dbHelper.GetConnection());
                    
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                        
                    while(dataReader.Read()) 
                    {
                                           

                        Modules modules = new Modules();
                        modules.ModuleID = Convert.ToInt32(dataReader[Sp_contant.moduleid])as int? ??Convert.ToInt32("");
                        modules.ModuleName = dataReader[Sp_contant.moduleName].ToString() ?? "";
                        module.Add(modules);
                    }
                    
                    if (module.Count<1)  
                    {

                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.NO_DATA);

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
            return module;
        }
    }

}

