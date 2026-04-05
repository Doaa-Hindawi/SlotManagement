using System;
using System.Collections.Generic;
using System.Text;

namespace SlotManagement.Slots
{
    public class SlotDto
    {
        public string LocalStartTime { get; set; } = string.Empty;
        public string LocalEndTime { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public bool IsBookable { get; set; }
    }
}
