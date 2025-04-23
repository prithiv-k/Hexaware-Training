using System;
using System.Collections.Generic;
using CrimeReportingSystem.Entities;
using CrimeReportingSystem.Dao;

namespace CrimeReportingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ICrimeAnalysisService crimeService = new CrimeAnalysisServiceImpl();
            bool exit = false;
            while (!exit)
            {
                //Menu for CrimeReportSystem
                Console.Clear();
                Console.WriteLine("\t\t\t --------------------------------------------------- ");
                Console.WriteLine("\t\t\t|             Crime Reporting System Menu           |");
                Console.WriteLine("\t\t\t|---------------------------------------------------|");
                Console.WriteLine("\t\t\t|  1. Create a new Incident                         |");
                Console.WriteLine("\t\t\t|  2. Update Incident Status                        |");
                Console.WriteLine("\t\t\t|  3. Get Incidents in Date Range                   |");
                Console.WriteLine("\t\t\t|  4. Search Incidents by Type                      |");
                Console.WriteLine("\t\t\t|  5. Generate Incident Report                      |");
                Console.WriteLine("\t\t\t|  6. Create a New Case                             |");
                Console.WriteLine("\t\t\t|  7. Get Case Details                              |");
                Console.WriteLine("\t\t\t|  8. Update Case Details                           |");
                Console.WriteLine("\t\t\t|  9. Get All Cases                                 |");
                Console.WriteLine("\t\t\t| 10. Exit                                          |");
                Console.WriteLine("\t\t\t --------------------------------------------------- ");
                Console.Write("Select an option: ");


                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    //here all the methods were called in Switch Case bases on the user's choice.
                    switch (choice)
                    {
                        case 1:
                            CreateIncident(crimeService);
                            break;

                        case 2:
                            UpdateIncidentStatus(crimeService);
                            break;

                        case 3:
                            GetIncidentsInDateRange(crimeService);
                            break;

                        case 4:
                            SearchIncidentsByType(crimeService);
                            break;

                        case 5:
                            GenerateIncidentReport(crimeService);
                            break;

                        case 6:
                            CreateNewCase(crimeService);
                            break;

                        case 7:
                            GetCaseDetails(crimeService);
                            break;

                        case 8:
                            UpdateCaseDetails(crimeService);
                            break;

                        case 9:
                            GetAllCases(crimeService);
                            break;

                        case 10:
                            Console.WriteLine("Exiting...");
                            exit= true;
                            return;

                        default:
                            Console.WriteLine("Invalid selection, please try again.");
                            break;
                    }
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Press any key to continue...");
          
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.ReadKey();
                }
               
            }
        }
        //method for create incident
        static void CreateIncident(ICrimeAnalysisService crimeService)
        {
            Console.WriteLine("Enter Incident Details:");
            Console.Write("Incident Type: ");
            string type = Console.ReadLine();
            Console.Write("Incident Date (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Location: ");
            string location = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.Write("Status: ");
            string status = Console.ReadLine();
            Console.Write("Victim ID: ");
            int victimId = int.Parse(Console.ReadLine());
            Console.Write("Suspect ID: ");
            int suspectId = int.Parse(Console.ReadLine());

            Incident incident = new Incident
            {
                IncidentType = type,
                IncidentDate = date,
                Location = location,
                Description = description,
                Status = status,
                VictimID = victimId,
                SuspectID = suspectId
            };

            bool result = crimeService.CreateIncident(incident);
            Console.WriteLine(result ? "Incident created successfully!" : "Error creating incident.");
        }
        //method for update the status
        static void UpdateIncidentStatus(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Incident ID: ");
            int incidentId = int.Parse(Console.ReadLine());
            Console.Write("Enter new Status: ");
            string status = Console.ReadLine();

            bool result = crimeService.UpdateIncidentStatus(status, incidentId);
            Console.WriteLine(result ? "Incident status updated successfully!" : "Error updating status.");
        }
        //method for retriving incident occur in the certain date range
        static void GetIncidentsInDateRange(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter End Date (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            List<Incident> incidents = crimeService.GetIncidentsInDateRange(startDate, endDate);
            Console.WriteLine("Incidents in the given date range:");
            foreach (var incident in incidents)
            {
                Console.WriteLine($"Incident ID: {incident.IncidentID}, Type: {incident.IncidentType}, Date: {incident.IncidentDate}");
            }
        }
        //method for searching the incident type

        static void SearchIncidentsByType(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Incident Type to search: ");
            string incidentType = Console.ReadLine();

            List<Incident> incidents = crimeService.SearchIncidents(incidentType);
            Console.WriteLine("Found incidents:");
            foreach (var incident in incidents)
            {
                Console.WriteLine($"Incident ID: {incident.IncidentID}, Type: {incident.IncidentType}, Status: {incident.Status}");
            }
        }
        //method for generating incident report
        static void GenerateIncidentReport(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Incident ID for the report: ");
            int incidentId = int.Parse(Console.ReadLine());

            Incident incident = new Incident { IncidentID = incidentId };

            // Get the list of reports
            List<Report> reports = crimeService.GenerateIncidentReport(incident);

            if (reports != null && reports.Count > 0)
            {
                // If the list is not empty, use the first report
                Report report = reports[0];  // You can choose another report if necessary

                Console.WriteLine($"Report Generated:\n{report.ReportDetails}");
            }
            else
            {
                Console.WriteLine("Failed to generate report. Please check the incident ID or try again.");
            }
        }


        //method for creting new case
        static void CreateNewCase(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Case ID: ");
            int caseId = int.Parse(Console.ReadLine());

            Console.Write("Enter Case Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter number of incidents to associate: ");
            int numIncidents = int.Parse(Console.ReadLine());

            List<Incident> incidents = new List<Incident>();
            for (int i = 0; i < numIncidents; i++)
            {
                Console.Write($"Enter Incident ID for incident {i + 1}: ");
                int incidentId = int.Parse(Console.ReadLine());
                incidents.Add(new Incident { IncidentID = incidentId });
            }

            Case newCase = crimeService.CreateCase(caseId, description, incidents);

            if (newCase != null)
            {
                Console.WriteLine($"Case created successfully! Case ID: {newCase.CaseId}");
            }
            else
            {
                Console.WriteLine("Failed to create case.");
            }
        }

        //method for getting the case details
        static void GetCaseDetails(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Case ID: ");
            int caseId = int.Parse(Console.ReadLine());

            Case caseDetails = crimeService.GetCaseDetails(caseId);
            if (caseDetails != null)
            {
                Console.WriteLine($"Case ID: {caseDetails.CaseId}, Description: {caseDetails.Description}");
                foreach (var incident in caseDetails.Incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.IncidentID}");
                }
            }
            else
            {
                Console.WriteLine("Case not found.");
            }
        }
        //updating the case description
        static void UpdateCaseDetails(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Case ID to update: ");
            int caseId = int.Parse(Console.ReadLine());
            Console.Write("Enter new Case Description: ");
            string description = Console.ReadLine();

            Case caseToUpdate = new Case { CaseId = caseId, Description = description };
            bool result = crimeService.UpdateCaseDetails(caseToUpdate);
            Console.WriteLine(result ? "Case updated successfully!" : "Error updating case.");
        }
        //method for getting all the cases

        static void GetAllCases(ICrimeAnalysisService crimeService)
        {
            List<Case> cases = crimeService.GetAllCases();
            Console.WriteLine("All Cases:");
            foreach (var c in cases)
            {
                Console.WriteLine($"Case ID: {c.CaseId}, Description: {c.Description}");
            }
        }
    }
}
