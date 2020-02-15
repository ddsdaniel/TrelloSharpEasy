using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloSharpEasy.Entities
{
    public abstract class EntityBase
    {
        public string Id { get; private set; }

        protected EntityBase(string id)
        {
            Id = id;
        }

    }
}
