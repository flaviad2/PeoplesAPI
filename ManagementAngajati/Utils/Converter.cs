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
                for (int i = 0; i < angajat.Posturi.Count; i++)
                {
                    posturi.Add(angajat.Posturi[i].ID);
                }
                return new AngajatResponse(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, posturi);

            }
            return null;
        }

        public static PostResponse PostToPostResponse(Post post)
        {
            List<long> angajati = new List<long>();
            for(int i=0; i<post.Angajati.Count; i++)
            {
                angajati.Add(post.Angajati[i].ID); 

            }
            return new PostResponse(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajati);
        }

        public static ConcediuResponse ConcediuToConcediuResponse(Concediu concediu)
        {
            return new ConcediuResponse(concediu.ID, concediu.Angajat.ID, concediu.DataIncepere, concediu.DataTerminare); 

        }

        public static IstoricAngajatResponse IstoricToIstoricResponse(IstoricAngajat istoric)
        {
            return new IstoricAngajatResponse(istoric.ID, istoric.Angajat.ID, istoric.Post.ID, istoric.DataAngajare, istoric.Salariu, istoric.DataReziliere); 

        }


        public static List<AngajatResponse> AngajatListToAngajatResponseList(List<Angajat> angajati)
        {

            List<AngajatResponse> listAnagajatiResponse = new List<AngajatResponse>();
            foreach(Angajat angajat in angajati)
            {
                List<long> posturi = new List<long>(); //posturile acestui angajat 
                for (int i = 0; i < angajat.Posturi.Count; i++)
                {
                    posturi.Add(angajat.Posturi[i].ID);
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
                for(int i=0; i<post.Angajati.Count; i++)
                {
                    angajati.Add(post.Angajati[i].ID); 
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
                listConcediiResponse.Add(new ConcediuResponse(concediu.ID, concediu.Angajat.ID, concediu.DataIncepere, concediu.DataTerminare));
            }
            return listConcediiResponse;
        }

        public static List<IstoricAngajatResponse> IstoricListToIstoricListResponse(List<IstoricAngajat> istoricuri)
        {
            List<IstoricAngajatResponse> listIstoricResponse = new List<IstoricAngajatResponse>(); 
            foreach(IstoricAngajat istoric in istoricuri)
            {
                listIstoricResponse.Add(new IstoricAngajatResponse(istoric.ID, istoric.Angajat.ID, istoric.Post.ID, istoric.DataAngajare, istoric.Salariu, istoric.DataReziliere)); 

            }
            return listIstoricResponse; 
        }
        public static Angajat AngajatW2ToAngajat(AngajatRequestW2 aRW2)
        {
            return new Angajat(aRW2.ID, aRW2.Nume, aRW2.Prenume, aRW2.Username, aRW2.Password, aRW2.DataNasterii, aRW2.Sex, aRW2.Experienta, aRW2.Posturi); 

        }

       public static Post PostW2ToPost(PostRequestW2 pRW2)
        {
            return new Post(pRW2.ID, pRW2.Functie, pRW2.DetaliuFunctie, pRW2.Departament, pRW2.Angajati); 
        }

        public static Concediu ConcediuW2ToConcediu(ConcediuRequestW2 cRW2)
        {
            return new Concediu(cRW2.ID, cRW2.Angajat, cRW2.DataIncepere, cRW2.DataTerminare); 

        }

        public static IstoricAngajat IstoricW2ToIstoric(IstoricAngajatRequestW2 iRW2)
        {
            return new IstoricAngajat(iRW2.ID, iRW2.Angajat, iRW2.Post, iRW2.DataAngajare, iRW2.Salariu, iRW2.DataReziliere); 
        }
        
    }
}
