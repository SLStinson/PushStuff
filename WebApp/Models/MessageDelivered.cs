using System;

namespace WebApp.Models
{
    public class MessageDelivered
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime OccuredAt { get; set; }
    }
}