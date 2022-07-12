using ManagementAngajati.Models;
using ManagementAngajati.Persistence; 

namespace ManagementAngajati.Utils
{
    public class Converter
    {

        public Converter() { }

        public static Converter? instance = null;

       
        public static Converter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Converter();
                }
                return instance;
            }
        }

       // public static Angajat AngajatEntityToAngajat (AngajatEntity angajatEntity )

        public static AngajatResponse AngajatToAngajatResponse(Angajat angajat)
        {
            //transformam lista de obiecte in lista de id-uri 
            if (angajat != null)
            {
                List<PostResponse> posturi = new List<PostResponse>();
                for (int i = 0; i < angajat.IdPosturi.Count; i++)
                {
                    Post p = angajat.IdPosturi[i];
                    posturi.Add(new PostResponse(p.ID, p.Functie, p.DetaliuFunctie, p.Departament));
                }
                return new AngajatResponse(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, posturi);

            }
            return null;
        }





        public static PostResponse PostToPostResponse(Post post)
        {
            List<long> angajati = new List<long>();
            for(int i=0; i<post.IdAngajati.Count; i++)
            {
                angajati.Add(post.IdAngajati[i].ID); 

            }
            return new PostResponse(post.ID, post.Functie, post.DetaliuFunctie, post.Departament);
        }

        public static ConcediuResponse ConcediuToConcediuResponse(Concediu concediu)
        {

            return new ConcediuResponse(concediu.ID, AngajatToAngajatResponse(concediu.IdAngajat), concediu.DataIncepere, concediu.DataTerminare); 

        }

        public static IstoricAngajatResponse IstoricToIstoricResponse(IstoricAngajat istoric)
        {
            Angajat angajat = istoric.IdAngajat;
            AngajatNoPostsResponse angajatResponse = new AngajatNoPostsResponse(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, new List<PostResponse>());

            Post postIstoric = istoric.IdPost;
            PostResponse postResponse = new PostResponse(postIstoric.ID, postIstoric.Functie, postIstoric.DetaliuFunctie, postIstoric.Departament);


            return new IstoricAngajatResponse(istoric.ID, angajatResponse, postResponse, istoric.DataAngajare, istoric.Salariu, istoric.DataReziliere); 

        }


        public static List<AngajatResponse> AngajatListToAngajatResponseList(List<Angajat> angajati)
        {

            List<AngajatResponse> listAnagajatiResponse = new List<AngajatResponse>();
            foreach(Angajat angajat in angajati)
            {
                List<PostResponse> posturi = new List<PostResponse>(); //posturile acestui angajat 
                for (int i = 0; i < angajat.IdPosturi.Count; i++)
                {
                    PostResponse postResponse = new PostResponse(angajat.IdPosturi[i].ID, angajat.IdPosturi[i].Functie, angajat.IdPosturi[i].DetaliuFunctie, angajat.IdPosturi[i].Departament);
                    posturi.Add(postResponse);
                }
                listAnagajatiResponse.Add(new AngajatResponse(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, posturi));
            }
            return listAnagajatiResponse;
        
    }

        public static List<PostResponse> PostListToPostResponseList(List<Post> posturi)
        {
            List<PostResponse> listPostResponse = new List<PostResponse>();
            foreach(Post post in posturi)
            {
                List<long> angajati = new List<long>(); 
                for(int i=0; i< post.IdAngajati.Count; i++)
                {
                    angajati.Add(post.IdAngajati[i].ID); 
                }
                listPostResponse.Add(new PostResponse(post.ID, post.Functie, post.DetaliuFunctie, post.Departament)); 

            }
            return listPostResponse;

        }

        public static List<ConcediuResponse> ConcediuListToConcediuResponseList(List<Concediu> concedii)
        {
            List<ConcediuResponse> listConcediiResponse = new List<ConcediuResponse>(); 
            foreach(Concediu concediu in concedii)
            {
                
                listConcediiResponse.Add(new ConcediuResponse(concediu.ID, AngajatToAngajatResponse(concediu.IdAngajat), concediu.DataIncepere, concediu.DataTerminare));
            }
            return listConcediiResponse;
        }

        public static List<IstoricAngajatResponse> IstoricListToIstoricListResponse(List<IstoricAngajat> istoricuri)
        {
            List<IstoricAngajatResponse> listIstoricResponse = new List<IstoricAngajatResponse>(); 
            foreach(IstoricAngajat istoric in istoricuri)
            {
                Angajat angajat = istoric.IdAngajat;
                AngajatNoPostsResponse angajatResponse = new AngajatNoPostsResponse(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, new List<PostResponse>());

                List<PostResponse> listPostResponse = new List<PostResponse>();
                foreach (Post post in angajat.IdPosturi)
                {

                    listPostResponse.Add(new PostResponse(post.ID, post.Functie, post.DetaliuFunctie, post.Departament));

                }

                Post postIstoric = istoric.IdPost;
                PostResponse postResponse = new PostResponse(postIstoric.ID, postIstoric.Functie, postIstoric.DetaliuFunctie, postIstoric.Departament);
                listIstoricResponse.Add(new IstoricAngajatResponse(istoric.ID, angajatResponse, postResponse, istoric.DataAngajare, istoric.Salariu, istoric.DataReziliere));



            }
            return listIstoricResponse; 
        }
        

       
       
    }
}
