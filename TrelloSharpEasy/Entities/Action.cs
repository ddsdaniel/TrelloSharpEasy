using System;

namespace TrelloSharpEasy.Entities
{
    public class Action : EntityBase
    {
        public string IdMemberCreator { get; private set; }
        public string Text { get; private set; }
        public string Type { get; private set; }
        public Member MemberCreator { get; private set; }
        public DateTime Date { get; private set; }

        public Action(string actionId, string idMemberCreator, string text, string type, Member memberCreator, DateTime date)
            : base(actionId)
        {
            IdMemberCreator = idMemberCreator;
            Text = text;
            Type = type;
            MemberCreator = memberCreator;
            Date = date;
        }

        public override string ToString() => $"{Type}: {Text}";
    }
}
