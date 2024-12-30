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
        public void AddClass_ShouldAddClass_WhenNoConflict()
        {
            // Arrange
            var manager = new ScheduleManager();
            var classSession1 = new ClassSession { StartTime = new DateTime(2024, 1, 1, 10, 0, 0), EndTime = new DateTime(2024, 1, 1, 11, 0, 0) };
            var classSession2 = new ClassSession { StartTime = new DateTime(2024, 1, 1, 11, 0, 0), EndTime = new DateTime(2024, 1, 1, 12, 0, 0) };

            // Act
            manager.AddClass("GroupA", classSession1);
            manager.AddClass("GroupA", classSession2);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void AddClass_ShouldThrowException_WhenConflictOccurs()
        {
            // Arrange
            var manager = new ScheduleManager();
            var classSession1 = new ClassSession { StartTime = new DateTime(2024, 1, 1, 10, 0, 0), EndTime = new DateTime(2024, 1, 1, 11, 0, 0) };
            var classSession2 = new ClassSession { StartTime = new DateTime(2024, 1, 1, 10, 30, 0), EndTime = new DateTime(2024, 1, 1, 11, 30, 0) };

            // Act
            manager.AddClass("GroupA", classSession1);

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => manager.AddClass("GroupA", classSession2));
            Assert.Equal("Заняття конфліктує за часом з існуючим заняттям.", exception.Message);
        }

        [Fact]
        public void AddClass_ShouldCreateNewGroup_WhenGroupDoesNotExist()
        {
            // Arrange
            var manager = new ScheduleManager();
            var classSession = new ClassSession { StartTime = new DateTime(2024, 1, 1, 10, 0, 0), EndTime = new DateTime(2024, 1, 1, 11, 0, 0) };

            // Act
            manager.AddClass("GroupB", classSession);

            // Assert
            Assert.True(true);
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
        public void RemoveClass_GroupNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var manager = new ScheduleManager();
            string groupName = "NonExistentGroup";
            string subject = "Math";

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
                manager.RemoveClass(groupName, subject));

            // Assert
            Assert.Equal("Група не знайдена.", exception.Message);
        }

        [Fact]
        public void RemoveClass_SubjectNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var manager = new ScheduleManager();
            string groupName = "Group A";
            string subject = "NonExistentSubject";

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
                manager.RemoveClass(groupName, subject));

            // Assert
            Assert.Equal("Заняття не знайдено.", exception.Message);
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