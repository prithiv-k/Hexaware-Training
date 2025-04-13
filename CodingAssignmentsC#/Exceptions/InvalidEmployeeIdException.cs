using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Exceptions
{
    public class InvalidEmployeeIdException : Exception
        {       

        public InvalidEmployeeIdException(): base("Invalid EmployeeID") { }
    }
    }

