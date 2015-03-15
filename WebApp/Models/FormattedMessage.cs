using WebApp.Controllers;

namespace WebApp.Models
{
    public class FormattedMessage
    {
        public string PlayerName { get; private set; }
        public Attendance Attendance { get; private set; }
        public string Message { get; private set; }

        public FormattedMessage(string playerName, Attendance attendance, string message)
        {
            PlayerName = playerName;
            Attendance = attendance;
            Message = message;
        }
    }
}