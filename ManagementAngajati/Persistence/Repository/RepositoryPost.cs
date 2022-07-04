using System.Data;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;
namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryPost : IRepositoryPost
    {
        private readonly ManagementAngajatiContext _context; 

        public RepositoryPost(ManagementAngajatiContext context)
        {
            _context = context;
        }

        public RepositoryPost()
        {

        }
        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context;
        }


        public Task<Post> Add(Post entity)
        {
           _context.Posturi.Add(entity);
            _context.SaveChanges();

            Task<Post> pTask = Task.FromResult(entity); 
            return pTask;
        }

        public Task<Post> Delete(long id)
        {
            Post? deSters = _context.Posturi?.Find(id);

            if (deSters != null)
            {
                _context.Posturi.Remove(deSters);
                _context.SaveChanges();

                Task<Post> pTask = Task.FromResult(deSters);
                return pTask; 
            }
            return null; 
        }

        public async Task<List<Post>> FindAll()
        {
            /* var dbPosturi = _context.Posturi.ToList();
             return dbPosturi;  */

            var dbPosturi = _context.Posturi.ToList(); 
            for(int i=0; i<dbPosturi.Count; i++)
            {
                var dbAngajatiPost = _context.Posturi.Where(p => p.ID == dbPosturi[i].ID).SelectMany(c => c.Angajati).ToList();
                dbPosturi[i].Angajati = dbAngajatiPost; 
            }
            return dbPosturi;
        }

        //lista de id-uri ale posturilor -> ids
        public async Task<List<Post>> GetPostsByIds(List<long> ids)
        {
            List<Post> res = new List<Post>(); 

            foreach (long id in ids)
            {
                var dbPost = _context.Posturi.Where(a => a.ID == id).SingleOrDefault();
                res.Add(dbPost);
            }

            return res; 
        }

        public async Task<Post> FindOne(long id)
        {
           
            var dbPost = _context.Posturi.Where(p=> p.ID == id).SingleOrDefault();
            if (dbPost != null)
            {
                var dbAngajatiPost = _context.Posturi.Where(p => p.ID == id).SelectMany(a => a.Angajati).ToList();
                dbPost.Angajati = dbAngajatiPost;
            }
            return dbPost; 

        }

        public Task<Post> Update(Post entity, long id)
        {
            Post oldPost = _context.Posturi.Find(id);
            long idPost = oldPost.ID; 

            if(oldPost != null)
            {
                oldPost.ID = idPost;
                oldPost.DetaliuFunctie = entity.DetaliuFunctie;
                oldPost.Angajati = entity.Angajati;
                oldPost.Functie = entity.Functie;
                oldPost.Departament = entity.Departament;

                Task<Post> pTask = Task.FromResult(oldPost);
                return pTask; 
            }
            return null;
        }
    }

}
