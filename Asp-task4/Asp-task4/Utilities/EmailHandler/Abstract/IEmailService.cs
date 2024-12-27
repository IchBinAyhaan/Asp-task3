using Asp_task4.Utilities.EmailHandler.Models;

namespace Asp_task4.Utilities.EmailHandler.Abstract
{
    public interface IEmailService
    {
        public void SendMessage(Message message);
    }
}