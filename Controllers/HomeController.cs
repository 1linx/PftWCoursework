using Blog.Platform2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Specialized;
using System.Net;

namespace Blog.Platform2.Controllers
{
    public class HomeController : Controller
    {
        private PostsContext db = new PostsContext();

        public ActionResult Index()
        {
            ViewBag.NumberOfPosts = GetPosts().Count();
            return View();
        }

        public JsonResult Json()
        {
            
            var getPosts = GetPosts();
            return Json(getPosts, JsonRequestBehavior.AllowGet);
        }

        
        // Methods - should this live somewhere else?                        
        private Dictionary<object, List<object>> GetPosts()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var postsQuery = db.Posts
                               .Include(p => p.Keywords)
                               .Where(p => p.Approved == true)
                               .OrderByDescending(p => p.PostId)
                               .ToList();
                               //.Take(5);

            var postSanitiser = new Dictionary<object, List<object>>();
            foreach (var item in postsQuery)
            {
                var contentList = new List<object>();
                contentList.Add(item.PostTitle);
                contentList.Add(item.PostContent);
                contentList.Add(item.CreatedBy);
                postSanitiser.Add(item.PostId.ToString(), contentList);

                var similarPosts = new List<Post>();
                foreach (var keys in item.Keywords)
                {
                    foreach (var i in keys.Posts)
                    {
                        if (i != item)
                        {
                            similarPosts.Add(i);
                        }
                    }
                }
                var count = 0;
                foreach (var simPost in similarPosts)
                {
                    contentList.Add(simPost.PostId.ToString());
                    contentList.Add(simPost.PostTitle);
                    count++;
                    if (count == 3)
                    {
                        break;
                    }

                }

            }

            return postSanitiser;
        }

    }

}