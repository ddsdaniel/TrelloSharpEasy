using System.Collections.Generic;

namespace TrelloSharpEasy.Entities
{
    public class CheckList : EntityBase
    {
        public string Name { get; private set; }
        public List<CheckItem> Items { get; private set; }

        public CheckList(string id, string name, List<CheckItem> items)
            :base(id)
        {
            Name = name;
            Items = items;
        }

        public override string ToString() => Name;
    }
}
