using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Model;
using ORM_GSB;

namespace Back_GSB.Controllers
{
    public class DepartementsController : ApiController
    {
        private GSB_Data_Model db = new GSB_Data_Model();

        // GET: api/Departements
        public IQueryable<Departement> GetDepartements()
        {
            return db.Departements;
        }

        // GET: api/Departements/id
        [ResponseType(typeof(Departement))]
        public async Task<IHttpActionResult> GetDepartement(int id)
        {
            Departement departement = await db.Departements.FindAsync(id);
            if (departement == null)
            {
                return NotFound();
            }

            return Ok(departement);
        }

        // PUT: api/Departements/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutDepartement(int id, Departement departement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != departement.dep_numero)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(departement).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartementExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Departements
        //[ResponseType(typeof(Departement))]
        //public async Task<IHttpActionResult> PostDepartement(Departement departement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Departements.Add(departement);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DepartementExists(departement.dep_numero))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = departement.dep_numero }, departement);
        //}

        // DELETE: api/Departements/5
        //[ResponseType(typeof(Departement))]
        //public async Task<IHttpActionResult> DeleteDepartement(int id)
        //{
        //    Departement departement = await db.Departements.FindAsync(id);
        //    if (departement == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Departements.Remove(departement);
        //    await db.SaveChangesAsync();

        //    return Ok(departement);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool DepartementExists(int id)
        //{
        //    return db.Departements.Count(e => e.dep_numero == id) > 0;
        //}
    }
}