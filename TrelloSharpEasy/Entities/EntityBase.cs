namespace TrelloSharpEasy.Entities
{
    public abstract class EntityBase
    {
        public string Id { get; private set; }

        protected EntityBase(string id)
        {
            Id = id;
        }

        public void AtualizarId(string id)
        {
            Id = id;
        }
    }
}
