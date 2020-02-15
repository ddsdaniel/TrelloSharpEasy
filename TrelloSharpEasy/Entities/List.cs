using System.Collections.Generic;
using System.Linq;
using TrelloSharp.ViewModels;

namespace TrelloSharpEasy.Entities
{
    public class List : EntityBase
    {
        public string Name { get; private set; }
        public bool Closed { get; private set; }
        public List<Card> Cards { get; private set; }

        public List(string id, string name, bool closed, List<Card> cards)
            : base(id)
        {
            Name = name;
            Closed = closed;
            Cards = cards;
        }

        public List(
            ListViewModel listViewModel, 
            List<CardViewModel> cardsViewModel, 
            List<MemberViewModel> membersViewModel,
            List<LabelViewModel> labelsViewModel,
            List<CheckListViewModel> checkListsViewModel
            )
            :this(
                 listViewModel.Id, 
                 listViewModel.Name, 
                 listViewModel.Closed,
                 cardsViewModel
                     .FindAll(c => c.IdList == listViewModel.Id)
                     .Select(vm => new Card(vm, membersViewModel, labelsViewModel, checkListsViewModel))
                     .ToList()
                 )
        {

        }
    }
}
