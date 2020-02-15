﻿using System.Collections.Generic;
using System.Linq;
using TrelloSharp.ViewModels;

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

        public CheckList(CheckListViewModel checkListViewModel)
            :this(
                 checkListViewModel.Id, 
                 checkListViewModel.Name, 
                 checkListViewModel.CheckItems.Select(vm => new CheckItem(vm)).ToList()
                 )
        {

        }
    }
}
