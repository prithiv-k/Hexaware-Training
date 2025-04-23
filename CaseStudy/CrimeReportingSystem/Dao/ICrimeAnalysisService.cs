using CrimeReportingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Dao
{
  public  interface ICrimeAnalysisService
    {
        // 1.Create a new incident 
        bool CreateIncident(Incident incident);

        // 2.Update the status of an incident
        bool UpdateIncidentStatus(string status, int incidentId);

        // 3.Get a list of incidents within a date range
        List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate);

        // 4.Search for incidents based on various criteria
        List<Incident> SearchIncidents(string incidentType);

        //5. Generate incident reports
        List<Report> GenerateIncidentReport(Incident incident);

        //6. Create a new case and associate it with incidents
        // In ICrimeAnalysisService
        Case CreateCase(int caseIdFromUser, string caseDescription, List<Incident> incidents);


        //7. Get details of a specific case
        Case GetCaseDetails(int caseId);

        //8. Update case details
        bool UpdateCaseDetails(Case caseObj);

        //9. Get a list of all cases
        List<Case> GetAllCases();
    }
}
