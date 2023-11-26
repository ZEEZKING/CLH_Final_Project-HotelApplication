using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationCodeController : ControllerBase
    {
        private readonly IVerificationCodeServices _verificationCodeServices;

        public VerificationCodeController(IVerificationCodeServices verificationCodeServices)
        {
            _verificationCodeServices = verificationCodeServices;
        }

        [HttpGet("VerifyCode/{code}/{id}")]
        public async Task<IActionResult> VerifyCode([FromRoute] int code,[FromRoute]int id)
        {
            var verifyCode = await _verificationCodeServices.VerifyCode(code, id);
            if (verifyCode.Sucesss == false)
            {
                return BadRequest(verifyCode);
            }
            return Ok(verifyCode);
        }

        [HttpPut("UpdateCode/{id}")]
        public async Task<IActionResult> UpdateCodeAsync([FromRoute] int id)
        {
            var code = await _verificationCodeServices.UpdateVerificationCodeAsync(id);
            if (code.Sucesss == false)
            {
                return BadRequest(code);
            }
            return Ok(code);

        }
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordRequestModel model)
        {
            var reset = await _verificationCodeServices.SendForgetPassWordVerificationCode(model.Email);
            if (reset.Sucesss == false)
            {
                return BadRequest(reset);
            }
            return Ok(reset);
        }
    }   

    
}
