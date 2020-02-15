using TrelloSharp.ViewModels;

namespace TrelloSharpEasy.Entities
{
    public class Member : EntityBase
    {
        public string FullName { get; private set; }
        public string UserName { get; private set; }

        public Member(string id, string fullName, string userName)
            :base(id)
        {
            FullName = fullName;
            UserName = userName;
        }

        public Member(MemberViewModel vm)
            :this(vm.Id, vm.FullName, vm.Username)
        {

        }
    }
}
