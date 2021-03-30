using DamaWeb.GameFolder;
using DamaWeb.Hubs;
using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Model.Models;
using Model.UIGame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        PlayGameRepository repository;
        IHubContext<GameHub> hub;

        public GameController( IHubContext<GameHub> _hub)
        {
            repository = new PlayGameRepository();
            hub = _hub;
        }

        [HttpPost]
        public JsonResult StartGame(int id)
        {
            var (pg,b) = repository.GetByColumNameFist("GameId", id);
            if (!b) return new JsonResult("error") { StatusCode = (int)HttpStatusCode.InternalServerError };
            return new JsonResult(pg);
        }

        [HttpPost]
        public string PossiblePlace(int x, int y, int z, int gameId)
        {
            var pg = repository.GetByColumNameFist("GameId", gameId).Item1;
            UICoordinate uICoordinate = new UICoordinate();
            UIPlayGame uIPlaygame = new SrzJson().desrz(pg);

            //uIPlaygame.BlackCoordinate.any(c=>c.X==x&&c.Y==y&&c.Z==z)
            if (pg.Gamer1 == pg.Queue&& uIPlaygame.WhiteCoordinate.Any(c => c.X == x && c.Y == y && c.Z == z))
            {
                uICoordinate = new PossiblePlace(
                    uIPlaygame,
                    Convert.ToByte(x),
                    Convert.ToByte(y),
                    Convert.ToByte(z)).
                    White();
            }
            else if (pg.Gamer2 == pg.Queue&& uIPlaygame.BlackCoordinate.Any(c => c.X == x && c.Y == y && c.Z == z))
            {
                uICoordinate = new PossiblePlace(
                    uIPlaygame,
                    Convert.ToByte(x),
                    Convert.ToByte(y),
                    Convert.ToByte(z)).
                    BLack();
            }
            return JsonConvert.SerializeObject(uICoordinate);
        }

        [HttpPost]
        public void Move(int gameID, int oldX, int oldY, int oldZ, int newX, int newY)
        {
            var pg = repository.GetByColumNameFist("GameId", gameID).Item1;
            var uiGame = new SrzJson().desrz(pg);
            var moveItem = new MoveItem(
                uiGame,
                Convert.ToByte(oldX),
                Convert.ToByte(oldY),
                Convert.ToByte(oldZ),
                Convert.ToByte(newX),
                Convert.ToByte(newY)
                );

            uiGame.Move.DumX = 0; uiGame.Move.DumY = 0;
            if (uiGame.Gamer1 == uiGame.Queue&& uiGame.WhiteCoordinate.Any(c => c.X == oldX && c.Y == oldY && c.Z == oldZ))
            {
                uiGame = moveItem.MoveWhite();
                uiGame.Queue = uiGame.Gamer2;
            }
            else if (uiGame.Gamer2 == uiGame.Queue&& uiGame.BlackCoordinate.Any(c => c.X == oldX && c.Y == oldY && c.Z == oldZ))
            {
                uiGame = moveItem.MoveBlack();
                uiGame.Queue = uiGame.Gamer1;
            }
            var mm=new SrzJson().srz(uiGame);
            if (!new PlayGameRepository().Update(mm, pg.Id)) return ;

            hub.Clients.Group(gameID.ToString()).SendAsync("movedstone", JsonConvert.SerializeObject(mm));

            //return JsonConvert.SerializeObject(uiGame.Move);
        }


        [HttpPost]
        public void DumMove(int gameID, int oldX, int oldY, int oldZ, int newX, int newY)
        {
            var pg = repository.GetByColumNameFist("GameId", gameID).Item1;
            var uiGame = new SrzJson().desrz(pg);
            var moveItem = new MoveItem(
                uiGame,
                Convert.ToByte(oldX),
                Convert.ToByte(oldY),
                Convert.ToByte(oldZ),
                Convert.ToByte(newX),
                Convert.ToByte(newY));



            if (uiGame.Gamer1 == uiGame.Queue) uiGame = moveItem.DumWhite();
            else if (uiGame.Gamer2 == uiGame.Queue) uiGame = moveItem.DumBlack();
            var mm = new SrzJson().srz(uiGame);
            if (!repository.Update(mm, pg.Id)) return;

            hub.Clients.Group(gameID.ToString()).SendAsync("movedstone", CkeckGame(mm));
            //return JsonConvert.SerializeObject(uiGame.Move);
        }

        
        public string CkeckGame(PlayGame pg)
        {
            //var pg = repository.GetByColumNameFist("GameId", gameId).Item1;
            //any stone win
            if (!pg.BlackCoordinate.Contains("X") || !pg.WhiteCoordinate.Contains("X"))
            {
                var gamesRepostory = new GamesRepository();
                var g = gamesRepostory.GetByColumNameFist("Id", pg.GameId).Item1;
                g.Status = GameStatus.Close;
                if (!pg.WhiteCoordinate.Contains("X")) g.WinUser = g.AcceptUser;
                if (!pg.BlackCoordinate.Contains("X")) g.WinUser = g.RequestUser;
                var b = gamesRepostory.Update(g, g.Id);
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                return  "GameStatuse:Win User" +User.Identity.Name;
            }
            return JsonConvert.SerializeObject(pg);
        }

    }

}
