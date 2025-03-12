using Microsoft.EntityFrameworkCore;
using Echo.Core.Abstractions.Models;
using Echo.Core.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Echo.Core.Models
{
    public class EchoEntity : Aggregate
    {
        public EchoEntity(Guid id) : base(id)
        {
        }

        public static EchoEntity Create(Guid userId, string text, string hash)
        {
            var Echo = new EchoEntity(Guid.NewGuid())
            {
                UserId = userId,
                Text = text,
                EchoDate = DateTime.UtcNow,
                Hash = hash
            };

            var @event = new EchoCreatedDomainEvent(Echo.Id,Echo.UserId,Echo.Text, Echo.EchoDate, Echo.Hash);

            Echo.AddDomainEvent(@event);
            return Echo;
        }

        [Required]
        public Guid UserId { get; private set; }

        public string Text { get; private set; }

        [Required]
        public DateTime EchoDate { get; private set; }

        [Required]
        public string Hash { get; private set; }
    }
}
