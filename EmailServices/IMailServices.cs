using CLH_Final_Project.Dtos.RequestModel;

namespace CLH_Final_Project.EmailServices
{
    public interface IMailServices
    {
        public void SendEmailAsync(MailRequset mailRequest);
    }
}
