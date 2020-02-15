using TrelloSharp.ViewModels;

namespace TrelloSharpEasy.Entities
{
    public class Label : EntityBase
    {
        public string Name { get; private set; }
        public string Color { get; private set; }

        public Label(string id, string name, string color)
            : base(id)
        {
            Name = name;
            Color = color;
        }

        public Label(LabelViewModel labelViewModel)
            : this(labelViewModel.Id, labelViewModel.Name, labelViewModel.Color)
        {

        }
    }
}
