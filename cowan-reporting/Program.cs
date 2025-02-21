using System.Globalization;
using OperationsReporting;
using OperationsReporting.Models;
using OperationsReporting.Repositories;

//var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();

//var host = builder.Build();
//host.Run();
const string midAtlPennCodes = "02,05,07,13,87,36,86,26,80,15,11";

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var dataRepoConnection = configuration["reportingSqlConnectionString"];
var acctRepoConnection = configuration["accountingSqlConnectionString"];

string startDate = "2025-02-09";
string endDate = "2025-02-15";

string startDateEstimation = "2025-02-16";
string endDateEstimation = "2025-02-20";


var repo = new CowanDataRepository(dataRepoConnection);
var acctRepo = new CowanAccountingRepository(acctRepoConnection);

Console.WriteLine($"Start Date: {startDate}");
Console.WriteLine($"End Date: {endDate}");
Console.WriteLine(string.Empty);


foreach (var region in Constants.regions)
{
    var totalMiles = await repo.GetTotalMiles(startDate, endDate, region.Value.Terminals);
    var totalMilesEstimation = await repo.GetTotalMiles(startDateEstimation, endDateEstimation, region.Value.Terminals);

    var revenues = await acctRepo.GetRevenuePerMile(1, 2025, "Asset", region.Value.AccountingCodes);

    decimal totalRevenue = totalMiles * revenues.Item1;
    decimal totalLhRevenue = totalMiles * revenues.Item2;

    Console.WriteLine($"Region: {region.Key}");
    Console.WriteLine($"Total Miles: {totalMiles}");
    Console.WriteLine($"RPM: {revenues.Item1}");
    Console.WriteLine($"LH RPM: {revenues.Item2}");
    Console.WriteLine($"Total Revenue: {totalRevenue}");
    Console.WriteLine($"Total LH Revenue: {totalLhRevenue}");
    Console.WriteLine(string.Empty);
}

