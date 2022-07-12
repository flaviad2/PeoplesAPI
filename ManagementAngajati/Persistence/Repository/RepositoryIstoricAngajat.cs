using ManagementAngajati.Models;
using AutoMapper;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Entities;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryIstoricAngajat : IRepositoryIstoricAngajat
    {
        private readonly ManagementAngajatiContext _context;
        private readonly IMapper _mapper;
        
        public RepositoryIstoricAngajat(ManagementAngajatiContext context)
        {
            _context = context;
        }

        public RepositoryIstoricAngajat()
        {

        }
        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context;
        }

        
        public async Task<IstoricAngajat> Add(IstoricAngajat entity)
        {
            IstoricAngajatEntity istoric = new IstoricAngajatEntity(entity.ID, _context.Angajati.Find(entity.IdAngajat), _context.Posturi.Find(entity.IdPost), entity.DataAngajare, entity.Salariu, entity.DataReziliere);
            var added = _context.IstoricuriAngajati.Add(istoric).Entity;
            _context.SaveChanges();
            entity.ID = added.ID;
            return entity;

            
        }

        public async Task<IstoricAngajat> Delete(long id)
        {

            IstoricAngajatEntity? deSters = _context.IstoricuriAngajati?.Find(id);

            if (deSters != null)
            {
                _context.IstoricuriAngajati.Remove(deSters);
                _context.SaveChanges();

                AngajatEntity angajatE = _context.Angajati.Find(deSters.Angajat.ID);
                Angajat angajat = new Angajat(angajatE.ID, angajatE.Nume, angajatE.Prenume, angajatE.Username, angajatE.Password, angajatE.DataNasterii, angajatE.Sex, angajatE.Experienta, new List<Post>());
                angajat.ID = deSters.ID;
                PostEntity postE = _context.Posturi.Find(deSters.Post.ID);
                Post post = new Post(postE.ID, postE.Functie, postE.DetaliuFunctie, postE.Departament, new List<Angajat>());
                IstoricAngajat istoric = new IstoricAngajat(deSters.ID, angajat, post, deSters.DataAngajare, deSters.Salariu, deSters.DataReziliere);
                return istoric;


                // return new IstoricAngajat(deSters.ID, deSters.Angajat.ID, deSters.Post.ID, deSters.DataAngajare, deSters.Salariu, deSters.DataReziliere);
            }
            return null; 
        }

        public async Task<List<IstoricAngajat>> FindAll()
        {
            var dbIstoricuri = _context.IstoricuriAngajati.ToList();
            List<IstoricAngajat> res = new List<IstoricAngajat>();
            for(int i=0; i<dbIstoricuri.Count; i++)
            {
                var dbIstoricAngajat = _context.IstoricuriAngajati.Where(a => a.ID == dbIstoricuri[i].ID).Select(c => c.Angajat).SingleOrDefault();
                var dbIstoricPost = _context.IstoricuriAngajati.Where(a => a.ID == dbIstoricuri[i].ID).Select(c => c.Post).SingleOrDefault();
                dbIstoricuri[i].Angajat = dbIstoricAngajat;
                dbIstoricuri[i].Post = dbIstoricPost;

                List<Post> posts = new List<Post>();
                foreach (PostEntity pE in dbIstoricAngajat.IdPosturi)
                    posts.Add(new Post(pE.ID, pE.Functie, pE.DetaliuFunctie, pE.Departament, new List<Angajat>()));
                Angajat a = new Angajat(dbIstoricuri[i].Angajat.ID, dbIstoricAngajat.Nume, dbIstoricAngajat.Prenume, dbIstoricAngajat.Username, dbIstoricAngajat.Password, dbIstoricAngajat.DataNasterii, dbIstoricAngajat.Sex, dbIstoricAngajat.Experienta, posts);
                a.ID = dbIstoricuri[i].Angajat.ID;
               Post p = new Post(dbIstoricPost.ID, dbIstoricPost.Functie, dbIstoricPost.DetaliuFunctie, dbIstoricPost.Departament,new List<Angajat>()); 


                res.Add(new IstoricAngajat(dbIstoricuri[i].ID,a , p, dbIstoricuri[i].DataAngajare, dbIstoricuri[i].Salariu, dbIstoricuri[i].DataReziliere));


            }
            return res;
            
        }

        public async Task<IstoricAngajat> FindOne(long id)
        {


            var dbIstoric = _context.IstoricuriAngajati.Where(i => i.ID == id).SingleOrDefault();
            if (dbIstoric != null)
            {
                var dbAngajat = _context.IstoricuriAngajati.Where(c => c.ID == id).Select(c => c.Angajat).SingleOrDefault();
                var dbPost = _context.IstoricuriAngajati.Where(c => c.ID == id).Select(c => c.Post).SingleOrDefault();
                dbIstoric.Angajat = dbAngajat;
                dbIstoric.Post = dbPost;



                AngajatEntity angajatE = _context.Angajati.Find(dbIstoric.Angajat.ID);
                Angajat angajat = new Angajat(angajatE.ID, angajatE.Nume, angajatE.Prenume, angajatE.Username, angajatE.Password, angajatE.DataNasterii, angajatE.Sex, angajatE.Experienta, new List<Post>());
                angajat.ID = dbAngajat.ID;
                PostEntity postE = _context.Posturi.Find(dbIstoric.Post.ID);
                Post post = new Post(postE.ID, postE.Functie, postE.DetaliuFunctie, postE.Departament, new List<Angajat>());
                IstoricAngajat istoric = new IstoricAngajat(dbIstoric.ID, angajat, post, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);
                return istoric;

            }
            return null;
         //   return new IstoricAngajat(dbIstoric.ID, dbIstoric.Angajat.ID, dbIstoric.Post.ID, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);

        }

        public async Task<IstoricAngajat> Update(IstoricAngajat entity, long id)
        {
            IstoricAngajatEntity dbIstoric = _context.IstoricuriAngajati.Find(id);
            AngajatEntity angajatE = _context.Angajati.Find(entity.IdAngajat.ID);
            Angajat angajat = new Angajat(angajatE.ID, angajatE.Nume, angajatE.Prenume, angajatE.Username, angajatE.Password, angajatE.DataNasterii, angajatE.Sex, angajatE.Experienta, new List<Post>());
            angajat.ID = dbIstoric.Angajat.ID;
            PostEntity postE = _context.Posturi.Find(entity.IdPost.ID);
            Post post = new Post(postE.ID, postE.Functie, postE.DetaliuFunctie, postE.Departament, new List<Angajat>()); 
            IstoricAngajat oldIstoric =  new IstoricAngajat(dbIstoric.ID, angajat, post, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);

            if(oldIstoric != null)
            {
                oldIstoric.ID = id;
                oldIstoric.Salariu = entity.Salariu;
                oldIstoric.IdAngajat = entity.IdAngajat;
                oldIstoric.DataReziliere = entity.DataReziliere;
                oldIstoric.DataAngajare = entity.DataAngajare;
               
                _context.Update(oldIstoric);
                _context.SaveChanges();
                return oldIstoric;
            }

            return null; 
        }

        public async Task<IstoricAngajat> FindOneByIdAngajat(long id)
        {
            var dbIstoric = _context.IstoricuriAngajati.Where(i => i.Angajat.ID == id).SingleOrDefault();
            if (dbIstoric != null)
            {
                var dbAngajat = _context.IstoricuriAngajati.Where(i => i.Angajat.ID == id).Select(c => c.Angajat).SingleOrDefault();
                var dbPost = _context.IstoricuriAngajati.Where(i => i.Angajat.ID == id).Select(c => c.Post).SingleOrDefault();
                dbIstoric.Angajat = dbAngajat;
                dbIstoric.Post = dbPost;

                AngajatEntity angajatE = _context.Angajati.Find(dbIstoric.Angajat.ID);
                Angajat angajat = new Angajat(angajatE.ID, angajatE.Nume, angajatE.Prenume, angajatE.Username, angajatE.Password, angajatE.DataNasterii, angajatE.Sex, angajatE.Experienta, new List<Post>());
                angajat.ID = dbAngajat.ID;
                PostEntity postE = _context.Posturi.Find(dbIstoric.Post.ID);
                Post post = new Post(postE.ID, postE.Functie, postE.DetaliuFunctie, postE.Departament, new List<Angajat>());
                IstoricAngajat istoric = new IstoricAngajat(dbIstoric.ID, angajat, post, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);
                return istoric;

               //  return new IstoricAngajat(dbIstoric.ID, dbIstoric.Angajat.ID, dbIstoric.Post.ID, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);

            }
            return null;

        }
    }
}
