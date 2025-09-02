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
                //trae instancia de la coneccion
                cnn = DataHelper.GetInstance().GetConnection();
                //la abre
                cnn.Open();
                //inicia transaccion
                t = cnn.BeginTransaction();
                //declara el comando para iniciar transacion
                var cmd = new SqlCommand("SP_INSERTAR_MAESTRO", cnn, t);
                //crea los parametros
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cliente", budget.Cliente);
                cmd.Parameters.AddWithValue("@vigencia", budget.Expiration);
                //lineas para traer parametro que devuelve el store procedure y poder asignarle 
                //el id del presupuesto a los detalles
                SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                //ejecuta la query
                cmd.ExecuteNonQuery();
                //lee el parametro que trae el id
                int budgetId = (int)param.Value;
                //recorre la lista de detalles para meterlos a un sp
                //que va insertar los detalles en el presupuesto anterior
                int detailId = 1;
                foreach (var detail in budget.Details)
                {
                    var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetail.CommandType = CommandType.StoredProcedure;
                    cmdDetail.Parameters.AddWithValue("@detalle", detailId);
                    cmdDetail.Parameters.AddWithValue("@presupuesto", budgetId);
                    cmdDetail.Parameters.AddWithValue("@producto", detail.Product.Codigo);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detail.Count);
                    cmdDetail.Parameters.AddWithValue("@precio", detail.Price);
                    cmdDetail.ExecuteNonQuery();
                    detailId++;
                }
                t.Commit();
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
