using CrimeReportingSystem.Entities;
using CrimeReportingSystem.Exceptions;
using CrimeReportingSystem.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CrimeReportingSystem.Dao
{
    public class CrimeAnalysisServiceImpl : ICrimeAnalysisService
    {
        SqlConnection sqlCon;
        SqlCommand cmd;
        private List<Incident> incidentList = new List<Incident>();
      

        public CrimeAnalysisServiceImpl()
        {
            sqlCon = DBConnUtil.GetConnection("appSetting.json");
            cmd = new SqlCommand();
        }

        public bool CreateIncident(Incident incident)
        {
            //method definition for create incident
            try
            {
                cmd.Connection = sqlCon;
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO Incidents (Incident_Type, Incident_Date,Location, Description, Status, Victim_Id, Suspect_Id) ");
                query.Append("VALUES (@Incident_Type, @Incident_Date, @Location, @Description, @Status, @Victim_Id, @Suspect_Id)");

                cmd.CommandText = query.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Incident_Type", incident.IncidentType);
                cmd.Parameters.AddWithValue("@Incident_Date", incident.IncidentDate);
                cmd.Parameters.AddWithValue("@Location", incident.Location);
           
                cmd.Parameters.AddWithValue("@Description", incident.Description);
                cmd.Parameters.AddWithValue("@Status", incident.Status);
                cmd.Parameters.AddWithValue("@Victim_Id", incident.VictimID);
                cmd.Parameters.AddWithValue("@Suspect_Id", incident.SuspectID);

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public bool UpdateIncidentStatus(string status, int incidentId)
        {
            //here in this method we will update the status of the incident
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "UPDATE Incidents SET Status = @Status WHERE Incident_Id = @IncidentId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@IncidentId", incidentId);

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new IncidentNumberNotFoundException($"Incident with ID {incidentId} not found.");
                }

                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate)
        {
            //here we will retrive the incident fron certain date range
            List<Incident> incidents = new List<Incident>();

            try
            {
                // Prepare the SQL query to get incidents in the specified date range
                cmd.Connection = sqlCon;
                cmd.CommandText = "SELECT Incident_Id, Incident_Type, Incident_Date, Location, Description, Status, Victim_Id, Suspect_Id FROM Incidents WHERE Incident_Date BETWEEN @StartDate AND @EndDate";

                // Add parameters to prevent SQL injection
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Map each row to an Incident object
                    int incidentId = reader.GetInt32(0);  // IncidentID
                    string incidentType = reader.GetString(1);  // IncidentType
                    DateTime incidentDate = reader.GetDateTime(2);  // IncidentDate
                    string location = reader.GetString(3);  // Location
                    string description = reader.GetString(4);  // Description
                    string status = reader.GetString(5);  // Status
                    int victimId = reader.GetInt32(6);  // VictimID
                    int suspectId = reader.GetInt32(7);  // SuspectID

                    // Create an Incident object and add it to the list
                    incidents.Add(new Incident(incidentId, incidentType, incidentDate, location, description, status, victimId, suspectId));
                }

                return incidents;
            }
            catch (SqlException ex)
            {
                // Handle SQL-specific exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
                return incidents;  // Return an empty list in case of error
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine("Unexpected Error: " + ex.Message);
                return incidents;  // Return an empty list in case of error
            }
            finally
            {
                // Ensure the connection is closed after execution
                sqlCon.Close();
            }
        }

        public List<Incident> SearchIncidents(string incidentType)
        {
            //here this method will return the incident by searching the incident type
            List<Incident> incidents = new List<Incident>();

            try
            {
                cmd.Connection = sqlCon;
                StringBuilder query = new StringBuilder();
                query.Append("SELECT Incident_Id, Incident_Type, Incident_Date,Location,Description, Status, Victim_Id, Suspect_Id ");
                query.Append("FROM Incidents WHERE Incident_Type = @IncidentType");

                cmd.CommandText = query.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IncidentType", incidentType);

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Incident incident = new Incident
                        {
                            IncidentID = reader.GetInt32(reader.GetOrdinal("Incident_Id")),
                            IncidentType = reader.GetString(reader.GetOrdinal("Incident_Type")),
                            IncidentDate = reader.GetDateTime(reader.GetOrdinal("Incident_Date")),
                            Location = reader.GetString(reader.GetOrdinal("Location")),
                          
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            VictimID = reader.GetInt32(reader.GetOrdinal("Victim_Id")),
                            SuspectID = reader.GetInt32(reader.GetOrdinal("Suspect_Id"))
                        };

                        incidents.Add(incident);
                    }
                }

                return incidents;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null; // Return null or an empty list based on your preference
            }
            finally
            {
                sqlCon.Close();
            }
        }

        // Implementing the method from the interface for generating te incident report
        public List<Report> GenerateIncidentReport(Incident incident)
        {
            List<Report> reportList = new List<Report>();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection("Data Source=PRITHIV;Initial Catalog=CrimeReportingSystem;Integrated Security=True;TrustServerCertificate=True;"))
                {
                    sqlCon.Open();

                    string query = "SELECT Incident_Id, Incident_Type, Location, Description, Victim_Id, Suspect_Id, Status FROM Incidents WHERE Incident_Id = @IncidentID";
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        // Assuming that incident.IncidentID is already set
                        cmd.Parameters.AddWithValue("@IncidentID", incident.IncidentID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Map the data from the database to the Report object
                                Report report = new Report
                                {
                                    IncidentID = reader.GetInt32(0),
                                    ReportDate = DateTime.Now,
                                    ReportingOfficer = 1234, // Hardcoded for now, can be dynamic later
                                    ReportDetails = $"Incident Type: {reader.GetString(1)}\n" +
                                                    $"Location: {reader.GetString(2)}\n" +
                                                    $"Description: {reader.GetString(3)}\n" +
                                                    $"Victim ID: {reader.GetInt32(4)}\n" +
                                                    $"Suspect ID: {reader.GetInt32(5)}",
                                    Status = reader.GetString(6) // Ensure Status is assigned
                                };

                                // Add the report to the list
                                reportList.Add(report);

                                Console.WriteLine("Report generated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("No incident found with the provided IncidentID.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating report: " + ex.Message);
            }

            return reportList;
        }


        public Case CreateCase(int caseIdFromUser, string Description, List<Incident> incidents)
        {
            try
            {
                if (incidents == null || incidents.Count == 0)
                {
                    throw new IncidentNumberNotFoundException("No incidents found to associate with the case.");
                }

                cmd.Connection = sqlCon;

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("INSERT INTO Cases (CaseId, Description) VALUES (@CaseId, @Description)");

                cmd.CommandText = queryBuilder.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CaseId", caseIdFromUser);
                cmd.Parameters.AddWithValue("@Description", Description);

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                cmd.ExecuteNonQuery();

                // Create the Case object
                Case newCase = new Case
                {
                    CaseId = caseIdFromUser,
                    Description = Description,
                    Incidents = incidents
                };

                return newCase;
            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the case: " + ex.Message);
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public Case GetCaseDetails(int caseId)
        {
            try
            {
                cmd.Connection = sqlCon;

                //Full JOIN query to fetch Case, Incident, Victims, Suspects
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT c.CaseId, c.Description,");
                query.AppendLine("i.Incident_Id, i.Incident_Type, i.Incident_Date, i.Location, i.Description AS IncidentDescription,");
                query.AppendLine("v.Victim_Id AS VictimID, v.First_Name AS VictimFirstName, v.Last_Name AS VictimLastName, v.Date_Of_Birth AS VictimDOB, v.Gender AS VictimGender, v.Contact_Info AS VictimContact,");
                query.AppendLine("s.Suspect_Id AS SuspectID, s.First_Name AS SuspectFirstName, s.Last_Name AS SuspectLastName, s.Date_Of_Birth AS SuspectDOB, s.Gender AS SuspectGender, s.Contact_Info AS SuspectContact");
                query.AppendLine("FROM Cases c");
                query.AppendLine("INNER JOIN Incidents i ON c.CaseId = i.CaseId");
                query.AppendLine("LEFT JOIN Victims v ON i.Incident_Id = v.Incident_Id");
                query.AppendLine("LEFT JOIN Suspects s ON i.Incident_Id = s.Incident_Id");
                query.AppendLine("WHERE c.CaseId = @CaseId");

                cmd.CommandText = query.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CaseId", caseId);

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                    throw new IncidentNumberNotFoundException($"Case with ID {caseId} not found.");

                Case caseDetails = null;

                while (reader.Read())
                {
                    if (caseDetails == null)
                    {
                        caseDetails = new Case
                        {
                            CaseId = (int)reader["CaseId"],  // CaseId from database
                            Description = reader["Description"].ToString(),
                            Incidents = new List<Incident>()
                        };
                    }

                    int incidentId = (int)reader["Incident_Id"];  // Incident_Id from database
                    Incident existingIncident = caseDetails.Incidents.FirstOrDefault(i => i.IncidentID == incidentId);
                    if (existingIncident == null)
                    {
                        existingIncident = new Incident
                        {
                            IncidentID = incidentId,
                            IncidentType = reader["Incident_Type"].ToString(),
                            IncidentDate = (DateTime)reader["Incident_Date"],
                            Location = reader["Location"].ToString(),
                            Description = reader["IncidentDescription"].ToString(),
                            Victims = new List<Victim>(),
                            Suspects = new List<Suspect>()
                        };
                        caseDetails.Incidents.Add(existingIncident);
                    }

                    // Add Victim if exists
                    if (reader["VictimID"] != DBNull.Value)
                    {
                        Victim victim = new Victim
                        {
                            VictimID = (int)reader["VictimID"],  // Victim_Id from database
                            FirstName = reader["VictimFirstName"].ToString(),
                            LastName = reader["VictimLastName"].ToString(),
                            DateOfBirth = (DateTime)reader["VictimDOB"],
                            Gender = reader["VictimGender"].ToString(),
                            ContactInformation = reader["VictimContact"].ToString()
                        };
                        existingIncident.Victims.Add(victim);
                    }

                    // Add Suspect if exists
                    if (reader["SuspectID"] != DBNull.Value)
                    {
                        Suspect suspect = new Suspect
                        {
                            SuspectID = (int)reader["SuspectID"],  // Suspect_Id from database
                            FirstName = reader["SuspectFirstName"].ToString(),
                            LastName = reader["SuspectLastName"].ToString(),
                            DateOfBirth = (DateTime)reader["SuspectDOB"],
                            Gender = reader["SuspectGender"].ToString(),
                            ContactInformation = reader["SuspectContact"].ToString()
                        };
                        existingIncident.Suspects.Add(suspect);
                    }
                }

                return caseDetails;
            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                throw new IncidentNumberNotFoundException($"An unexpected error occurred while retrieving the case details. Error: {ex.Message}");
            }
            finally
            {
                sqlCon.Close();
            }
        }







        public bool UpdateCaseDetails(Case caseObj)
        {
            try
            {
                // Prepare the query to update the case details
                cmd.Connection = sqlCon;

                StringBuilder query = new StringBuilder();
                query.Append("UPDATE Cases SET Description = @Description WHERE CaseID = @CaseID");

                cmd.CommandText = query.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Description", caseObj.Description);
                cmd.Parameters.AddWithValue("@CaseID", caseObj.CaseId);

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // Case not found, throw IncidentNumberNotFoundException
                    throw new IncidentNumberNotFoundException($"Case with ID {caseObj.CaseId} not found.");
                }

                return true;
            }
            catch (IncidentNumberNotFoundException ex)
            {
                // Handle the specific case not found exception
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            catch (SqlException ex)
            {
                // Handle SQL-specific exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine("Unexpected Error: " + ex.Message);
                return false;
            }
            finally
            {
                // Ensure the connection is closed after execution
                sqlCon.Close();
            }
        }
        public List<Case> GetAllCases()
        {
            List<Case> cases = new List<Case>();

            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "SELECT CaseID, Description FROM Cases";

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int caseId = reader.GetInt32(0);
                    string description =reader.GetString(1);

                    cases.Add(new Case(caseId, description));
                }

                return cases;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return cases;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
                return cases;
            }
            finally
            {
                sqlCon.Close();
            }
        }


    }
}
