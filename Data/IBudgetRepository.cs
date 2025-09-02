using Repository2025.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository2025.Data
{
    public interface IBudgetRepository
    {
        Budget GetById(int id);
        List<Budget> GetAll();
        bool Save(Budget budget);
        bool Delete(int id);
    }
}
