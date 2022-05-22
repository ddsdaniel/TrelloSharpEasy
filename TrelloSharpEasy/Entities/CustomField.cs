using System;

namespace TrelloSharpEasy.Entities
{
    public abstract class CustomField<T> : EntityBase
    {
        public string Name { get; private set; }
        public T Value { get; private set; }

        public CustomField(string id, string name, T value)
            :base(id)
        {
            Name = name;
            Value = value;
        }
    }
}