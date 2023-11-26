namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class UserResponseModel : BaseResponse
    {
        public UserDto Data { get; set; }

    }

    public class UsersResponseModel : BaseResponse
    {
        public List<UserDto> Data { get; set; }

    }
}
