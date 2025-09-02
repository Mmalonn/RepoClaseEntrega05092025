using Microsoft.Data.SqlClient;
using Repository2025.Data.Helper;
using Repository2025.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository2025.Data
{
    public class BudgetRepository : IBudgetRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Budget> GetAll()
        {
            throw new NotImplementedException();
        }

        public Budget GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Budget budget)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction t = null;
            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();

            }
            catch (SqlException e)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
    }
}
