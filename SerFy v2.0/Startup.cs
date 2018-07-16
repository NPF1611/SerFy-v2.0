using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SerFy_v2._0.Models;

[assembly: OwinStartupAttribute(typeof(SerFy_v2._0.Startup))]
namespace SerFy_v2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            iniciaAplicacao();
        }

        /// <summary>
        /// creates the app roles 
        /// creates the initial users 
        /// </summary>
        private void iniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var utilizador = new User();

            var utilizador1 = new User();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // create the Role 'Admin'
            if (!roleManager.RoleExists("Administrador"))
            {
                // if the role doesnt exist
                // creates the role
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);
            }

            //creates the role moderator (Version2)
            if (!roleManager.RoleExists("Version2"))
            {
                // if the role doesnt exist
                // creates the role
                var role = new IdentityRole();
                role.Name = "Version2";
                roleManager.Create(role);
            }

            // creates the Role normal User (Utilizador)
            if (!roleManager.RoleExists("Utilizador"))
            {
                // if the role doesnt exist
                // creates the role
                var role = new IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);
            }
          //.----------------------------------Users creation---------------------------------.

            //---------- creates the 'Administrador'
            var user = new ApplicationUser();
            user.UserName = "admin@gmail.pt";
            user.Email = "admin@gmail.pt";
            string userPWD = "Ipt123.";
            var chkUser = userManager.Create(user, userPWD);

            //Admin info
            utilizador.Name ="Admin";
            utilizador.ID = 300;
            utilizador.UName = "Admin";
            utilizador.email = user.Email;
            utilizador.photo = "default.png";
            utilizador.CRTime = System.DateTime.Now;
            db.Utilizadores.Add(utilizador);

            //---------- creates an regular user'utilizador'
            var user2 = new ApplicationUser();
            user2.UserName = "user@gmail.pt";
            user2.Email = "user@gmail.pt";
            string userPWD2 = "Ipt123.";
            var chkUser2 = userManager.Create(user2, userPWD2);

            //user info
            utilizador1.Name = "User";
            utilizador1.ID = 300;
            utilizador1.UName = "User";
            utilizador1.email = user2.Email;
            utilizador1.photo = "default.png";
            utilizador1.CRTime = System.DateTime.Now;
            db.Utilizadores.Add(utilizador1);



            //Add the role to the user
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Administrador");
            }
            if (chkUser2.Succeeded)
            {
                var result2 = userManager.AddToRole(user2.Id, "Version2");
            }
        }

        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97





    }
}
