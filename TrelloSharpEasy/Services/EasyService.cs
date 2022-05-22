using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TrelloSharp.Abstractions.Services;
using TrelloSharp.Enums;
using TrelloSharp.Services;
using TrelloSharp.ViewModels;
using TrelloSharpEasy.Entities;

namespace TrelloSharpEasy.Services
{
    public class EasyService : Service
    {
        private readonly ILogger<Service> _logger;
        public string AppKey { get; private set; }
        public string UserToken { get; private set; }

        public EasyService(string appKey, string userToken, ILogger<Service> logger)
        {
            AppKey = appKey;
            UserToken = userToken;
            _logger = logger;
        }

        public List<Board> GetBoards(string organizationId, BoardFilter boardFilter)
        {
            var organizationViewModel = new OrganizationViewModel();
            var organizationApiService = new OrganizationApiService(organizationId, AppKey, UserToken, _logger);

            var boardsViewModel = organizationApiService.GetBoardsAsync(boardFilter).Result;

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
            var boardApiService = new BoardApiService(AppKey, UserToken, _logger);
            var cardApiService = new CardApiService(AppKey, UserToken, _logger);

            var boardViewModel = boardApiService.GetBoard(boardId).Result;
            var listsViewModel = boardApiService.GetLists(boardId).Result;
            var membersViewModel = boardApiService.GetMembers(boardId).Result;
            var labelsViewModel = boardApiService.GetLabelsAsync(boardId).Result;
            var cardsViewModel = boardApiService.GetCardsAsync(boardId).Result;
            var checkLists = boardApiService.GetCheckLists(boardId).Result;

            var listas = new List<List>();

            foreach (var listViewModel in listsViewModel)
            {
                var cardsDaListaViewModel = cardsViewModel.Where(c => c.IdList == listViewModel.Id);
                var cardsDaLista = new List<Card>();

                foreach (var cardViewModel in cardsDaListaViewModel)
                {
                    var attachments = cardApiService
                        .GetAttachments(cardViewModel.Id)
                        .Result
                        .Select(a => new Attachment
                            (
                                id: a.Id,
                                bytes: a.Bytes,
                                date: a.Date,
                                edgeColor: a.EdgeColor,
                                idMember: a.IdMember,
                                isUpload: a.IsUpload,
                                mimeType: a.MimeType,
                                name: a.Name,
                                url: a.Url,
                                pos: a.Pos,
                                fileName: a.FileName
                            ))
                        .ToList();

                    if (cardViewModel.ShortUrl == "https://trello.com/c/2HU6Ln2J")
                    {
                        var x = 0;
                    }

                    var actions = cardApiService
                        .GetActions(cardViewModel.Id)
                        .Result
                        .Select(a => new Action
                            (
                                actionId: a.Id,
                                idMemberCreator: a.idMemberCreator,
                                text: a.data.text,
                                type: a.type,
                                memberCreator: new Member(a.memberCreator.Id, a.memberCreator.FullName, a.memberCreator.Username),
                                date: a.date
                            ))
                        .ToList();

                    var membrosDoCard = membersViewModel
                        .Where(m => cardViewModel.IdMembers.Contains(m.Id))
                        .Select(m => new Member(m.Id, m.FullName, m.Username))
                        .ToList();

                    var labelsDoCard = cardViewModel.Labels
                        .Select(l => new Label(l.Id, l.Name, l.Color))
                        .ToList();

                    var checkListsDoCard = checkLists
                        .Where(c => cardViewModel.IdChecklists.Contains(c.Id))
                        .Select(c =>
                        {
                            var itens = c.CheckItems
                                .Select(item => new CheckItem(item.Id, item.Name, item.State.Equals("complete")))
                                .ToList();

                            return new CheckList(c.Id, c.Name, itens);
                        })
                        .ToList();

                    var card = new Card
                        (
                            cardId: cardViewModel.Id,
                            name: cardViewModel.Name,
                            description: cardViewModel.Desc,
                            shortUrl: cardViewModel.ShortUrl,
                            closed: cardViewModel.Closed,
                            members: membrosDoCard,
                            labels: labelsDoCard,
                            checkLists: null,
                            boardName: boardViewModel.Name,
                            listName: listViewModel.Name,
                            actions: actions,
                            attachments: attachments
                        );

                    cardsDaLista.Add(card);
                }

                var lista = new List(listViewModel.Id, listViewModel.Name, listViewModel.Closed, cardsDaLista);
                listas.Add(lista);
            }


            var board = new Board(boardViewModel.Id, boardViewModel.Name, boardViewModel.Closed, listas);

            return board;
        }
    }
}
