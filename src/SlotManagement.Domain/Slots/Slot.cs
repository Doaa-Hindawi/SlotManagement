using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SlotManagement.Slots;
    public class Slot : AuditedAggregateRoot<Guid>
    {
        public Instant StartInstant { get; set; } = default!;
        public Instant EndInstant { get; set; } = default!;
        public string CreationTimeZone { get; set; } = string.Empty;
        public SlotStatus Status { get; set; } = SlotStatus.Available;

        protected Slot() { }

        public Slot(Guid id, Instant startInstant, Instant endInstant, string creationTimeZone, SlotStatus status)
        {
            Id = id;
            StartInstant = startInstant;
            EndInstant = endInstant;
            CreationTimeZone = creationTimeZone;
            Status = status;
    }
    }

public enum SlotStatus
{
    Available = 0,
    Booked = 1
}