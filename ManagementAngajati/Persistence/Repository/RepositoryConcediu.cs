using System.Data;
using AutoMapper;
using ManagementAngajati.Models.AngajatModel;
using ManagementAngajati.Models.ConcediuModel;
using ManagementAngajati.Models.PostModel;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Entities;
using ManagementAngajati.Utils;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryConcediu : IRepositoryConcediu
    {

        private readonly ManagementAngajatiContext _context;

        private IMapper _mapper;

        private IMapper _mapper2;

        public RepositoryConcediu(ManagementAngajatiContext context)
        {
            _context = context;


            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Concediu, ConcediuEntity>().ForMember(destination => destination.IdAngajat, options =>
            {
                options.MapFrom(i => new AngajatEntity { ID = i.IdAngajat.ID});
            }));

            _mapper2 = new Mapper(config2);


        }
        public RepositoryConcediu()
        {

        }

        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context;
        }

        public async Task<Concediu> Add(Concediu entity)
        {
            ConcediuEntity concediu = new ConcediuEntity(entity.ID, _context.Angajati.Find(entity.IdAngajat.ID), entity.DataIncepere, entity.DataTerminare);
            var added = _context.Concedii.Add(concediu).Entity;
            _context.SaveChanges();
            entity.ID = added.ID; 
          
            return entity;


        }



        public async Task<Concediu> Delete(long id)
        {
            ConcediuEntity? deSters = _context.Concedii?.Find(id);
            if (deSters != null)
            {
                _context.Concedii.Remove(deSters);
                _context.SaveChanges();
                return _mapper.Map<Concediu>(deSters);

            }
            return null;


        }

        public async Task<List<Concediu>> FindAll()
        {

            var dbConcedii = _context.Concedii.ToList();
            List<Concediu> res = new List<Concediu>();
            for (int i = 0; i < dbConcedii.Count; i++)
            {
                var dbConcediuAngajat = _context.Concedii.Where(a => a.ID == dbConcedii[i].ID).Select(c => c.IdAngajat).SingleOrDefault() ;
                dbConcedii[i].IdAngajat = dbConcediuAngajat;
                var dbPosturiAngajat = _context.Angajati.Where(a => a.ID == dbConcedii[i].IdAngajat.ID).SelectMany(c => c.IdPosturi).ToList();
                dbConcedii[i].IdAngajat.IdPosturi = dbPosturiAngajat;
            }

            List<Concediu> listRes = new List<Concediu>();
            foreach (ConcediuEntity aE in dbConcedii)
            {
             
                AngajatEntity angajatEntity = aE.IdAngajat;
                List<Post> listPosts = new List<Post>();

                foreach (PostEntity pE in angajatEntity.IdPosturi)
                    listPosts.Add(new Post(pE.ID, pE.Functie, pE.DetaliuFunctie, pE.Departament, new List<Angajat>()));
                Angajat angajat = new Angajat(angajatEntity.ID, angajatEntity.Nume, angajatEntity.Prenume, angajatEntity.Username, angajatEntity.Password, angajatEntity.DataNasterii, angajatEntity.Sex, angajatEntity.Experienta, listPosts);
                angajat.ID = aE.IdAngajat.ID;
                listRes.Add(new Concediu(aE.ID, angajat,aE.DataIncepere, aE.DataTerminare));

            }
            return listRes;

        }

        public async Task<Concediu> FindOne(long id)
        {


            var dbConcediu = _context.Concedii.Find(id);
            if (dbConcediu != null)
            {
                var dbAngajat = _context.Concedii.Where(c => c.ID == id).Select(c => c.IdAngajat).SingleOrDefault();
                dbConcediu.IdAngajat = dbAngajat;
                var dbPosturiAngajat = _context.Angajati.Where(a => a.ID == dbAngajat.ID).SelectMany(c => c.IdPosturi).ToList();
                dbConcediu.IdAngajat.IdPosturi = dbPosturiAngajat;
                AngajatEntity angajatEntity = dbConcediu.IdAngajat;
                var dbPosturiA = _context.Angajati.Where(a => a.ID == angajatEntity.ID).SelectMany(c => c.IdPosturi).ToList();
                angajatEntity.IdPosturi = dbPosturiA;

                List<Post> posturiAngajat = new List<Post>(); 
                foreach(PostEntity pE in angajatEntity.IdPosturi)
                {
                    posturiAngajat.Add(new Post(pE.ID, pE.Functie, pE.DetaliuFunctie, pE.Departament, new List<Angajat>()));
                }
                Angajat angajat = new Angajat(angajatEntity.ID, angajatEntity.Nume, angajatEntity.Prenume, angajatEntity.Username, angajatEntity.Password, angajatEntity.DataNasterii, angajatEntity.Sex, angajatEntity.Experienta,posturiAngajat);
                angajat.ID = dbAngajat.ID;
                
                return new Concediu(dbConcediu.ID, angajat, dbConcediu.DataIncepere, dbConcediu.DataTerminare);

               // return _mapper.Map<Concediu>(dbConcediu); 
            }
            return null;



        }

        public async Task<Concediu> Update(Concediu entity, long id)
        {

            ConcediuEntity oldConcediu = _context.Concedii.Find(id);


            if (oldConcediu != null)
            {
                oldConcediu.ID = id;
                oldConcediu.DataIncepere = entity.DataIncepere;
                oldConcediu.DataTerminare = entity.DataTerminare;
                _context.Concedii.Update(oldConcediu);
                _context.SaveChanges();

                entity.ID = id;
                return entity;

            }

            return null;

        }



    }
}