using System;
using System.Collections.Generic;

namespace TrelloSharpEasy.Entities
{
    public class Organization : EntityBase
    {
        public string Name { get; private set; }
        public List<Board> Boards { get; private set; }

        public Organization(string id, string name, List<Board> boards)
            :base(id)
        {
            Name = name;
            Boards = boards;
        }
    }
}
