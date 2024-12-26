using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTDD
{
    public class ScheduleManager
    {
        public Dictionary<string, List<ClassSession>> groupSchedules = new Dictionary<string, List<ClassSession>>();

        public void AddClass(string groupName, ClassSession classSession)
        {
            if (!groupSchedules.ContainsKey(groupName))
            {
                groupSchedules[groupName] = new List<ClassSession>();
            }

            foreach (var session in groupSchedules[groupName])
            {
                if ((classSession.StartTime < session.EndTime) && (classSession.EndTime > session.StartTime))
                {
                    throw new InvalidOperationException("Заняття конфліктує за часом з існуючим заняттям.");
                }
            }

            groupSchedules[groupName].Add(classSession);
        }

        public void RemoveClass(string groupName, string subject)
        {
            if (!groupSchedules.ContainsKey(groupName)) 
            {
                throw new InvalidOperationException("Група не знайдена.");
            }

            var sessionToRemove = groupSchedules[groupName].Find(s => s.Subject == subject);

            if (sessionToRemove == null) 
            {
                throw new InvalidOperationException("Заняття не знайдено.");
            }

            groupSchedules[groupName].Remove(sessionToRemove);
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