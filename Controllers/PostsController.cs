using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Platform2.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Blog.Platform2.Controllers
{
    public class PostsController : Controller
    {
        private PostsContext db = new PostsContext();

        // GET: Posts
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var currentUser = User.Identity.GetUserName();

            if (User.IsInRole("admin"))
            {
                return View(await db.Posts.OrderByDescending(p => p.PostId).ToListAsync());
            }

            return View(await db.Posts.Where(p => p.CreatedBy == currentUser).OrderByDescending(p => p.PostId).ToListAsync());
        }

        // GET: Approve
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Approve()
        {
            return View(await db.Posts.OrderByDescending(p => p.PostId).ToListAsync());
        }

        // POST: Posts/ApprovePost/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApprovePost(int id)
        {
            Post post = await db.Posts.FindAsync(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            foreach (var keyword in post.Keywords)
            {
                keyword.Approved = true;
            }

            post.Approved = true;
            await db.SaveChangesAsync();
            return RedirectToAction("Approve");
        }

        // GET: Posts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var currentUser = User.Identity.GetUserName();

            if (User.IsInRole("admin"))
            {
                return View(post);
            }
            else if (post.CreatedBy == currentUser)
            {
                return View(post);
            }

            return RedirectToAction("Index");
        }


        [Authorize]
        // GET: Posts/Create
        public ActionResult Create()
        {
            PopulateKeywordList();
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,PostTitle,PostContent,Keywords")] Post post, List<int> Selectedkeywords, string newKeywords)
        {
            if (ModelState.IsValid)
            {
                post.Keywords = new List<Keyword>();

                if (Selectedkeywords != null)
                {
                    foreach (var item in Selectedkeywords)
                    {
                        var keyword = db.Keywords.Find(item);
                        post.Keywords.Add(keyword);
                    }

                }

                List<Keyword> NewKeywords = AddNewKeywords(newKeywords);
                if (NewKeywords != null)
                {
                    foreach (var keyword in NewKeywords)
                    {
                        post.Keywords.Add(keyword);
                    }
                }


                post.CreatedBy = User.Identity.GetUserName();
                db.Posts.Add(post);
                db.SaveChanges();

                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Approve");
                }

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            
            if (User.IsInRole("admin") || post.CreatedBy == User.Identity.GetUserName())
            {
                PopulateKeywordListForPost(post.Keywords);
                return View(post);
            }
            else
            {
                return RedirectToAction("Index");
            }          

        }

        
        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostId,PostTitle,PostContent,Keywords")] Post post, List<int> Selectedkeywords = null)
        {
            if (ModelState.IsValid)
            {
                var postToUpdate = db.Posts
                        .Include(i => i.Keywords).First(i => i.PostId == post.PostId);

                if (postToUpdate.CreatedBy != User.Identity.GetUserName() && !User.IsInRole("admin"))
                {
                    return HttpNotFound();
                }

                if (Selectedkeywords != null)
                {
                    var updatedKeywords = new List<int>(Selectedkeywords);
                    foreach (Keyword keyword in db.Keywords)
                    {
                        if (!updatedKeywords.Contains(keyword.KeywordId))
                        {
                            postToUpdate.Keywords.Remove(keyword);
                        }
                        else
                        {
                            postToUpdate.Keywords.Add((keyword));
                        }
                    }
                }
                else
                {
                    foreach (var keyword in postToUpdate.Keywords.ToList())
                    {
                        postToUpdate.Keywords.Remove(keyword);
                    }
                }

                postToUpdate.PostTitle = post.PostTitle;
                postToUpdate.PostContent = post.PostContent;
                postToUpdate.CreatedBy = User.Identity.GetUserName();

                db.Entry(postToUpdate).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Approve");
                }
                return RedirectToAction("Index");

            }
            return View(post);
        }
        
        // POST: Posts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Approve");
            }
            return RedirectToAction("Index");
        }

        // POST: Posts/CreateKeyword
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateKeyword(string newKeywords)
        {
            var isPresent = db.Keywords
                            .Where(k => k.KeywordText == newKeywords)
                            .FirstOrDefault();

            if (isPresent == null)
            {
                Keyword addKeyword = new Keyword();
                addKeyword.KeywordText = newKeywords;
                addKeyword.Approved = false;
                db.Keywords.Add(addKeyword);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }


        // Begin controller functions - must be abetter place for these.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateKeywordList()
        {
            var keywordsQuery = from k in db.Keywords
                                orderby k.KeywordText
                                where k.Approved == true
                                select k;
            ViewBag.KeywordId = keywordsQuery;
        }

        private void PopulateKeywordListForPost (ICollection<Keyword> postKeywords)
        {
            List<int> keywordIdList = new List<int>();
            foreach (var keyword in postKeywords)
            {
                keywordIdList.Add(keyword.KeywordId);
            }
            var keywordsQuery = from k in db.Keywords
                                orderby k.KeywordText
                                //where k.Approved == true
                                select k;
            ViewBag.SelectedKeywords = keywordIdList;
            ViewBag.KeywordId = keywordsQuery;
        }

        public virtual List<Keyword> AddNewKeywords(string newKeywords)
        {
            List<Keyword> newKeywordList = new List<Keyword>();

            List<string> stringList = new List<string>();

            Regex regex = new Regex(@"([A-Za-z\S])\w+");
            Match initMatch = regex.Match(newKeywords);
            if (initMatch.Success)
            {
                MatchCollection matches = regex.Matches(newKeywords);

                foreach (Match match in matches)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        stringList.Add(capture.Value);
                    }
                }

                foreach (var item in stringList)
                {
                    var isPresent = db.Keywords
                                        .Where(k => k.KeywordText == item)
                                        .FirstOrDefault();

                    if (isPresent == null)
                    {
                        Keyword addKeyword = new Keyword();
                        addKeyword.KeywordText = item;
                        addKeyword.Approved = false;
                        newKeywordList.Add(addKeyword);
                    }
                }

                foreach (Keyword newKeyword in newKeywordList)
                    {
                        db.Keywords.Add(newKeyword);
                    }
                    db.SaveChanges();

                    return newKeywordList;
                }           

            return(null);
        }        
        
    }
}
