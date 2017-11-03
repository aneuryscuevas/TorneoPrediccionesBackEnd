using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Backend.Helpers;
using Backend.Models;
using Domain;
using PsTools;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContextLocal _db = new DataContextLocal();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = _db.Users.Include(u => u.FavoriteTeam).Include(u => u.UserType);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.FavoriteLeagueId = new SelectList(_db.Leagues.OrderBy(l=>l.Name), "LeagueId", "Name");
            ViewBag.FavoriteTeamId = new SelectList(_db.Teams.Where(t=>t.LeagueId==_db.Leagues.FirstOrDefault().LeagueId).OrderBy(t=>t.Name), "TeamId", "Name");
            ViewBag.UserTypeId = new SelectList(_db.UserTypes, "UserTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Users";

                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder,"");
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var user = ToUser(view);
                user.Picture = pic;
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                UsersHelper.CreateUserAsp(view.Email, "User", view.Password);
                return RedirectToAction("Index");
            }

            ViewBag.FavoriteLeagueId = new SelectList(_db.Leagues.OrderBy(l => l.Name), "LeagueId", "Name", view.FavoriteLeagueId);
            ViewBag.FavoriteTeamId = new SelectList(_db.Teams.Where(t => t.LeagueId == view.FavoriteLeagueId).OrderBy(t => t.Name), "TeamId", "Name", view.FavoriteTeamId);
            ViewBag.UserTypeId = new SelectList(_db.UserTypes.OrderBy(ut => ut.Name), "UserTypeId", "Name", view.UserTypeId);
            return View(view);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(   UserView view)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var pic = string.Empty;
        //        var folder = "~/Content/Users";

        //        if (view.PictureFile != null)
        //        {
        //            pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
        //            pic = string.Format("{0},{1}", folder, pic);

        //        }
        //        var user = ToUser(view);
        //        user.Picture = pic;
        //        db.Users.Add(user);
        //        await db.SaveChangesAsync();
        //        UsersHelper.CreateUserASP(view.Email,"User", view.Password);
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.FavoriteLeagueId = new SelectList(db.Leagues.OrderBy(l => l.Name), "LeagueId", "Name");
        //    ViewBag.FavoriteTeamId = new SelectList(db.Teams.Where(t => t.LeagueId == db.Leagues.FirstOrDefault().LeagueId).OrderBy(t => t.Name), "TeamId", "Name");
        //    // ViewBag.FavoriteTeamId = new SelectList(db.Teams, "TeamId", "Name", user.FavoriteTeamId);
        //    ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Name", view.UserTypeId);
        //    return View(view);
        //}

        private User ToUser(UserView view)
        {
            return new User
            {
                UserId = view.UserId,
                FirstName = view.FirstName,
                LastName = view.LastName,
                UserTypeId = view.UserTypeId,
                Picture = view.Picture,
                Email = view.Email,
                NickName = view.NickName,
                FavoriteTeamId = view.FavoriteTeamId,
                Points = view.Points,
            };
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.FavoriteTeamId = new SelectList(_db.Teams, "TeamId", "Name", user.FavoriteTeamId);
            ViewBag.UserTypeId = new SelectList(_db.UserTypes, "UserTypeId", "Name", user.UserTypeId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(   User user)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(user).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FavoriteTeamId = new SelectList(_db.Teams, "TeamId", "Name", user.FavoriteTeamId);
            ViewBag.UserTypeId = new SelectList(_db.UserTypes, "UserTypeId", "Name", user.UserTypeId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null) _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
