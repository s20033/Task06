using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task06.Models;

namespace Task06.Services
{
    public interface IDbService
    {
        IEnumerable<Students> GetStudents();

        Students GetStudentByIndex(string index);

        void SaveLogData(string data);
    }
}
