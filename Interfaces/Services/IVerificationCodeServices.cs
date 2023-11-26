using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IVerificationCodeServices
    {
        Task<BaseResponse> UpdateVerificationCodeAsync(int id);
        Task<BaseResponse> VerifyCode(int id,int verificationcode);
        Task<ResetPasswordResponseModel> SendForgetPassWordVerificationCode(string email);
    }
}
