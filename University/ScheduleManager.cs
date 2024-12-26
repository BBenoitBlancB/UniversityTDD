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

        public void AddClass(string groupName, ClassSession classSession)
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
        }
    }
}