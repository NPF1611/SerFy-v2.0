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
        /// cria, caso não existam, as Roles de suporte à aplicação: Veterinario, Funcionario, Dono
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var utilizador = new User();

            var utilizador1 = new User();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Admin'
            if (!roleManager.RoleExists("Administrador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);
            }

            // criar a Role 'Moderador'
            if (!roleManager.RoleExists("V2"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "V2";
                roleManager.Create(role);
            }

            // criar a Role 'Utilizador'
            if (!roleManager.RoleExists("Utilizador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);
            }
          

            // criar um utilizador 'Administrador'
            var user = new ApplicationUser();
            user.UserName = "admin@mail.pt";
            user.Email = "admin@mail.pt";
            string userPWD = "IPT123";
            var chkUser = userManager.Create(user, userPWD);

            //dados do Admin
            utilizador.Name = "Admin";
            utilizador.ID = 300;
            utilizador.UName = user.UserName;
            utilizador.photo = "default_King.jpg";
            utilizador.CRTime = System.DateTime.Now;
            db.Utilizadores.Add(utilizador);



            //Adicionar o Utilizador à respetiva Role-Administrador-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Administrador");
            }
        }

        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97





    }
}
