using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hostel_management.Models
{
    public class RoomModel
    {
        [Key]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Hostel name is required")]
        public string HostelName { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }


        [Required(ErrorMessage = "Capacity is required")]
        public int Capacity { get; set; }


        [Required(ErrorMessage = "Fees is required")]
        public decimal Fees { get; set; }

        public bool AcRoom { get; set; }

        public string IssueItems { get; set; }
    }
}
