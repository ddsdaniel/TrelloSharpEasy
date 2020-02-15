using System.Collections.Generic;
using System.Linq;
using TrelloSharp.ViewModels;

namespace TrelloSharpEasy.Entities
{
    public class Card : EntityBase
    {
        public string Name { get; private set; }
        public string ShortUrl { get; private set; }
        public bool Closed { get; private set; }
        public List<Member> Members { get; private set; }
        public List<Label> Labels { get; private set; }
        public List<CheckList> CheckLists { get; private set; }
        //TODO: public List<NumericCustomField> NumericCustomFields { get; private set; }

        public Card(string id, string name, string shortUrl, bool closed, List<Member> members, List<Label> labels, List<CheckList> checkLists)
            : base(id)
        {
            Name = name;
            ShortUrl = shortUrl;
            Closed = closed;
            Members = members;
            Labels = labels;
            CheckLists = checkLists;
        }

        public Card(
            CardViewModel cardViewModel,
            List<MemberViewModel> membersOfBoardViewModel,
            List<LabelViewModel> labelsViewModel,
            List<CheckListViewModel> checkListsViewModel
            )
            : this(
                  cardViewModel.Id,
                  cardViewModel.Name,
                  cardViewModel.ShortUrl,
                  cardViewModel.Closed,
                  
                  membersOfBoardViewModel
                    .FindAll(vm => cardViewModel.IdMembers.Contains(vm.Id))
                    .Select(member => new Member(member)).ToList(),
                  
                  labelsViewModel
                    .FindAll(vm => cardViewModel.IdLabels.Contains(vm.Id))
                    .Select(label => new Label(label)).ToList(),

                  checkListsViewModel
                    .FindAll(vm => cardViewModel.IdChecklists.Contains(vm.Id))
                    .Select(chk => new CheckList(chk)).ToList()

                  )
        {

        }

    }
}
