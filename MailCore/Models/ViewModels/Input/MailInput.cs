namespace MailCore.Models.ViewModels.Input
{
    public class MailInput
    {
        public int ConfigId { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Tos { get; set; }
    }
}
