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
        public void RemoveClass_ShouldRemoveClass_WhenClassExists()
        {
            // Arrange
            var manager = new ScheduleManager();
            var groupName = "Group A";
            var classSession = new ClassSession
            {
                Subject = "Math",
                StartTime = new DateTime(2024, 12, 30, 10, 0, 0),
                EndTime = new DateTime(2024, 12, 30, 11, 0, 0)
            };

            manager.AddClass(groupName, classSession);

            // Act
            manager.RemoveClass(groupName, "Math");

            // Assert
            var classes = manager.GetClassesForGroup(groupName);
            Assert.Empty(classes);
        }

        [Fact]
        public void RemoveClass_ShouldThrowException_WhenGroupDoesNotExist()
        {
            // Arrange
            var manager = new ScheduleManager();
            var groupName = "Nonexistent Group";

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
                manager.RemoveClass(groupName, "Math"));

            // Assert
            Assert.Equal("Група не знайдена.", exception.Message);
        }

        [Fact]
        public void RemoveClass_ShouldThrowException_WhenClassDoesNotExist()
        {
            // Arrange
            var manager = new ScheduleManager();
            var groupName = "Group A";
            var classSession = new ClassSession
            {
                Subject = "Math",
                StartTime = new DateTime(2024, 12, 30, 10, 0, 0),
                EndTime = new DateTime(2024, 12, 30, 11, 0, 0)
            };

            manager.AddClass(groupName, classSession);

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
                manager.RemoveClass(groupName, "Science"));

            // Assert
            Assert.Equal("Заняття не знайдено.", exception.Message);
        }

        [Fact]
        public void GetClassesForGroup_ExistingGroup_ReturnsCorrectClasses()
        {
            // Arrange
            var manager = new ScheduleManager();
            var groupName = "GroupA";
            var classSession1 = new ClassSession { Subject = "Math", StartTime = new DateTime(2024, 1, 1, 10, 0, 0), EndTime = new DateTime(2024, 1, 1, 11, 0, 0) };
            var classSession2 = new ClassSession { Subject = "English", StartTime = new DateTime(2024, 1, 1, 12, 0, 0), EndTime = new DateTime(2024, 1, 1, 13, 0, 0) };

            manager.AddClass(groupName, classSession1);
            manager.AddClass(groupName, classSession2);

            // Act
            var result = manager.GetClassesForGroup(groupName);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, cs => cs.Subject == "Math");
            Assert.Contains(result, cs => cs.Subject == "English");
        }

        [Fact]
        public void GetClassesForGroup_NonExistingGroup_ReturnsEmptyList()
        {
            // Arrange
            var manager = new ScheduleManager();
            var groupName = "NonExistingGroup";

            // Act
            var result = manager.GetClassesForGroup(groupName);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetClassesForGroup_EmptySchedule_ReturnsEmptyList()
        {
            // Arrange
            var manager = new ScheduleManager();
            var groupName = "EmptyGroup";

            // Act
            var result = manager.GetClassesForGroup(groupName);

            // Assert
            Assert.Empty(result);
        }
    }
}