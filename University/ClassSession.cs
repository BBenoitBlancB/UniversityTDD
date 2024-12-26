using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTDD
{
    public class ClassSession
    {
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ClassSession()
        {
            Subject = string.Empty;
            Teacher = string.Empty;
            Room = string.Empty;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
        }

        public ClassSession(string subject, string teacher, string room, DateTime startTime, DateTime endTime)
        {
            Subject = subject;
            Teacher = teacher;
            Room = room;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}