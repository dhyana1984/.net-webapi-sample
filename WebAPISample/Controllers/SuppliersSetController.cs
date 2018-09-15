using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebAPISample.Models;

namespace WebAPISample.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebAPISample.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Supplier>("SuppliersSet");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SuppliersSetController : ODataController
    {
        private ODataContext db = new ODataContext();

        // GET: odata/SuppliersSet
        [EnableQuery]
        public IQueryable<Supplier> GetSuppliersSet()
        {
            return db.Suppliers;
        }

        // GET: odata/SuppliersSet(5)
        [EnableQuery]
        public SingleResult<Supplier> GetSupplier([FromODataUri] string key)
        {
            return SingleResult.Create(db.Suppliers.Where(supplier => supplier.Key == key));
        }

        // PUT: odata/SuppliersSet(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<Supplier> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Supplier supplier = db.Suppliers.Find(key);
            if (supplier == null)
            {
                return NotFound();
            }

            patch.Put(supplier);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(supplier);
        }

        // POST: odata/SuppliersSet
        public IHttpActionResult Post(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Suppliers.Add(supplier);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SupplierExists(supplier.Key))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(supplier);
        }

        // PATCH: odata/SuppliersSet(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<Supplier> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Supplier supplier = db.Suppliers.Find(key);
            if (supplier == null)
            {
                return NotFound();
            }

            patch.Patch(supplier);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(supplier);
        }

        // DELETE: odata/SuppliersSet(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            Supplier supplier = db.Suppliers.Find(key);
            if (supplier == null)
            {
                return NotFound();
            }

            db.Suppliers.Remove(supplier);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierExists(string key)
        {
            return db.Suppliers.Count(e => e.Key == key) > 0;
        }
    }
}
