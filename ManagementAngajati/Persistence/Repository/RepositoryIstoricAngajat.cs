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

                return new IstoricAngajat(deSters.ID, deSters.Angajat.ID, deSters.Post.ID, deSters.DataAngajare, deSters.Salariu, deSters.DataReziliere);
            }
            return null; 
        }

        public async Task<List<IstoricAngajat>> FindAll()
        {
            var dbIstoricuri = _context.IstoricuriAngajati.ToList();
            List<IstoricAngajat> res = new List<IstoricAngajat>();
            for(int i=0; i<dbIstoricuri.Count; i++)
            {
                var dbIstoricAngajat = _context.IstoricuriAngajati.Where(a => a.ID == dbIstoricuri[i].ID).Select(c => c.Angajat);
                var dbIstoricPost = _context.IstoricuriAngajati.Where(a => a.ID == dbIstoricuri[i].ID).Select(c => c.Post);
                dbIstoricuri[i].Angajat = dbIstoricAngajat.SingleOrDefault();
                dbIstoricuri[i].Post = dbIstoricPost.SingleOrDefault();
                res.Add(new IstoricAngajat(dbIstoricuri[i].ID, dbIstoricuri[i].Angajat.ID, dbIstoricuri[i].Post.ID, dbIstoricuri[i].DataAngajare, dbIstoricuri[i].Salariu, dbIstoricuri[i].DataReziliere));


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
            }
            return new IstoricAngajat(dbIstoric.ID, dbIstoric.Angajat.ID, dbIstoric.Post.ID, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);

        }

        public async Task<IstoricAngajat> Update(IstoricAngajat entity, long id)
        {
            IstoricAngajatEntity dbIstoric = _context.IstoricuriAngajati.Find(id);
            IstoricAngajat oldIstoric =  new IstoricAngajat(dbIstoric.ID, dbIstoric.Angajat.ID, dbIstoric.Post.ID, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);


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
                return new IstoricAngajat(dbIstoric.ID, dbIstoric.Angajat.ID, dbIstoric.Post.ID, dbIstoric.DataAngajare, dbIstoric.Salariu, dbIstoric.DataReziliere);

            }
            return null;

        }
    }
}
