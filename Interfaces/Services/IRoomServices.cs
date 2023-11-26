using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IRoomServices
    {
        Task<RoomsResponseModel> Create(CreateRoomRequestModel model);
        Task<RoomsResponseModel> GetAllAvailableRoom();
        Task<RoomsResponseModel> GetUnAvailableRoom();
        Task<RoomsResponseModel> GetAllRooms();
        Task<RoomResponseModel> GetRoomByIdAsync(int id);
        Task<RoomResponseModel> UpdateRoomAsync(UpdateRoomRequestModel model,int id);
        Task<BaseResponse> DeleteAsync(int id);

    }
}
