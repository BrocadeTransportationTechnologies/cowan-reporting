using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsReporting.Models;

public static class Constants
{
    public static Dictionary<string, Region> regions = new Dictionary<string, Region>
    {
        {
            "Mid-Atlantic/Pennsylvania",
            new Region
            {
                RegionName = "Mid-Atlantic/Pennsylvania",
                Terminals = new List<string>
                            {
                                "BEARVA", "COKSAN", "COKCAP", "VAMALO", "RMG",
                                "SPARMD", "RIVVA", "CARLIS", "BURLOC", "SHELL",
                                "TWI"
                            }, //per scott ford 2/21 - remove New Castle (11) - HAMMD
                AccountingCodes = "02,05,07,13,87,36,86,26,80,15"
            }
        },
        {
            "Northeast/Midwest",
            new Region
            {
                RegionName = "Northeast/Midwest",
                Terminals = new List<string>
                            {
                                "COKNOR", "CIN", "HICK", "BJSDED", "MICBER",
                                "BJSUXB", "ABER", "TNEDED", "PORTIN", "BOWGRN"
                            },
                AccountingCodes = "03,12,21,24,29,62,66,67,79,39"
            }
        },
        {
            "Southeast/West",
            new Region
            {
                RegionName = "Southeast/West",
                Terminals = new List<string>
                            {
                                "SOCGA", "COCFL", "JAC", "SLCSHT", "WAGCAS",
                                "NATRO", "NTRU", "AHENDE", "CHATN", "COKUTD"
                            },
                AccountingCodes = "16,28,32,38,61,63,64,69,78,89"
            }
        }
    };
};
