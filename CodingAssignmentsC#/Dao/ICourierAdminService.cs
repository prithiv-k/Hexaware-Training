using System;

namespace CodingAssignmentsC_.Dao
{
    public interface ICourierAdminService<T>
    {
        int AddCourierStaff(T entity);
    }
}
