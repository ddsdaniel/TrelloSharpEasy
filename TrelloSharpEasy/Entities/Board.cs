using System.Collections.Generic;
using System.Linq;
using TrelloSharp.ViewModels;

namespace TrelloSharpEasy.Entities
{
    public class Board : EntityBase
    {
        public string Name { get; private set; }
        public bool Closed { get; private set; }
        public List<List> Lists { get; private set; }
        public List<Member> Members { get; private set; }
        public List<Label> Labels { get; private set; }

        public Board(string id, string name, bool closed, List<List> lists, List<Member> members, List<Label> labels) 
            : base(id)
        {
            Name = name;
            Closed = closed;
            Lists = lists;
            Members = members;
            Labels = labels;
        }

        public Board(
            BoardViewModel boardViewModel,
            List<CardViewModel> cardsViewModel,
            List<ListViewModel> listsViewModel,
            List<MemberViewModel> membersViewModel,
            List<LabelViewModel> labelsViewModel,
            List<CheckListViewModel> checkListsViewModel
            )
            : this(
                 boardViewModel.Id,
                 boardViewModel.Name,
                 boardViewModel.Closed,
                 listsViewModel.Select(vm => new List(vm, cardsViewModel, membersViewModel, labelsViewModel, checkListsViewModel)).ToList(),
                 membersViewModel.Select(vm => new Member(vm)).ToList(),
                 labelsViewModel.Select(vm => new Label(vm)).ToList()
                 )
        {
        }
    }
}
