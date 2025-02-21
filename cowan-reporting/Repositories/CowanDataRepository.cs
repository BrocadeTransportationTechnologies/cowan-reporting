using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace OperationsReporting.Repositories;

public class CowanDataRepository(string connectionString)
{
    public async Task<int> GetTotalMiles(string startDate, string endDate, List<string> terminals)
    {
        int totalMiles = 0;
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("SSRS_RevenueVsPay_COWAN_01052023", connection);

        command.CommandType = System.Data.CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@StartDate", startDate));
        command.Parameters.Add(new SqlParameter("@EndDate", endDate));
        command.Parameters.Add(new SqlParameter("@Grouping", "TA"));
        command.Parameters.Add(new SqlParameter("@Filter", string.Join(",", terminals)));

        command.CommandTimeout = 180;
        await connection.OpenAsync();

        using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (reader.Read())
        {
            totalMiles += reader["LegMiles"] as int? ?? 0;
        }

        return totalMiles;
    }
}
