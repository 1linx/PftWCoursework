using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.Platform2.Models
{
    public class UserDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        bool AddUserAndRole(Blog.Platform2.Models.ApplicationDbContext context, string username, string password, string role)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole(role));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = username,
                Email = username,
            };
            ir = um.Create(user, password);
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, role);
            return ir.Succeeded;
        }

        protected override void Seed(Blog.Platform2.Models.ApplicationDbContext context)
        {

            AddUserAndRole(context, "admin@example.com", "P4ssword!", "admin");
            AddUserAndRole(context, "user@example.com", "P4ssword!", "user");

        }
    }

    public class BlogDbInitializer : DropCreateDatabaseIfModelChanges<PostsContext>
    {
        protected override void Seed(PostsContext context)
        {

            var post1 = new Post { PostTitle = "The history of typefaces", PostContent = "Nunc sodales fringilla lorem, vel pulvinar purus congue at. Sed dapibus neque nibh. Proin auctor ipsum at sem faucibus, a rhoncus massa commodo. Nulla fringilla auctor justo, et egestas arcu vehicula ac. Phasellus orci arcu, iaculis sit amet mollis porta, suscipit ut libero.Suspendisse mattis efficitur eros id viverra. Curabitur bibendum neque lectus, at condimentum metus maximus vitae. Sed fringilla ornare leo, quis sollicitudin lectus fermentum sit amet. Nam scelerisque vehicula accumsan. Nulla eget posuere elit.Vestibulum tempus, dolor eu blandit rhoncus, turpis lacus ornare nunc, posuere feugiat orci dui et ex.Duis venenatis ullamcorper massa, quis maximus diam ultrices id. Mauris massa lorem, tristique non libero tincidunt, viverra gravida leo. Quisque ornare auctor vulputate. Phasellus egestas diam eleifend sem posuere, id consequat tortor imperdiet.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post2 = new Post { PostTitle = "Perspective on William Caslon", PostContent = "Phasellus orci arcu, iaculis sit amet mollis porta, suscipit ut libero. Suspendisse mattis efficitur eros id viverra. Curabitur bibendum neque lectus, at condimentum metus maximus vitae. Sed fringilla ornare leo, quis sollicitudin lectus fermentum sit amet. Nam scelerisque vehicula accumsan. Nulla eget posuere elit.Vestibulum tempus, dolor eu blandit rhoncus, turpis lacus ornare nunc, posuere feugiat orci dui et ex. Duis venenatis ullamcorper massa, quis maximus diam ultrices id. Mauris massa lorem, tristique non libero tincidunt, viverra gravida leo. Quisque ornare auctor vulputate. Phasellus egestas diam eleifend sem posuere, id consequat tortor imperdiet. Vestibulum mauris nisl, pharetra a vulputate nec, ultrices non metus. Fusce sit amet ornare est.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post3 = new Post { PostTitle = "Dutch Baroque in typography", PostContent = "Nunc sodales fringilla lorem, vel pulvinar purus congue at. Sed dapibus neque nibh. Proin auctor ipsum at sem faucibus, a rhoncus massa commodo. Nulla fringilla auctor justo, et egestas arcu vehicula ac. Phasellus orci arcu, iaculis sit amet mollis porta, suscipit ut libero.Suspendisse mattis efficitur eros id viverra. Curabitur bibendum neque lectus, at condimentum metus maximus vitae. Sed fringilla ornare leo, quis sollicitudin lectus fermentum sit amet. Nam scelerisque vehicula accumsan. Nulla eget posuere elit. Vestibulum tempus, dolor eu blandit rhoncus, turpis lacus ornare nunc, posuere feugiat orci dui et ex.Duis venenatis ullamcorper massa, quis maximus diam ultrices id. Mauris massa lorem, tristique non libero tincidunt, viverra gravida leo. Quisque ornare auctor vulputate. Phasellus egestas diam eleifend sem posuere, id consequat tortor imperdiet. Vestibulum mauris nisl, pharetra a vulputate nec, ultrices non metus. Fusce sit amet ornare est.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post4 = new Post { PostTitle = "Humanistic designs", PostContent = "Suspendisse metus tortor, malesuada ut tempus vel, porta sit amet justo. Nam risus nunc, vehicula vel libero at, suscipit sagittis odio. Sed tincidunt, tellus et efficitur vehicula, nunc nunc sollicitudin urna, dignissim tempor nisl erat luctus sem. Donec bibendum elit est, faucibus ultrices velit maximus nec. Sed a egestas tellus, vel consequat risus. Duis pellentesque ex fermentum congue lacinia. Praesent malesuada porta enim non commodo. Donec vehicula, velit eu luctus tempus, sapien felis ornare urna, sed pulvinar nisl nulla volutpat ante. Phasellus vehicula libero ac nunc dapibus, id rhoncus magna luctus.Mauris luctus lacus sagittis enim ornare, in condimentum ante vulputate. Morbi nec convallis libero. Cras laoreet, purus ac blandit pellentesque, nibh orci mollis ligula, eu ullamcorper orci ante quis purus. Etiam scelerisque diam in eros mollis varius. Proin suscipit lorem pulvinar dignissim suscipit. Donec pretium massa id enim luctus, at finibus purus semper.Vestibulum tempus, dolor eu blandit rhoncus, turpis lacus ornare nunc, posuere feugiat orci dui et ex. Duis venenatis ullamcorper massa, quis maximus diam ultrices id. Mauris massa lorem, tristique non libero tincidunt, viverra gravida leo. Quisque ornare auctor vulputate. Phasellus egestas diam eleifend sem posuere, id consequat tortor imperdiet. Vestibulum mauris nisl, pharetra a vulputate nec, ultrices non metus. Fusce sit amet ornare est.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post5 = new Post { PostTitle = "Fonts in the public domain", PostContent = "Nam risus nunc, vehicula vel libero at, suscipit sagittis odio. Sed tincidunt, tellus et efficitur vehicula, nunc nunc sollicitudin urna, dignissim tempor nisl erat luctus sem. Donec bibendum elit est, faucibus ultrices velit maximus nec.Sed a egestas tellus, vel consequat risus. Duis pellentesque ex fermentum congue lacinia. Praesent malesuada porta enim non commodo. Donec vehicula, velit eu luctus tempus, sapien felis ornare urna, sed pulvinar nisl nulla volutpat ante. Phasellus vehicula libero ac nunc dapibus, id rhoncus magna luctus. Mauris luctus lacus sagittis enim ornare, in condimentum ante vulputate. Morbi nec convallis libero. Cras laoreet, purus ac blandit pellentesque, nibh orci mollis ligula, eu ullamcorper orci ante quis purus.Nunc sodales fringilla lorem, vel pulvinar purus congue at. Sed dapibus neque nibh. Proin auctor ipsum at sem faucibus, a rhoncus massa commodo. Nulla fringilla auctor justo, et egestas arcu vehicula ac. Phasellus orci arcu, iaculis sit amet mollis porta, suscipit ut libero. Suspendisse mattis efficitur eros id viverra. Curabitur bibendum neque lectus, at condimentum metus maximus vitae. Sed fringilla ornare leo, quis sollicitudin lectus fermentum sit amet. Nam scelerisque vehicula accumsan. Nulla eget posuere elit.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post6 = new Post { PostTitle = "Italics and Roman type", PostContent = "Nunc sodales fringilla lorem, vel pulvinar purus congue at. Sed dapibus neque nibh. Proin auctor ipsum at sem faucibus, a rhoncus massa commodo. Nulla fringilla auctor justo, et egestas arcu vehicula ac. Vestibulum mauris nisl, pharetra a vulputate nec, ultrices non metus. Fusce sit amet ornare est. Phasellus orci arcu, iaculis sit amet mollis porta, suscipit ut libero. Suspendisse mattis efficitur eros id viverra. Curabitur bibendum neque lectus, at condimentum metus maximus vitae. Sed fringilla ornare leo, quis sollicitudin lectus fermentum sit amet.Nam scelerisque vehicula accumsan. Nulla eget posuere elit. Vestibulum tempus, dolor eu blandit rhoncus, turpis lacus ornare nunc, posuere feugiat orci dui et ex. Duis venenatis ullamcorper massa, quis maximus diam ultrices id. Mauris massa lorem, tristique non libero tincidunt, viverra gravida leo. Quisque ornare auctor vulputate.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post7 = new Post { PostTitle = "Reflections on type design", PostContent = "Nulla fringilla auctor justo, et egestas arcu vehicula ac. Phasellus orci arcu, iaculis sit amet mollis porta, suscipit ut libero. Suspendisse mattis efficitur eros id viverra. Curabitur bibendum neque lectus, at condimentum metus maximus vitae. Nam scelerisque vehicula accumsan.Nulla eget posuere elit.Vestibulum tempus, dolor eu blandit rhoncus, turpis lacus ornare nunc, posuere feugiat orci dui et ex. Duis venenatis ullamcorper massa, quis maximus diam ultrices id. Mauris massa lorem, tristique non libero tincidunt, viverra gravida leo. Quisque ornare auctor vulputate. Phasellus egestas diam eleifend sem posuere, id consequat tortor imperdiet. Vestibulum mauris nisl, pharetra a vulputate nec, ultrices non metus. Fusce sit amet ornare est. Sed fringilla ornare leo, quis sollicitudin lectus fermentum sit amet.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post8 = new Post { PostTitle = "Five best free fonts", PostContent = "Curabitur bibendum neque lectus, at condimentum metus maximus vitae. Sed fringilla ornare leo, quis sollicitudin lectus fermentum sit amet. Nam scelerisque vehicula accumsan. Nulla eget posuere elit.Vestibulum tempus, dolor eu blandit rhoncus, turpis lacus ornare nunc, posuere feugiat orci dui et ex. Duis venenatis ullamcorper massa, quis maximus diam ultrices id. Mauris massa lorem, tristique non libero tincidunt, viverra gravida leo. Quisque ornare auctor vulputate. Phasellus egestas diam eleifend sem posuere, id consequat tortor imperdiet. Vestibulum mauris nisl, pharetra a vulputate nec, ultrices non metus. Fusce sit amet ornare est.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post9 = new Post { PostTitle = "Serif and sans serif", PostContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque elementum consequat ligula at vehicula. In turpis purus, tristique ut ex a, egestas dignissim dolor. Pellentesque ut magna efficitur, porta dolor vitae, scelerisque metus. Phasellus porta, eros quis maximus eleifend, velit ante vestibulum arcu, eget pulvinar dui libero id eros. Pellentesque vitae nisl mollis ex blandit cursus. Vivamus dapibus iaculis odio, ac scelerisque lorem euismod eu. Proin eu nisi porta, consectetur purus sit amet, scelerisque ligula. Donec iaculis mi at dui auctor, at pulvinar nisl laoreet.Suspendisse metus tortor, malesuada ut tempus vel, porta sit amet justo. Nam risus nunc, vehicula vel libero at, suscipit sagittis odio. Sed tincidunt, tellus et efficitur vehicula, nunc nunc sollicitudin urna, dignissim tempor nisl erat luctus sem. Donec bibendum elit est, faucibus ultrices velit maximus nec. Sed a egestas tellus, vel consequat risus. Duis pellentesque ex fermentum congue lacinia. Praesent malesuada porta enim non commodo. Donec vehicula, velit eu luctus tempus, sapien felis ornare urna, sed pulvinar nisl nulla volutpat ante. Phasellus vehicula libero ac nunc dapibus, id rhoncus magna luctus. Mauris luctus lacus sagittis enim ornare, in condimentum ante vulputate. Morbi nec convallis libero.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };
            var post10 = new Post { PostTitle = "Great lost fonts", PostContent = "Many modern typefaces are named after the pioneers of the fifteenth and sixteenth centuries. These typefaces, often referred to as 'humanist', are among the most iconic and, arguably, old fashioned that you see today. However, at the time, printers such as Claude Garamond, Pietro Bembo and Giambattista Palatino were trail-blazers in the advancement of Renaissance printing.Typographic design in this period put form at the front and centre, giving rise to the development of italic typefaces, marrying the curves, angles and proportions of the cursive form in formalised typesets. Over the following centuries, typography has continued to evolve, yielding countless variations, from naturalistic letterforms conforming to human anatomies, to the intentionally abstract models emerging from the avant-garde art movements.The advent of computing has done nothing to halt the rate of typeface development - from the iconic, low resolution print of the 1980s, to the abundance of freely available experimental type of today. Thanks to the internet, text has never been so abundant nor so varied.", CreatedBy = "user@example.com", Approved = true, Keywords = new List<Keyword>() };

            var keyword1 = new Keyword { KeywordText = "Garamond", Approved = true };
            var keyword2 = new Keyword { KeywordText = "Palatino", Approved = true };
            var keyword3 = new Keyword { KeywordText = "Renaissance", Approved = true };
            var keyword4 = new Keyword { KeywordText = "printing", Approved = true };
            var keyword5 = new Keyword { KeywordText = "type", Approved = true };
            var keyword6 = new Keyword { KeywordText = "Caslon", Approved = true };
            var keyword7 = new Keyword { KeywordText = "typography", Approved = true };
            var keyword8 = new Keyword { KeywordText = "Merriweather", Approved = true };
            var keyword9 = new Keyword { KeywordText = "Britain", Approved = true };
            var keyword10 = new Keyword { KeywordText = "Dutch Baroque", Approved = true };
            var keyword11 = new Keyword { KeywordText = "Baskerville", Approved = true };
            var keyword12 = new Keyword { KeywordText = "cursive", Approved = true };
            var keyword13 = new Keyword { KeywordText = "Type Design", Approved = true };
            var keyword14 = new Keyword { KeywordText = "Moveable Type", Approved = true };
            var keyword15 = new Keyword { KeywordText = "typefaces", Approved = true };
            var keyword16 = new Keyword { KeywordText = "printing", Approved = true };
            var keyword17 = new Keyword { KeywordText = "font", Approved = true };
            var keyword18 = new Keyword { KeywordText = "Roboto", Approved = true };
            var keyword19 = new Keyword { KeywordText = "OpenSans", Approved = true };

            post1.Keywords.Add(keyword1);
            post1.Keywords.Add(keyword2);
            post1.Keywords.Add(keyword3);
            post1.Keywords.Add(keyword4);
            post1.Keywords.Add(keyword5);

            post2.Keywords.Add(keyword4);
            post2.Keywords.Add(keyword5);
            post2.Keywords.Add(keyword6);
            post2.Keywords.Add(keyword7);
            post2.Keywords.Add(keyword8);

            post3.Keywords.Add(keyword9);
            post3.Keywords.Add(keyword10);
            post3.Keywords.Add(keyword11);
            post3.Keywords.Add(keyword12);
            post3.Keywords.Add(keyword13);

            post4.Keywords.Add(keyword14);
            post4.Keywords.Add(keyword15);
            post4.Keywords.Add(keyword16);
            post4.Keywords.Add(keyword17);
            post4.Keywords.Add(keyword18);

            post5.Keywords.Add(keyword19);
            post5.Keywords.Add(keyword1);
            post5.Keywords.Add(keyword4);
            post5.Keywords.Add(keyword9);
            post5.Keywords.Add(keyword14);
            post5.Keywords.Add(keyword2);

            post6.Keywords.Add(keyword5);
            post6.Keywords.Add(keyword10);
            post6.Keywords.Add(keyword15);
            post6.Keywords.Add(keyword1);
            post6.Keywords.Add(keyword6);

            post7.Keywords.Add(keyword11);
            post7.Keywords.Add(keyword16);
            post7.Keywords.Add(keyword3);
            post7.Keywords.Add(keyword8);
            post7.Keywords.Add(keyword13);

            post8.Keywords.Add(keyword18);
            post8.Keywords.Add(keyword4);
            post8.Keywords.Add(keyword9);
            post8.Keywords.Add(keyword13);
            post8.Keywords.Add(keyword18);

            post9.Keywords.Add(keyword1);
            post9.Keywords.Add(keyword4);
            post9.Keywords.Add(keyword11);
            post9.Keywords.Add(keyword16);
            post9.Keywords.Add(keyword19);

            post10.Keywords.Add(keyword4);
            post10.Keywords.Add(keyword6);
            post10.Keywords.Add(keyword7);
            post10.Keywords.Add(keyword8);
            post10.Keywords.Add(keyword9);


            context.Posts.Add(
                post1
                );

            context.Posts.Add(
                post2
                );

            context.Posts.Add(
                post3
                );

            context.Posts.Add(
                post4
                );

            context.Posts.Add(
                post5
                );

            context.Posts.Add(
                post6
                );

            context.Posts.Add(
                post7
                );

            context.Posts.Add(
                post8
                );

            context.Posts.Add(
                post9
                );

            context.Posts.Add(
                post10
                );


        }
    }
}