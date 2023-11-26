namespace CLH_Final_Project.Dtos.RequestModel
{
    public class MailRequset
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string AttachmentName { get; set; }
        public string HtmlContent { get; set; }
        public string Subject { get; set; }

    }
}
