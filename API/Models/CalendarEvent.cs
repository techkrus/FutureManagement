using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Enums;

namespace API.Models
{
    public class CalendarEvent
    {

        public CalendarEvent(int id, CalendarEventType eventType, string name, string description, DateTime startTime, DateTime endTime, bool repeats, int repeatedInterval, User createdBy)
        {
            this.Id = id;
            this.EventType = eventType;
            this.Name = name;
            this.Description = description;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Repeats = repeats;
            this.RepeatedInterval = repeatedInterval;
            this.CreatedBy = createdBy;
        }

        [Key]
        public int Id { get; private set; }

        [Required]
        public CalendarEventType EventType { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public bool Repeats { get; set; }
        public int RepeatedInterval { get; set; }
        public User CreatedBy { get; set; }
        public ICollection<User> Participants { get; set; }
    }
}