using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    public class MedsController : ControllerBase
    {
        DateTime today = DateTime.Today;

        [HttpGet()]
        public IEnumerable<Meds> Get()
        {
            using (var db = new MedsServerContext())
            {
                Console.WriteLine("Get meds\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
                return db.Meds.ToList(); // .Select(u => new { u.Id, u.Name, 0, "", "", "", "", "", ""} )
            }
        }

        //GET: api/Meds/sorted
        [HttpGet("{command}")]
        public IEnumerable<Meds> Get(string command)
        {
            using (var db = new MedsServerContext())
            {
                Console.WriteLine("Get meds {3}\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), command);
                if (command.Equals("sorted"))
                {
                    return db.Meds.OrderBy(x => x.Name).ToList();
                }
                else if (command.Equals("date"))
                {
                    return db.Meds.Where(x => x.BestBefore < today).ToList();
                }
                else
                {
                    return new List<Meds>();
                }
            }
        }

        //GET: api/Meds/date/sorted || api/date/null
        [HttpGet("{first}/{second}")]
        public IEnumerable<Meds> Get(string first, string second)
        {
            using (var db = new MedsServerContext())
            {
                if (first.Equals("date"))
                {
                    if (second.Equals("sorted"))
                    {
                        Console.WriteLine("Get meds {3} sorted by name \t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), first);
                        return db.Meds.Where(x => x.BestBefore < today).OrderBy(x => x.Name).ToList();
                    }
                    else
                    {
                        Console.WriteLine("Get meds {3} - {4} returns empty list\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), first, second);
                        return new List<Meds>();
                    }
                }
                else
                {
                    Console.WriteLine("Get meds {3} - {4} returns empty list\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), first, second);
                    return new List<Meds>();
                }
            }
        }

        //GET: api/Meds/5
        [HttpGet("{id:int}")]
        public Meds Get(int id)
        {
            using (var db = new MedsServerContext())
            {
                try
                {
                    Console.WriteLine("Get med with id {3}\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), id);
                    return db.Meds.Where(x => x.Id == id).First<Meds>();
                } 
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Failed! There was and error!");
                    return new Meds();
                }
            }
        }

        //POST: api/Meds
        [HttpPost()]
        public string Post([FromBody]Meds value)
        {
            using (var db = new MedsServerContext())
            {
                try
                {
                    db.Meds.Add(value);
                    db.SaveChanges();
                    Console.WriteLine("Added med with name {3}\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), value.Name);
                    return true.ToString();
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Failed! There was and error!");
                    return false.ToString();
                }
            }
        }

        //PUT: api/Med/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody]Meds value)
        {
            using (var db = new MedsServerContext())
            {
                try
                {
                    var entity = db.Meds.Where(x => x.Id == id).First<Meds>();

                    entity.Name = value.Name;
                    entity.Pieces = value.Pieces;
                    entity.Type = value.Type;
                    entity.BestBefore = value.BestBefore;
                    entity.Picture = value.Picture;
                    entity.BaseSubstance = value.BaseSubstance;
                    entity.BaseSubstanceQuantity = value.BaseSubstanceQuantity;
                    entity.Description = value.Description;

                    db.SaveChanges();
                    Console.WriteLine("Updated med with name {3}\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), value.Name);
                    return true.ToString();
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Failed! There was and error!");
                    return false.ToString();
                }
            }
        }

        //DELETE: api/Med/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            using (var db = new MedsServerContext())
            {
                try
                {
                    var entity = db.Meds.Where(x => x.Id == id).First<Meds>();
                    db.Meds.Remove(entity);
                    db.SaveChanges(); 
                    Console.WriteLine("Deleted med with id {3}\t {0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), id);
                    return true.ToString();
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Failed! There was and error!");
                    return false.ToString();
                }
            }
        }
    }
}
