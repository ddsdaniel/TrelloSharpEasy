using System;
using System.Collections.Generic;

namespace TrelloSharpEasy.Entities
{
    public class List : EntityBase
    {
        public string Name { get; private set; }
        public bool Closed { get; private set; }
        public List<Card> Cards { get; private set; }

        public List(
            string listId,
            string name,
            bool closed,
            List<Card> cards)
            :base(listId)
        {
            Name = name;
            Closed = closed;
            Cards = cards;
        }

        public override string ToString() => Name;
    }
}
