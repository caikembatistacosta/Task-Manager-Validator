using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace StatusDemanda
{
    public class Function1
    {

        //create procedure sp_verificarDemandasExpiradas
        //as
        //update[dbo].[DEMANDAS] set statusDaDemanda = 3 where getdate() > datafim and statusdademanda<> 2
        //go
        [FunctionName("VerificarDemandasExpiradas")]
        public void Run([TimerTrigger("* * 23 * * *")] TimerInfo myTimer, ILogger log)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.ConnectionString = "Server=tcp:taskmanagervalidator.database.windows.net,1433;Initial Catalog=TaskManagerValidator;Persist Security Info=False;User ID=cmbcosta;Password=Senhasenha@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                try
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_verificarDemandasExpiradas";
                    command.ExecuteNonQuery();
                    log.LogInformation("deucerto");
                }
                catch (Exception)
                {

               
                }
            }
        }
    }
}
