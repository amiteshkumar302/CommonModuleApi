using MindITCommonModulesAPI.ExceptionHandler;
using System.Net;
using System.Reflection;

namespace MindITCommonModulesAPI.Util
{

    public static class EnumUtilities
    {
        #region [ + Extension Methods ]

        #region [ GetDescription ]

        public static int GetErrorId(this Enum constant)
        {
            return EnumUtilities.GetEnumNamedConstantAttribute(constant).ErrorId;
        }
        public static string? GetErrorMessage(this Enum constant)
        {
            return EnumUtilities.GetEnumNamedConstantAttribute(constant).ErrorMessage;
        }
        public static ErrorSeverityEnum GetErrorSeverity(this Enum constant)
        {
            return EnumUtilities.GetEnumNamedConstantAttribute(constant).ErrorSeverity;
        }
        public static string? GetErrorCode(this Enum constant)
        {
            return EnumUtilities.GetEnumNamedConstantAttribute(constant).ErrorCode;
        }
        public static HttpStatusCode GetHttpStatus(this Enum constant)
        {
            return EnumUtilities.GetEnumNamedConstantAttribute(constant).HttpStatus;

        }


        #endregion

        #endregion

        #region [ + Static Methods ]

        #region [ GetEnumerable ]

        public static IEnumerable<CustomErrorCodeAttribute> GetEnumerable<T>()
        {
            T instancia = Activator.CreateInstance<T>();

            FieldInfo[] objInfos = instancia.GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo objFileInfo in objInfos)
            {
                Enum? constant = (Enum?)objFileInfo.GetValue(objFileInfo);
                if (objFileInfo.GetCustomAttributes(typeof(CustomErrorCodeAttribute), false).Length != 0)
                {
                    yield return new CustomErrorCodeAttribute()
                    {
                        ErrorId = GetEnumNamedConstantAttribute(constant).ErrorId,
                        ErrorCode = GetEnumNamedConstantAttribute(constant).ErrorCode,
                        ErrorMessage = GetEnumNamedConstantAttribute(constant).ErrorMessage,
                        ErrorSeverity = GetEnumNamedConstantAttribute(constant).ErrorSeverity,
                        HttpStatus = GetEnumNamedConstantAttribute(constant).HttpStatus,
                    };
                }
            }
        }
        #endregion

        #endregion

        #region [ + Privates ]

        #region [ GetEnumNamedConstantAttribute ]
        private static CustomErrorCodeAttribute? GetEnumNamedConstantAttribute(Enum constant)
        {
            FieldInfo[] objInfos = constant.GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo objFileInfo in objInfos)
            {
                Enum? constantItem = (Enum?)objFileInfo.GetValue(objFileInfo);
                if (constantItem.GetHashCode().Equals(constant.GetHashCode()))
                {
                    object[] attributes = objFileInfo.GetCustomAttributes(typeof(CustomErrorCodeAttribute), false);

                    if (attributes.Length > 0)
                        return (CustomErrorCodeAttribute)attributes[0];
                }
            }
            return null;
        }
        #endregion

    }
    #endregion
}
