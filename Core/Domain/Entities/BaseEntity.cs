namespace Domain.Entities
{
    public class BaseEntity<TKey> // Genaric base entity class with a primary key of type TKey
    {
        public TKey Id { get; set; }

    }
}
