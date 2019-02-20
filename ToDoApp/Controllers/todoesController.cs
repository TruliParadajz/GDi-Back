using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ToDoApp.DAL;
using ToDoApp.Models;
using System.Web.Http.Cors;

namespace ToDoApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TodoesController : ApiController
    {
        private context db = new context();

        // GET: api/todoes
        public IQueryable<todo> GetTodos()
        {
            return db.Todos;
        }

        // GET: api/todoes/5
        [ResponseType(typeof(todo))]
        public IHttpActionResult Gettodo(int id)
        {
            todo todo = db.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/todoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttodo(int id, todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todo.id)
            {
                return BadRequest();
            }

            db.Entry(todo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!todoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/todoes
        [ResponseType(typeof(todo))]
        public IHttpActionResult Posttodo(todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Todos.Add(todo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todo.id }, todo);
        }

        // DELETE: api/todoes/5
        [ResponseType(typeof(todo))]
        public IHttpActionResult Deletetodo(int id)
        {
            todo todo = db.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            db.Todos.Remove(todo);
            db.SaveChanges();

            return Ok(todo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool todoExists(int id)
        {
            return db.Todos.Count(e => e.id == id) > 0;
        }
    }
}