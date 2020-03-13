using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meds_Server.Utils
{
    public static class QueryMed
    {
        private static string query_select_Id_Name = "SELECT ID, Nume FROM Medicamente";

        private static string query_select_Id = "SELECT * FROM Medicamente WHERE id = ";

        private static string query_insert = "INSERT INTO Medicamente(Nume, Buc, Tip, DataExpirarii, SubstantaBaza, SubstBazaCantitate, Descriere) " +
                                    "VALUES (@name, @pieces, @type, @best_before, @base_substance, @base_substance_quantity, @description)";

        private static string query_minimum_insert = "INSERT INTO Medicamente(Nume, Buc, Tip, DataExpirarii) VALUES (@name, @pieces, @type, @best_before)";

        private static string query_update = "UPDATE Medicamente SET name=@name, pieces=@pieces, type=@type, best_before=@best_before, base_substance=@base_substance, " +
                                    "base_substance_quantity=@base_substance_quantity, description=@description WHERE id=";

        private static string query_delete = "DELETE FROM Medicamente WHERE id=";

        private static string order_by_name = " ORDER BY Nume";

        private static string filter_by_best_before = "DataExpirarii < (select GETDATE())";


        public static string GetConnectionString()
        {
            return @"Data Source = CIPRI-ROG\SQLEXPRESS; " +
                    "Initial Catalog = Medicamente_DB; " +
                    "Integrated Security = True; " +
                    "MultipleActiveResultSets = True;";
        }

        public static string getIdAndName() { return query_select_Id_Name; }

        public static string getId() { return query_select_Id; }

        public static string getInsert() { return query_insert; }

        public static string getMinimumInsert() { return query_minimum_insert; }

        public static string getUpdate() { return query_update; }

        public static string getDelete() { return query_delete; }

        public static string getFilterBestBefore() { return filter_by_best_before; }

        public static string getOrderName() { return order_by_name; }
    }
}