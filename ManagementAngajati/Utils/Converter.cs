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
                List<long> posturi = new List<long>();
                for (int i = 0; i < angajat.IdPosturi.Count; i++)
                {
                    posturi.Add(angajat.IdPosturi[i].ID);
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
            return new PostResponse(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajati);
        }

        public static ConcediuResponse ConcediuToConcediuResponse(Concediu concediu)
        {
            return new ConcediuResponse(concediu.ID, concediu.IdAngajat, concediu.DataIncepere, concediu.DataTerminare); 

        }

        public static IstoricAngajatResponse IstoricToIstoricResponse(IstoricAngajat istoric)
        {
            return new IstoricAngajatResponse(istoric.ID, istoric.IdAngajat, istoric.IdPost, istoric.DataAngajare, istoric.Salariu, istoric.DataReziliere); 

        }


        public static List<AngajatResponse> AngajatListToAngajatResponseList(List<Angajat> angajati)
        {

            List<AngajatResponse> listAnagajatiResponse = new List<AngajatResponse>();
            foreach(Angajat angajat in angajati)
            {
                List<long> posturi = new List<long>(); //posturile acestui angajat 
                for (int i = 0; i < angajat.IdPosturi.Count; i++)
                {
                    posturi.Add(angajat.IdPosturi[i].ID);
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
                listPostResponse.Add(new PostResponse(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajati)); 

            }
            return listPostResponse;

        }

        public static List<ConcediuResponse> ConcediuListToConcediuResponseList(List<Concediu> concedii)
        {
            List<ConcediuResponse> listConcediiResponse = new List<ConcediuResponse>(); 
            foreach(Concediu concediu in concedii)
            {
                listConcediiResponse.Add(new ConcediuResponse(concediu.ID, concediu.IdAngajat, concediu.DataIncepere, concediu.DataTerminare));
            }
            return listConcediiResponse;
        }

        public static List<IstoricAngajatResponse> IstoricListToIstoricListResponse(List<IstoricAngajat> istoricuri)
        {
            List<IstoricAngajatResponse> listIstoricResponse = new List<IstoricAngajatResponse>(); 
            foreach(IstoricAngajat istoric in istoricuri)
            {
                listIstoricResponse.Add(new IstoricAngajatResponse(istoric.ID, istoric.IdAngajat, istoric.IdPost, istoric.DataAngajare, istoric.Salariu, istoric.DataReziliere)); 

            }
            return listIstoricResponse; 
        }
        

       
       
    }
}
