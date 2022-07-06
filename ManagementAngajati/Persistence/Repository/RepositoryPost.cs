using System.Data;
using AutoMapper;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Entities;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryPost : IRepositoryPost
    {
        private readonly ManagementAngajatiContext _context; 

        private readonly IMapper _mapper;
        //mapeaza de la Entity la Object 


        private readonly IMapper _mapper2; 
        //mapeaza de la Object la Entity

        public RepositoryPost(ManagementAngajatiContext context)
        {
            _context = context;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<PostEntity, Post>().ForMember(destination => destination.IdAngajati, options =>
            {
                options.MapFrom(source => source.Angajati.Select(a => a.ID).ToList());
            }));

            _mapper = new Mapper(config);



            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostEntity>().ForMember(destination => destination.Angajati, options =>
            {
                options.MapFrom(source => source.IdAngajati.Select(a => a.ID).ToList());
            }));

            _mapper2 = new Mapper(config2);

        }

        public RepositoryPost()
        {

        }
        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context;
        }


        public async Task<Post> Add(Post entity)
        {
            PostEntity postEntity = _mapper2.Map<PostEntity>(entity);
            var added = _context.Posturi.Add(postEntity).Entity;
            _context.SaveChanges();
            entity.ID = added.ID;
            return entity;
           
        }

        public async Task<Post> Delete(long id)
        {
            PostEntity? deSters = _context.Posturi?.Find(id);

            if (deSters != null)
            {
                _context.Posturi.Remove(deSters);
                _context.SaveChanges();

                return _mapper.Map<Post>(deSters); 
            }
            return null; 
        }

        public async Task<List<Post>> FindAll()
        {

            var dbPosturi = _context.Posturi.ToList();
            List<Post> posts = new List<Post>();


            for (int i=0; i<dbPosturi.Count; i++)
            {
                var dbAngajatiPost = _context.Posturi.Where(p => p.ID == dbPosturi[i].ID).SelectMany(c => c.Angajati).ToList();
                dbPosturi[i].Angajati = dbAngajatiPost;
                posts.Add(_mapper.Map<Post>(dbPosturi[i])); 
                
            }
            return posts; 
            
        }

        
        public async Task<List<Post>> GetPostsByIds(List<long> ids)
        {
            List<Post> res = new List<Post>(); 

            foreach (long id in ids)
            {
                var dbPost = _context.Posturi.Where(a => a.ID == id).SingleOrDefault();
                res.Add(_mapper.Map<Post>(dbPost));
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
                return _mapper.Map<Post>(dbPost);

            }
            return null; 

        }

        public async Task<Post> Update(Post entity, long id)
        {
            PostEntity oldPostEntity = _context.Posturi.Find(id);
           

            if(oldPostEntity != null)
            {
                oldPostEntity.DetaliuFunctie = entity.DetaliuFunctie;
                oldPostEntity.Angajati.Clear();
               // foreach (AngajatEntity angajat in entity.Angajati)
               //     oldPostEntity.Angajati.Add(angajat);
                oldPostEntity.Functie = entity.Functie;
                oldPostEntity.Departament = entity.Departament;
                _context.Posturi.Update(oldPostEntity);
                _context.SaveChanges();

                return _mapper.Map<Post>(oldPostEntity);

            }
            return null;
        }
    }

}
