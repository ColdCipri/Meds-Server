using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Meds_Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meds_Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MedController : ControllerBase
    {
        [HttpGet()]
        public IEnumerable<Med> Get()
        {
            using var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString());
            var querySelect = Utils.QueryMed.getIdAndName();
            _conn.Open();
            using var cmd = new SqlCommand(querySelect, _conn);
            using var reader = cmd.ExecuteReader();
            List<Med> meds = new List<Med>();
            while (reader.Read())
            {
                meds.Add(new ReadMed(reader));
            }
            Console.WriteLine("Get meds\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
            return meds;
        }

        //GET: api/Med/sorted
        [HttpGet("{command}")]
        public IEnumerable<Med> Get(string command)
        {
            using var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString());
            string querySelect = "";
            if (command.Equals("sorted"))
            {
                querySelect = Utils.QueryMed.getIdAndName() + Utils.QueryMed.getOrderName();
                _conn.Open();
                using var cmd = new SqlCommand(querySelect, _conn);
                using var reader = cmd.ExecuteReader();
                List<Med> meds = new List<Med>();
                while (reader.Read())
                {
                    meds.Add(new ReadMed(reader));
                }
                Console.WriteLine("Get meds {3}\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), command);

                return meds;
            }
            else if (command.Equals("date"))
            {
                querySelect = Utils.QueryMed.getIdAndName() + " WHERE " + Utils.QueryMed.getFilterBestBefore();
                _conn.Open();
                using var cmd = new SqlCommand(querySelect, _conn);
                using var reader = cmd.ExecuteReader();
                List<Med> meds = new List<Med>();
                while (reader.Read())
                {
                    meds.Add(new ReadMed(reader));
                }
                Console.WriteLine("Get meds {3}\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), command);

                return meds;
            }
            else
            {
                Console.WriteLine("Get meds {3} returns empty list\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), command);

                return new List<Med>();
            }
        }

        //GET: api/date/sorted || api/date/null
        [HttpGet("{first}/{second}")]
        public IEnumerable<Med> Get(string first, string second)
        {
            using var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString());
            string querySelect = "";
            if (first.Equals("date"))
            {
                if (second.Equals("sorted"))
                {
                    querySelect = Utils.QueryMed.getIdAndName() + " WHERE " + Utils.QueryMed.getFilterBestBefore() + Utils.QueryMed.getOrderName();
                    _conn.Open();
                    using var cmd = new SqlCommand(querySelect, _conn);
                    using var reader = cmd.ExecuteReader();
                    List<Med> meds = new List<Med>();
                    while (reader.Read())
                    {
                        meds.Add(new ReadMed(reader));
                    }
                    Console.WriteLine("Get meds {3} sorted by name \t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), first);

                    return meds;
                }
                else
                {
                    Console.WriteLine("Get meds {3} - {4} returns empty list\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), first, second);

                    return new List<Med>();
                }
            }
            else
            {
                Console.WriteLine("Get meds {3} - {4} returns empty list\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), first, second);

                return new List<Med>();
            }
        }

        // GET: api/Med/5
        [HttpGet("{id:int}")]
        public IEnumerable<Med> Get(int id)
        {
            using (var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString()))
            {
                var querySelect = Utils.QueryMed.getId() + id;
                _conn.Open();
                using (var cmd = new SqlCommand(querySelect, _conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {

                        List<Med> meds = new List<Med>();
                        while (reader.Read())
                        {
                            meds.Add(new ReadMed(reader));
                        }

                        Console.WriteLine("Get med by id\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
                        return meds;
                    }
                }
            }
        }

        /*// GET: api/Meds/user
        [HttpGet("{user}")]
        public IEnumerable<Med> Get(string user)
        {
            using (var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString()))
            {
                var querySelect = Utils.QueryMed.getSelectUser() + user + "'";
                _conn.Open();
                using (var cmd = new SqlCommand(querySelect, _conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Med> meds = new List<Med>();
                        while (reader.Read())
                        {
                            meds.Add(new ReadMed(reader));
                        }
                        return meds;

                    }
                }
            }
        }*/

        // POST: api/Med
        [HttpPost()]
        public string Post([FromBody]CreateMed value)
        {
            using var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString());

            var queryInsert = "";
            var result = 0;

            if (value.base_substance == null && value.base_substance_quantity == null && value.description == null)
            {
                queryInsert = Utils.QueryMed.getMinimumInsert();
                using SqlCommand insertCommand = new SqlCommand(queryInsert, _conn);
                insertCommand.Parameters.AddWithValue("@name", value.name);
                insertCommand.Parameters.AddWithValue("@pieces", value.pieces);
                insertCommand.Parameters.AddWithValue("@type", value.type);
                insertCommand.Parameters.AddWithValue("@best_before", value.best_before);

                _conn.Open();


                Console.WriteLine("Post 4 atributes\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
                result = insertCommand.ExecuteNonQuery();
            }
            else
            {
                queryInsert = Utils.QueryMed.getInsert();
                using SqlCommand insertCommand = new SqlCommand(queryInsert, _conn);

                insertCommand.Parameters.AddWithValue("@name", value.name);
                insertCommand.Parameters.AddWithValue("@pieces", value.pieces);
                insertCommand.Parameters.AddWithValue("@type", value.type);
                insertCommand.Parameters.AddWithValue("@best_before", value.best_before);
                insertCommand.Parameters.AddWithValue("@base_substance", value.base_substance);
                insertCommand.Parameters.AddWithValue("@base_substance_quantity", value.base_substance_quantity);
                insertCommand.Parameters.AddWithValue("@description", value.description);

                _conn.Open();

                Console.WriteLine("Post all atributes\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
                result = insertCommand.ExecuteNonQuery();
            }

            if (result > 0)
                return "true";
            else
                return "false";
        }

        // PUT: api/Med/5
        [HttpPut("{id}")]

        public string Put(int id, [FromBody]CreateMed value)
        {
            using var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString());

            var queryUpdate = Utils.QueryMed.getUpdate() + id;
            SqlCommand updateCommand = new SqlCommand(queryUpdate, _conn);

            updateCommand.Parameters.AddWithValue("@name", value.name);
            updateCommand.Parameters.AddWithValue("@pieces", value.pieces);
            updateCommand.Parameters.AddWithValue("@type", value.type);
            updateCommand.Parameters.AddWithValue("@best_before", value.best_before);
            updateCommand.Parameters.AddWithValue("@base_substance", value.base_substance);
            updateCommand.Parameters.AddWithValue("@base_substance_quantity", value.base_substance_quantity);
            updateCommand.Parameters.AddWithValue("@description", value.description);

            _conn.Open();
            Console.WriteLine("Put\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
            int result = updateCommand.ExecuteNonQuery();
            if (result > 0)
                return "true";
            else
                return "false";
        }

        // DELETE: api/Med/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            using var _conn = new SqlConnection(Utils.QueryMed.GetConnectionString());

            var queryDelete = Utils.QueryMed.getDelete() + id;
            SqlCommand deleteCommand = new SqlCommand(queryDelete, _conn);

            _conn.Open();
            Console.WriteLine("Delete\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
            int result = deleteCommand.ExecuteNonQuery();
            if (result > 0)
                return "true";
            else
                return "false";
        }
    }
}
