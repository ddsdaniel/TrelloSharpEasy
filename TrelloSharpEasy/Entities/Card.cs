using System;
using System.Collections.Generic;

namespace TrelloSharpEasy.Entities
{
    public class Card : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ShortUrl { get; private set; }
        public bool Closed { get; private set; }
        public List<Member> Members { get; private set; }
        public List<Label> Labels { get; private set; }
        public List<CheckList> CheckLists { get; private set; }
        public string BoardName { get; private set; }
        public string ListName { get; private set; }
        public List<Action> Actions { get; private set; }
        public List<Attachment> Attachments { get; private set; }
        public string AllLabels => String.Join(' ', Labels
                        .OrderBy(label => label.Name)
                        .Select(label => $"#{label.Name}"));

        public Card(
            string cardId,
            string name,
            string description,
            string shortUrl,
            bool closed,
            List<Member> members,
            List<Label> labels,
            List<CheckList> checkLists,
            string boardName,
            string listName,
            List<Action> actions, 
            List<Attachment> attachments)
            : base(cardId)
        {
            Name = name;
            Description = description;
            ShortUrl = shortUrl;
            Closed = closed;
            Members = members;
            Labels = labels;
            CheckLists = checkLists;
            BoardName = boardName;
            ListName = listName;
            Actions = actions;
            Attachments = attachments;
        }

        public override string ToString() => Name;
    }
}
