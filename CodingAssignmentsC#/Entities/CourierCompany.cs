using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Entities
{
   public class CourierCompany
    {
        private string companyName;
        private List<Courier> courierDetails;
        private List<Employee> employeeDetails;
        private List<Location> locationDetails;

        
        public CourierCompany()
        {
            courierDetails = new List<Courier>();
            employeeDetails = new List<Employee>();
            locationDetails = new List<Location>();
        }

    
        public CourierCompany(string companyName, List<Courier> courierDetails, List<Employee> employeeDetails, List<Location> locationDetails)
        {
            this.companyName = companyName;
            this.courierDetails = courierDetails ?? new List<Courier>();
            this.employeeDetails = employeeDetails ?? new List<Employee>();
            this.locationDetails = locationDetails ?? new List<Location>();
        }

    
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public List<Courier> CourierDetails
        {
            get { return courierDetails; }
            set { courierDetails = value; }
        }

        public List<Employee> EmployeeDetails
        {
            get { return employeeDetails; }
            set { employeeDetails = value; }
        }

        public List<Location> LocationDetails
        {
            get { return locationDetails; }
            set { locationDetails = value; }
        }

      
        public override string ToString()
        {
            return $"Company Name: {companyName}, Couriers: {courierDetails.Count}, Employees: {employeeDetails.Count}, Locations: {locationDetails.Count}";
        }
    }
}
