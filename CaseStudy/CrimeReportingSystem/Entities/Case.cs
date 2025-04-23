using CrimeReportingSystem.Entities;

public class Case
{
    public int CaseId { get; set; }
    public string Description { get; set; }
    public List<Incident> Incidents { get; set; }

    public Case() { }

    public Case(int caseId, string description)
    {
        CaseId = caseId;
        Description = description;
        Incidents = new List<Incident>();
    }
}
