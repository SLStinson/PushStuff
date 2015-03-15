using System;
using WebApp.Models;

namespace WebApp.Formatters
{
    public class MessageFormatter
    {
        public FormattedMessage FormatMessage(string messageBody)
        {
            var messageParts = messageBody.Split('-');
            if (messageParts.Length != 3)
            {
                return null;
            }

            var attendance = messageParts[1];
            if (string.Equals(attendance, Attendance.Accepted.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return new FormattedMessage(messageParts[0], Attendance.Accepted, messageParts[2]);
            }
            if (string.Equals(attendance, Attendance.Tentative.ToString()))
            {
                return new FormattedMessage(messageParts[0], Attendance.Tentative, messageParts[2]);
            }

            return new FormattedMessage(messageParts[0], Attendance.Declined, messageParts[2]);
        }
    }
}