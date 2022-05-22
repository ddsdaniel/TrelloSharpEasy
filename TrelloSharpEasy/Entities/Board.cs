using System;
using System.Collections.Generic;

namespace TrelloSharpEasy.Entities
{
    public class Board : EntityBase
    {
        public string Name { get; private set; }
        public bool Closed { get; private set; }
        public List<List> Lists { get; private set; }

        public Board(string boardId, string name, bool closed, List<List> lists)
            :base(boardId)
        {
            Name = name;
            Closed = closed;
            Lists = lists;
        }

        public override string ToString() => Name;
    }
}
