using MySql.Data.MySqlClient;

namespace MindITCommonModulesAPI.DataAccess
{
    public class DBHelper
    {

            private MySqlConnection connection;
            
            
            private readonly IConfiguration configuration;
            //Constructor
            public DBHelper(IConfiguration configuration)
            {
            this.configuration = configuration;
            Initialize();

            }

        //Initialize values
        private void Initialize()
            {
            // Connection with database
                string connectionString=configuration["ConnectionString"];
                connection = new MySqlConnection(connectionString);
            }
            public MySqlConnection GetConnection()
        {
            return connection;
        }
            //open connection to database
            public bool OpenConnection()
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (MySqlException ex)
                {
                Console.WriteLine(ex);
                    return false;
                }
            }

            //Close connection
            public bool CloseConnection()
            {
                try
                {
                    connection.Close();
                    return true;
                }
                catch (MySqlException ex)
                {
                Console.WriteLine(ex);
                return false;
                }
            }

            
        }
    }

