using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using Domain;

namespace API.Controllers
{
    //[Authorize]
    [Authorize(Roles = "User")]
    public class LeaguesController : ApiController
    {
        private DataContext db = new DataContext();

        public async Task<IHttpActionResult> GetLeagues()
        {
            var leagues = await db.Leagues.ToListAsync();
            var leaugesResponse = new List<LeagueResponse>();
            foreach (var league in leagues)
            {
                leaugesResponse.Add(new LeagueResponse
                {
                    LeagueId = league.LeagueId,
                    Logo = league.Logo,
                    Name = league.Name,
                    Teams = league.Teams.ToList(),
                });
            }

            return Ok(leaugesResponse);
        }

        //// GET: api/Leagues/5
        //[ResponseType(typeof(League))]
        //public async Task<IHttpActionResult> GetLeague(int id)
        //{
        //    var league = await db.Leagues.FindAsync(id);
        //    if (league == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(league);
        //}

        //// PUT: api/Leagues/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutLeague(int id, League league)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != league.LeagueId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(league).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LeagueExists(id))
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

        //// POST: api/Leagues
        //[ResponseType(typeof(League))]
        //public async Task<IHttpActionResult> PostLeague(League league)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Leagues.Add(league);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = league.LeagueId }, league);
        //}

        //// DELETE: api/Leagues/5
        //[ResponseType(typeof(League))]
        //public async Task<IHttpActionResult> DeleteLeague(int id)
        //{
        //    var league = await db.Leagues.FindAsync(id);
        //    if (league == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Leagues.Remove(league);
        //    await db.SaveChangesAsync();

        //    return Ok(league);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeagueExists(int id)
        {
            return db.Leagues.Count(e => e.LeagueId == id) > 0;
        }
    }
}