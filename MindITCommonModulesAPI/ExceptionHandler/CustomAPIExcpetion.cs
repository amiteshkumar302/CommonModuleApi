namespace MindITCommonModulesAPI.ExceptionHandler
{
    public class CustomAPIExcpetion : Exception
    {
      private readonly  CustomErrorCodeEnum error;
        public CustomAPIExcpetion(CustomErrorCodeEnum error)
        {
            this.error = error;
        }
        public CustomErrorCodeEnum getError()
        {
            return error;
        }
    }
}
