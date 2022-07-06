using System.Data;
using AutoMapper;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Entities;
using ManagementAngajati.Utils;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryConcediu : IRepositoryConcediu
    {

        private readonly ManagementAngajatiContext _context;

        private IMapper _mapper;
        //mapeaza de la Entity la Object 

        private IMapper _mapper2;
        //mapeaza de la Object la Entity 

        public RepositoryConcediu(ManagementAngajatiContext context)
        {
            _context = context;

            //din ConcediuEntity in Concediu
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ConcediuEntity, Concediu>().ForMember(destination => destination.IdAngajat, options =>
            {
                options.MapFrom(source => source.IdAngajat.ID);
              
            }));
            _mapper = new Mapper(config);



       

       /* var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Concediu, ConcediuEntity>().ForMember(destination => destination.IdAngajat.ID, options =>
             {
                 options.MapFrom(source => source.IdAngajat);
             }));


            CreateMap<Owner, Car>().ForMember(dest => dest.OwnerData,
         input => input.MapFrom(i => new Owner { Name = i.Name }));

            */

          /*  var config3 = new MapperConfiguration(cfg => cfg.CreateMap<Concediu, ConcediuEntity>().ForMember(destination => destination.IdAngajat, options =>
            {
                options.MapFrom(i => i.IdAngajat);
            }));

            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Concediu, ConcediuEntity>().ForPath(a => a.IdAngajat, o => o.MapFrom(p => p.IdAngajat)));
          */


            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Concediu, ConcediuEntity>().ForMember(destination => destination.IdAngajat, options =>
            {
                options.MapFrom(i => new AngajatEntity { ID = i.IdAngajat});
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
            ConcediuEntity concediu = new ConcediuEntity(entity.ID, _context.Angajati.Find(entity.IdAngajat), entity.DataIncepere, entity.DataTerminare);
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
            }

            List<Concediu> listRes = new List<Concediu>();
            foreach (ConcediuEntity aE in dbConcedii)
            {
                var x = aE;
                listRes.Add(new Concediu(aE.ID, aE.IdAngajat.ID, aE.DataIncepere, aE.DataTerminare));
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
                return new Concediu(dbConcediu.ID, dbConcediu.IdAngajat.ID, dbConcediu.DataIncepere, dbConcediu.DataTerminare); 

            }
            return null;



        }

        public async Task<Concediu> Update(Concediu entity, long id)
        {

            ConcediuEntity oldConcediu = await _context.Concedii.FindAsync(id);


            if (oldConcediu != null)
            {
                oldConcediu.ID = id;
                oldConcediu.DataIncepere = entity.DataIncepere;
                oldConcediu.DataTerminare = entity.DataTerminare;

                _context.Concedii.Update(oldConcediu);
                _context.SaveChanges();


                return new Concediu(oldConcediu.ID, oldConcediu.IdAngajat.ID, oldConcediu.DataIncepere, oldConcediu.DataTerminare); 

            }

            return null;

        }



    }
}