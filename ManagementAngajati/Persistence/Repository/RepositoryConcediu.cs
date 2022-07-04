using System.Data;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryConcediu : IRepositoryConcediu
    {

        private readonly ManagementAngajatiContext _context; 

        public RepositoryConcediu(ManagementAngajatiContext context)
        {
            _context= context;
        }
        public RepositoryConcediu()
        {

        }

        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context; 
        }
       
        public Task<Concediu> Add(Concediu entity)
        {
            _context.Concedii.Add(entity);
            _context.SaveChanges();

            Task<Concediu> cTask = Task.FromResult(entity);
            return cTask; 
        }



        public Task<Concediu> Delete(long id)
        {
            Concediu? deSters = _context.Concedii?.Find(id);
            if(deSters!=null)
            {
                _context.Concedii.Remove(deSters);
                _context.SaveChanges();

                Task<Concediu> cTask = Task.FromResult(deSters);
                return cTask;
            }
            return null;

           
        }

        public async Task<List<Concediu>> FindAll()
        {
            

            var dbConcedii = _context.Concedii.ToList();
            for (int i = 0; i < dbConcedii.Count; i++)
            {
                var dbConcediuAngajat = _context.Concedii.Where(a => a.ID == dbConcedii[i].ID).Select(c => c.Angajat);
                dbConcedii[i].Angajat = dbConcediuAngajat.SingleOrDefault();
            }
            return dbConcedii;
        }

        public async Task<Concediu> FindOne(long id)
        {
            
           
            var dbConcediu = _context.Concedii.Find(id);
            if (dbConcediu != null)
            {
                var dbAngajat = _context.Concedii.Where(c => c.ID == id).Select(c => c.Angajat).SingleOrDefault();
                dbConcediu.Angajat = dbAngajat;
            }
            return dbConcediu; 


            
        }

        public Task<Concediu> Update(Concediu entity, long id)
        {
            Concediu oldConcediu = _context.Concedii.Find(id);
            long idConcediu = oldConcediu.ID; 

            if(oldConcediu != null)
            {
                oldConcediu.ID = idConcediu;
                oldConcediu.Angajat = entity.Angajat; 
                oldConcediu.DataIncepere = entity.DataIncepere;
                oldConcediu.DataTerminare = entity.DataTerminare;

                _context.Concedii.Update(oldConcediu);
                _context.SaveChanges();

                Task<Concediu> cTask = Task.FromResult(oldConcediu);
                return cTask; 

            }

            return null;

        }
    }
}