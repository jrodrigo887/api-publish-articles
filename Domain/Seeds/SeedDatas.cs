using ApiBooks.Domain;
using ApiBooks.Enums;
using System.Linq;

namespace ApiBooks.Domai.Seeds
{
    public class SeedDatas
    {
        private UserContext _context;
        
        public SeedDatas(UserContext context)
        {
            _context = context;
        }

        public void seed()
        {
            if(_context.Users.Any() ||
                _context.Articles.Any() ||
                _context.Categories.Any())
            {
                return;
            }

            User us1 = new User("Joao", "joao@gmail.com", "123", "Administrador" );
            User us2 = new User("maria", "maria@gmail.com", "123", "Visitante");
            User us3 = new User("jose", "jose@gmail.com", "123", "Anonimo");
            User us4 = new User("urias", "urias@gmail.com", "123", "Gerente");

            Articles at1 = new Articles("Estrutura", "Coisa qualquer", "iamgem", "Conteudo qualquer");
            Articles at2 = new Articles("Arquitertura", "Coisa qualquer", "iamgem", "Conteudo qualquer");
            Articles at3 = new Articles("Linguagem", "Coisa qualquer", "iamgem", "Conteudo qualquer");
            Articles at4 = new Articles("DevOps", "Coisa qualquer", "iamgem", "Conteudo qualquer");

            Categories ct1 = new Categories("Tecnologias", 0);
            Categories ct2 = new Categories("Internet", 0);
            Categories ct3 = new Categories("Web", 0);
            Categories ct4 = new Categories("Backend", 0);


            _context.Users.AddRange(us1, us2, us3, us4);
            _context.Articles.AddRange(at1, at2, at3, at4);
            _context.Categories.AddRange(ct1, ct2, ct3, ct4);

            _context.SaveChanges();
        }


    }
}
