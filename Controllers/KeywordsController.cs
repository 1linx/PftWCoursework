using Blog.Platform2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Blog.Platform2.Controllers
{
    public class KeywordsController : Controller
    {
        private PostsContext db = new PostsContext();

        // GET: Keywords
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {

            return View(db.Keywords.ToList());
        }
        
        // GET: Keywords/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Keywords/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeywordText")] String KeywordText)
        {

            if (ModelState.IsValid)
                {
                    List<Keyword> NewKeywords = AddNewKeywords(KeywordText);
                if (NewKeywords != null)
                {
                    foreach (var keyword in NewKeywords)
                    {
                        keyword.Approved = false;
                        db.Keywords.Add(keyword);
                    }
                }          
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Keywords/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View(db.Keywords.Find(id));
        }

        // POST: Keywords/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeywordId,KeywordText,Approved")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyword).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }


        // POST: Keywords/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Keyword keyword = await db.Keywords.FindAsync(id);

            if (keyword == null)
            {
                return HttpNotFound();
            }

            db.Keywords.Remove(keyword);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Keywords/Approve/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve(int id)
        {
            Keyword keyword = await db.Keywords.FindAsync(id);

            if (keyword == null)
            {
                return HttpNotFound();
            }

            keyword.Approved = true;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

            return (null);
        }
    }
}
