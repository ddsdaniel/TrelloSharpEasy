using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrelloSharp.Enums;
using TrelloSharp.Services.Api;
using TrelloSharp.ViewModels;
using TrelloSharpEasy.Entities;

namespace TrelloSharpEasy.Services
{
    public class EasyService
    {
        public string AppKey { get; private set; }
        public string UserToken { get; private set; }

        public EasyService(string appKey, string userToken)
        {
            AppKey = appKey;
            UserToken = userToken;
        }

        public List<Board> GetBoards(string organizationId, BoardFilter boardFilter)
        {
            var organizationViewModel = new OrganizationViewModel();
            var organizationApiService = new OrganizationApiService(organizationId, AppKey, UserToken);

            var boardsViewModel = organizationApiService.GetBoards(boardFilter).Result;

            var boards = new List<Board>();

            foreach (var boardViewModel in boardsViewModel)
            {
                Board board = GetBoard(boardViewModel.Id);
                boards.Add(board);
            }

            return boards;
        }

        public Board GetBoard(string boardId)
        {
            var boardApiService = new BoardApiService(boardId, AppKey, UserToken);

            var boardViewModel = boardApiService.GetBoard(boardId).Result;
            var cards = boardApiService.GetCards().Result;
            var lists = boardApiService.GetLists().Result;
            var members = boardApiService.GetMembers().Result;
            var labels = boardApiService.GetLabels().Result;
            var checkLists = boardApiService.GetCheckLists().Result;
            //var customFields = this.getCustomFields(board.id);

            var board = new Board(boardViewModel, cards, lists, members, labels, checkLists);
            return board;
        }
    }
}
