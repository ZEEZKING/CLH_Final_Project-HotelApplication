namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class RoleResponseModel : BaseResponse
    {
        public RoleDto Data { get; set; }
    }

    public class RolesResponseModel : BaseResponse
    {
        public List<RoleDto> Data { get; set; }
    }
}
