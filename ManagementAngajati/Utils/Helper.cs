using ManagementAngajati.Models.AngajatModel;
using ManagementAngajati.Models.PostModel;
using ManagementAngajati.Persistence.Entities;

namespace ManagementAngajati.Utils
{
    public class Helper
    {

        public Helper() { }

        public static Helper? instance = null;

       
        public static Helper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Helper();
                }
                return instance;
            }
        }

        public static bool IsEqualListOfIds(List<long>ids1, List<long>ids2)
        {
            if(ids1.Count != ids2.Count)
                return false; 
            foreach(long id in ids1)
            {
                if (!ids2.Contains(id))
                    return false;
            }
            return true; 
        }

         public static bool IsEqualListOfPosts(List<Post>posts1, List<Post>posts2)
        {
            if(posts1.Count != posts2.Count)
                return false; 
            foreach(Post post in posts1)
            {
                if (!posts2.Contains(post))
                    return false;
            }
            return true; 
        }

        public static bool IsEqualListOfAngajati(List<Angajat>angajati1, List<Angajat>angajati2)
        {
            if (angajati1.Count != angajati2.Count)
                return false; 
            foreach(Angajat angajat in angajati1)
            {
                if (!angajati2.Contains(angajat))
                    return false; 
            }
            return true;
        }
    }
}
