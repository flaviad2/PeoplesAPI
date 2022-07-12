using System.Data;
using AutoMapper;
using ManagementAngajati.Utils;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using ManagementAngajati.Models.AngajatModel;
using ManagementAngajati.Models.PostModel;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryAngajat : IRepositoryAngajat
    {

        private readonly ManagementAngajatiContext _context;

        private readonly Mapper _mapper;
        private readonly Mapper _mapper2;



        public RepositoryAngajat(ManagementAngajatiContext context)
        {
            _context = context;

    
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Angajat, AngajatEntity>().ForMember(destination => destination.IdPosturi, options =>
            {
                options.MapFrom(source => source.IdPosturi.Select(id => _context.Posturi.Find(id.ID)).ToList()
            ) ; 
            }
            ));

            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<AngajatEntity, Angajat>().ForMember(destination => destination.IdPosturi, options =>
            {
                options.MapFrom(source => source.IdPosturi.Select(id => new Post { ID = id.ID, Departament = id.Departament, DetaliuFunctie = id.DetaliuFunctie, Functie = id.Functie }).ToList());
            }));


            _mapper2 = new Mapper(config2);
            _mapper = new Mapper(config);

        }



        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context;
        }


        public async Task<Angajat> GetAngajatByUsername(string username)
        {
            var dbAngajat = _context.Angajati.Where(a => a.Username == username).SingleOrDefault() ;
            var dbPosturiAngajat = _context.Angajati.Where(a => a.Username == username).SelectMany(c => c.IdPosturi).ToList();
           

            if (dbPosturiAngajat != null && dbAngajat != null)
            {
                dbAngajat.IdPosturi = dbPosturiAngajat;
                Angajat angajat = _mapper.Map<Angajat>(dbAngajat);
                return angajat;

            }
            return null; 
        }

       

        public async Task<Angajat> Add(Angajat entity)
        {
            var added =  _context.Angajati.Add(_mapper.Map<AngajatEntity>(entity)).Entity;
            _context.SaveChanges();
            entity.ID = added.ID;
            return _mapper2.Map<Angajat>(entity);
            
        }

        public async Task<Angajat> Delete(long id)
        {
            AngajatEntity? deSters = _context.Angajati?.Find(id);

            if (deSters != null)
            {   
                _context.Angajati.Remove(deSters);
                _context.SaveChanges();
                return _mapper.Map<Angajat>(deSters);

            }
            return null;
        }

        public async Task<Angajat> Update(Angajat entity, long id)
        {
             AngajatEntity oldAngajatEntity = _context.Angajati.Find(id);
             
             
            if (oldAngajatEntity != null)
            {
                oldAngajatEntity.Sex = entity.Sex;
                oldAngajatEntity.Username = entity.Username;
                oldAngajatEntity.Password = entity.Password;
                oldAngajatEntity.Nume = entity.Nume;
                oldAngajatEntity.Prenume = entity.Prenume;
                oldAngajatEntity.DataNasterii = entity.DataNasterii;
                oldAngajatEntity.Experienta = entity.Experienta;
               

                _context.Angajati.Update(oldAngajatEntity);
                _context.SaveChanges();

                entity.ID = id;
                return entity;
            }
            
            return null;
        }

        public async Task<Angajat> FindOne(long id)
        {

            var dbAngajat = _context.Angajati.Where(a => a.ID == id).SingleOrDefault();
            var dbPosturiAngajat = _context.Angajati.Where(a => a.ID == id).SelectMany(c => c.IdPosturi).ToList();
            if (dbPosturiAngajat != null && dbAngajat!=null)
            {
                dbAngajat.IdPosturi = dbPosturiAngajat;
                return _mapper2.Map<Angajat>(dbAngajat);

            }
            return null;
        }



        public async Task<List<Angajat>> FindAll()
        {
            
            var dbAngajati = _context.Angajati.ToList();
            for(int i=0; i<dbAngajati.Count; i++)
            {
               
                var dbPosturiAngajat = _context.Angajati.Where(p => p.ID == dbAngajati[i].ID).SelectMany(c => c.IdPosturi).ToList();

                dbAngajati[i].IdPosturi = dbPosturiAngajat;
            }
            List<Angajat> listRes = new List<Angajat>();
            foreach(AngajatEntity aE in dbAngajati)
            {
                listRes.Add(_mapper2.Map<Angajat>(aE));
            }
            return listRes;

        }

       
    }
}
