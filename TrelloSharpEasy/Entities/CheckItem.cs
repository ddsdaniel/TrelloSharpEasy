using System;

namespace TrelloSharpEasy.Entities
{
    public class CheckItem : EntityBase
    {
        public string Name { get; private set; }
        public bool Checked { get; private set; }

        public CheckItem(string id, string name, bool isChecked)
            : base(id)
        {
            Name = name;
            Checked = isChecked;
        }

        public override string ToString() => Name;
    }
}