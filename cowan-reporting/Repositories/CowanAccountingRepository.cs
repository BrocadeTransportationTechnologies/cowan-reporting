using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace OperationsReporting.Repositories;

public class CowanAccountingRepository(string connectionString)
{
    public async Task<(decimal, decimal)> GetRevenuePerMile(int month, int year, string division, string depts, string format = "Monthly")
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("COWAN_sp_GetAssetStats", connection);

        command.CommandType = System.Data.CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@format", format));
        command.Parameters.Add(new SqlParameter("@fiscPer", month));
        command.Parameters.Add(new SqlParameter("@fiscYear", year));
        command.Parameters.Add(new SqlParameter("@divStr", division));
        command.Parameters.Add(new SqlParameter("@deptStr", depts));

        command.CommandTimeout = 180;
        await connection.OpenAsync();

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        decimal rpm = 0;
        decimal lhrpm = 0;

        while (reader.Read())
        {
            var lineDesc = reader["LineDesc"] as string ?? string.Empty;
            var columnHeading = reader["ColumnHeading"] as string ?? string.Empty;

            if (lineDesc == "Revenue per Mile"
                && columnHeading == string.Format("Trailing Qtr\r\n{0}  / {1}", month, year))
            {
                rpm = reader["Amount"] as decimal? ?? 0;
            }

            if (lineDesc == "Net Revenue per Mile"
                && columnHeading == string.Format("Trailing Qtr\r\n{0}  / {1}", month, year))
            {
                lhrpm = reader["Amount"] as decimal? ?? 0;
            }
        }

        //Console.WriteLine($"RPM: {rpm}");

        return (Math.Round(rpm, 2), Math.Round(lhrpm, 2));
    }
}
