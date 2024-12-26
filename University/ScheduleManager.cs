using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTDD
{
    public class ScheduleManager
    {
        public Dictionary<string, List<ClassSession>> groupSchedules;

        public bool AddClass(string groupName, ClassSession classSession)
        {
            if (!groupSchedules.ContainsKey(groupName))
            {
                groupSchedules[groupName] = new List<ClassSession>();
            }

            if (groupSchedules[groupName].Any(c => c.StartTime < classSession.EndTime && c.EndTime > classSession.StartTime))
            {
                throw new InvalidOperationException("New class conflicts with an existing class.");
            }

            groupSchedules[groupName].Add(classSession);
            return true;
        }

        public bool RemoveClass(string groupName, string subject)
        {
            if (!groupSchedules.ContainsKey(groupName) || !groupSchedules[groupName].Any(c => c.Subject == subject))
            {
                throw new KeyNotFoundException("Class not found for removal.");
            }

            groupSchedules[groupName].RemoveAll(c => c.Subject == subject);
            return true;
        }

        public List<ClassSession> GetClassesForGroup(string groupName)
        {
            if (!groupSchedules.ContainsKey(groupName))
            {
                return new List<ClassSession>();
            }

            return groupSchedules[groupName];
        }
    }
}