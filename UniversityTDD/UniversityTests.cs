using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

namespace UniversityTDD
{
    public class UniversityTests
    {
        [Fact]
        public void CreateClassSession_ValidArguments_ReturnsValidResult()
        {
            //Arrange
            string subject = string.Empty;
            string teacher = string.Empty;
            string room = string.Empty;
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            //Act
            var actual = new ClassSession();
            //Assert
            Assert.True(actual.Subject == subject &&
                actual.Teacher == teacher &&
                actual.Room == room &&
                actual.StartTime == startTime &&
                actual.EndTime == endTime);
        }

        [Fact]
        public void CreateClassSessionWithParams_ValidArguments_ReturnsValidResult()
        {
            //Arrange
            string subject = "Math";
            string teacher = "Dr. Smith";
            string room = "Room 101";
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue.AddHours(1);
            //Act
            var actual = new ClassSession(subject, teacher, room, startTime, endTime);
            //Assert
            Assert.True(actual.Subject == subject &&
                actual.Teacher == teacher &&
                actual.Room == room &&
                actual.StartTime == startTime &&
                actual.EndTime == endTime);
        }

        [Fact]
        public void AddClass_ValidArguments_ReturnsValidResult()
        {
            //Arrange
            string group = "Group A";
            var manager = new ScheduleManager();
            var session = new ClassSession("Math", "Dr. Smith", "Room 101", DateTime.MinValue, DateTime.MinValue.AddHours(1));
            //Act-Assert
            Assert.True(manager.AddClass(group, session) == true);
        }

        [Fact]
        public void RemoveClass_ValidArguments_ReturnsValidResult()
        {
            //Arrange
            var manager = new ScheduleManager();
            var session = new ClassSession("Math", "Dr. Smith", "Room 101", DateTime.MinValue, DateTime.MinValue.AddHours(1));
            //Act
            manager.AddClass("Group A", session);
            //Assert
            Assert.True(manager.RemoveClass("Group A", "Math") == true);
        }
    }
}