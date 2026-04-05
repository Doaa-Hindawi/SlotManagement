using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SlotManagement.Slots
{
    public class GenerateSlotsInput
    {
        [Required]
        public string StartDate { get; set; } = string.Empty; 

        [Required]
        public string EndDate { get; set; } = string.Empty;

        [Required]
        public string TimeZone { get; set; } = string.Empty;

        [Required]
        [Range(1, 1440)]
        public int SlotDuration { get; set; }
    }
}
