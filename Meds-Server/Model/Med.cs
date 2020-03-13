using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meds_Server.Model
{
    public class Med
    {
        public int id { get; set; }
        public string name { get; set; }
        public int pieces { get; set; }
        public string type { get; set; }
        public string best_before { get; set; }
        public string base_substance { get; set; }
        public string base_substance_quantity { get; set; }
        public string description { get; set; }

    }
    public class CreateMed : Med
    {

    }

    public class ReadMed : Med
    {
        public ReadMed(SqlDataReader row)
        {
            if (row.FieldCount == 2)
            {
                id = row["Id"] != null ? Convert.ToInt32(row["Id"]) : 0;
                name = row["Nume"] != null ? row["Nume"].ToString() : " ";
            }
            else
            {
                id = row["Id"] != null ? Convert.ToInt32(row["Id"]) : 0;
                name = row["Nume"] != null ? row["Nume"].ToString() : " ";
                pieces = row["Buc"] != null ? Convert.ToInt32(row["Buc"]) : 0;
                type = row["Tip"] != null ? row["Tip"].ToString() : " ";
                best_before = row["DataExpirarii"] != null ? row["DataExpirarii"].ToString() : " ";
                base_substance = row["SubstantaBaza"] != null ? row["SubstantaBaza"].ToString() : " ";
                base_substance_quantity = row["SubstBazaCantitate"] != null ? row["SubstBazaCantitate"].ToString() : " ";
                description = row["Descriere"] != null ? row["Descriere"].ToString() : " ";
            }
        }

    }
}
