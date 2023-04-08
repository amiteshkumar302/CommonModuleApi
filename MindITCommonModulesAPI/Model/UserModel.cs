namespace MindITCommonModulesAPI.Model
{
    public class UserModel
    {
        public string? userId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public int isDeleted { get; set; }

        public int isActive { get; set; }
      
        public List<RoleListModel>? RolesList { get; set; }

    }
}
