using TrelloSharp.ViewModels;

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

        public CheckItem(CheckItemViewModel checkItemViewModel)
            : this(checkItemViewModel.Id, checkItemViewModel.Name, checkItemViewModel.State.Equals("complete"))
        {

        }
    }
}