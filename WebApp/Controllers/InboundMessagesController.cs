using System;
using System.Linq;
using System.Web.Http;
using com.esendex.sdk.messaging;
using com.esendex.sdk.sent;
using WebApp.Formatters;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("Test")]
    public class InboundMessagesController : ApiController
    {
        private Guid _deliveredId;

        [Route("")]
        public IHttpActionResult Post(InboundMessage inboundMessage)
        {
            var messageFormatter = new MessageFormatter();
            var formattedMessage = messageFormatter.FormatMessage(inboundMessage.MessageText);
            if (formattedMessage != null)
            {
                var messageDispatcher = new MessagingService(AccountInformation.USERNAME, AccountInformation.PASSWORD);
                messageDispatcher.SendMessage(new SmsMessage(AccountInformation.PHONE_NUMBER,
                    string.Format("Message from: {0}. Attendance: {1}. Message:{2}",
                        formattedMessage.PlayerName,
                        formattedMessage.Attendance,
                        formattedMessage.Message),
                    AccountInformation.ACCOUNT_REFERENCE));
            }

            return Ok();
        }

        [Route("AccountEventHandle")]
        [HttpPost, HttpPut]
        public IHttpActionResult EventHandler(params object[] args)
        {
            var messageDispatcher = new MessagingService(AccountInformation.USERNAME, AccountInformation.PASSWORD);
            messageDispatcher.SendMessage(new SmsMessage(AccountInformation.PHONE_NUMBER, "Something just happened on your account, you should probably check it out!", AccountInformation.ACCOUNT_REFERENCE));
            
            return Ok();
        }

        [Route("Delivered")]
        public IHttpActionResult Post(MessageDelivered deliveredResult)
        {
            if (deliveredResult.MessageId != _deliveredId)
            {
                var messageDispatcher = new MessagingService(AccountInformation.USERNAME, AccountInformation.PASSWORD);
                var sentService = new SentService(AccountInformation.USERNAME, AccountInformation.PASSWORD);
                var sentMessage = sentService.GetMessage(deliveredResult.MessageId);
                var result =
                    messageDispatcher.SendMessage(new SmsMessage(AccountInformation.PHONE_NUMBER,
                        string.Format("Your account has been used to send a message to: {0}, at: {1}",
                            sentMessage.Recipient, sentMessage.SentAt), AccountInformation.ACCOUNT_REFERENCE));
                _deliveredId = result.MessageIds.First().Id;
            }

            return Ok();
        }
    }
}

