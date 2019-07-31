using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Helper
    {
        public static async Task DeleteAllQueues()
        {
            await Task.Factory.StartNew(() =>
            {
                var machineQueues = MessageQueue.GetPrivateQueuesByMachine(".");
                foreach (var q in machineQueues) MessageQueue.Delete(q.Path);
            });
        }

        public static async Task DeleteAllTables(string connectionString)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC sp_MSforeachtable @command1 = \"DROP TABLE ?\"";
                        command.ExecuteNonQuery();
                    }
                }
            });

        }
    }
}
