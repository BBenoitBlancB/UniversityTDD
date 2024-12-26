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
            var session = new ClassSession("Math", "John Doe", "Room 101", DateTime.MinValue, DateTime.MinValue.AddHours(1));
            //Act-Assert
            Assert.Equal("Math", session.Subject);
            Assert.Equal("John Doe", session.Teacher);
            Assert.Equal("Room 101", session.Room);
            Assert.Equal(DateTime.MinValue, session.StartTime);
            Assert.Equal(DateTime.MinValue.AddHours(1), session.EndTime);
        }

        [Fact]
        public void AddClass_ValidArguments_ReturnsValidResult()
        {
            //Arrange
            var manager = new ScheduleManager();
            var session = new ClassSession("Math", "John Doe", "Room 101", DateTime.Now, DateTime.Now.AddHours(1));
            //Act
            manager.AddClass("Group A", session);
            var classes = manager.GetClassesForGroup("Group A");
            //Assert
            Assert.Equal(1, classes.Count);
        }

        [Fact]
        public void RemoveClass_ValidArguments_ReturnsValidResult()
        {
            //Arrange
            var manager = new ScheduleManager();
            var session = new ClassSession("Math", "John Doe", "Room 101", DateTime.Now, DateTime.Now.AddHours(1));
            //Act
            manager.AddClass("Group A", session);
            manager.RemoveClass("Group A", "Math");
            var classes = manager.GetClassesForGroup("Group A");
            //Assert
            Assert.Equal(0, classes.Count);
        }

        [Fact]
        public void GetClassesForGroup_ValidArguments_ReturnsValidResult()
        {
            //Arrange
            var manager = new ScheduleManager();
            //Act
            manager.AddClass("Group A", new ClassSession("Math", "John Doe", "Room 101", DateTime.Now, DateTime.Now.AddHours(1)));
            var classes = manager.GetClassesForGroup("Group A");
            //Assert
            Assert.Equal(1, classes.Count);
        }
    }
}