using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.EmailServices;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;

namespace CLH_Final_Project.Implementation.Services
{
    public class VerificationCodeServices : IVerificationCodeServices
    {
        private readonly IMailServices _mailServices;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IUserRepository _userRepository;

        public VerificationCodeServices(IMailServices mailServices, ICustomerRepository customerRepository, IVerificationCodeRepository verificationCodeRepository, IUserRepository userRepository)
        {
            _mailServices = mailServices;
            _customerRepository = customerRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _userRepository = userRepository;
        }

        public async Task<ResetPasswordResponseModel> SendForgetPassWordVerificationCode(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null)
            {
                return new ResetPasswordResponseModel
                {
                    Message = "Email not found",
                    Sucesss = false
                };
            }
            var code = await _verificationCodeRepository.GetAsync(x => x.Customer.Id == customer.Id);
            if (code == null)
            {
                return new ResetPasswordResponseModel
                {
                    Message = "No Code has been sent to at registration Point ",
                    Sucesss = false
                };
            }
            int random = new Random().Next(100000, 999999);
            code.Code = random;
            code.DateCreated = DateTime.Now;
            var mailRequest = new MailRequset
            {
                Subject = "Reset Password",
                ToEmail = customer.User.Email,
                ToName = customer.User.Name,
                HtmlContent = $"<html><body><h1>Hello {customer.User.Name}, Welcome to SkyBox Hotel Limited.</h1><h4>Your password reset code is {code.Code} to continue with the registration</h4></body></html>",
            };
             _mailServices.SendEmailAsync(mailRequest);
            customer.User.IsDeleted = true;
            await _userRepository.UpdateAsync(customer.User);
            await _verificationCodeRepository.UpdateAsync(code);
            return new ResetPasswordResponseModel
            {
                Id = customer.Id,
                Message = "Reset passeord code successfully reset",
                Sucesss = true
            };

        }

        public async Task<BaseResponse> UpdateVerificationCodeAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return new BaseResponse
                {
                    Message = "Customer Not found",
                    Sucesss = false,
                };
            }
            var code = await _verificationCodeRepository.GetAsync(x => x.Customer.Id == id);
            if (code == null)
            {
                return new BaseResponse
                {
                    Message = "No Code has been sent to you before",
                    Sucesss = false,
                };
            }
            int random = new Random().Next(100000,999999);
            code.Code = random;
            code.DateCreated = DateTime.Now;
            var mailRequest = new MailRequset
            {
                Subject = "Confirmation Code",
                ToEmail = customer.User.Email,
                ToName = customer.User.Name,
                HtmlContent = $"<html><body><h1>Hello {customer.User.Name}, Welcome to SkyBox Hotel Limited.</h1><h4>Your confirmation code is {code.Code} to continue with the registration</h4></body></html>",
            };
          _mailServices.SendEmailAsync(mailRequest);
            await _verificationCodeRepository.UpdateAsync(code);
            return new BaseResponse
            {
                Message = "Code Successfully resent",
                Sucesss = true,
            };

        }

        public async Task<BaseResponse> VerifyCode(int verificationcode, int id)
        {
            var code = await _verificationCodeRepository.GetAsync(x => x.CustomerId == id && x.Code == verificationcode);
            if (code == null)
            {
                return new BaseResponse
                {
                    Message = "Invalid Code",
                    Sucesss = false
                };
            }
            else if ((DateTime.Now - code.DateCreated).TotalSeconds > 300)
            {
                return new BaseResponse
                {
                    Message = "Code expired",
                    Sucesss = false
                };
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            customer.User.IsDeleted = false;
            await _userRepository.UpdateAsync(customer.User);
            return new BaseResponse
            {
                Message = "Email Successfully Verified",
                Sucesss = true,
            };

        }
    }
}
