using CrimeReportingSystem.Dao;
using CrimeReportingSystem.Entities;
namespace CrimeReportingSystem.Tests
{
    public class Tests
    {
        private CrimeAnalysisServiceImpl _service;

        [SetUp]
        public void Setup()
        {
            _service = new CrimeAnalysisServiceImpl();
        }

        [Test]
        //For testing the CreateIncident Function
        public void Test_CreateIncident_ShouldReturnTrueForSucces()
        {
            // Arrange
            Incident incident = new Incident
            {
               
                IncidentType = "Robbery",
                IncidentDate = DateTime.Now,
                Location = "Zone B",
                Description = "Robbery reported in zone A",
                Status = "open",
                VictimID = 2,
                SuspectID = 2
            };

            // Act
            bool result = _service.CreateIncident(incident);

            // Assert
            Assert.IsTrue(result, "Incident creation should return true");
        }

        [Test]
        //Test for updating the status of the incident
        public void Test_UpdateIncidentStatus_ShouldReturnTrueInCaseOfProperUpdate()
        {
            // Arrange
            int incidentId = 3; // Use the same ID as the created one above
            string newStatus = "under investigation";

            // Act
            bool result = _service.UpdateIncidentStatus(newStatus, incidentId);

            // Assert
            Assert.IsTrue(result, "Incident status update should return true");
        }
    }
}
