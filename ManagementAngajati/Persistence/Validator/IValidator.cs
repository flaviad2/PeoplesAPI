namespace ManagementAngajati.Persistence.Validator
{
       public interface IValidator<E>
        {
            void Validate(E e);
        }
    
}
