using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoApp_AngJS_WebAPI.Model;

namespace ToDoApp_AngJS_WebAPI.Controllers
{
    public class TaskController : ApiController
    {
        ToDoTaskContext db = new ToDoTaskContext();
        //get all tasks
        [HttpGet]
        public IEnumerable<ToDoTask> Get()
        {
            return db.ToDoTasks.AsEnumerable();
        }

        //get task by id
        public ToDoTask Get(int id)
        {
            ToDoTask task = db.ToDoTasks.Find(id);
            if (task == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            
            return task;
        }//insert task
        public ToDoTask Post(ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                db.ToDoTasks.Add(task);
                db.SaveChanges();
               return task;
            }
           else
           {
               throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
           }
        }//update task
        public HttpResponseMessage Put(int id, ToDoTask task)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != task.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            db.Entry(task).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }//delete task by id
        public HttpResponseMessage Delete(int id)
        {
            ToDoTask task = db.ToDoTasks.Find(id);
            if (task == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            db.ToDoTasks.Remove(task);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, task);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
