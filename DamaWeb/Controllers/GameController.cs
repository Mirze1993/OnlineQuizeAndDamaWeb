using DamaWeb.GameFolder;
using DamaWeb.Repostory;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    public class GameController : Controller
    {
        PlayGameRepository repository;
        public GameController()
        {
            repository = new PlayGameRepository();
        }
        public IActionResult Start(int id)
        {
            ViewBag.GameId = id;
            return View();
        }

        [HttpPost]
        public JsonResult StartGame(int id)
        {
            var (pg,b) = repository.GetByColumNameFist("GameId", id);
            return new  JsonResult(pg);
        }

        [HttpPost]
        public JsonResult PossiblePlace(int x, int y, int z, int gameId)
        {
            var pg = repository.GetByColumNameFist("GameId", gameId).Item1;
            UICoordinate uICoordinate = new UICoordinate();
            UIPlayGame uIPlaygame = new SrzJson().desrz(pg);

            if (pg.Gamer1 == pg.Queue)
            {
                uICoordinate = new PossiblePlace(
                    uIPlaygame,
                    Convert.ToByte(x),
                    Convert.ToByte(y),
                    Convert.ToByte(z)).
                    White();
            }

            else if (pg.Gamer2 == pg.Queue)
            {
                uICoordinate = new PossiblePlace(
                    uIPlaygame,
                    Convert.ToByte(x),
                    Convert.ToByte(y),
                    Convert.ToByte(z)).
                    BLack();
            }
            return new JsonResult(uICoordinate);
        }

        [HttpPost]
        public JsonResult Move(int gameID, int oldX, int oldY, int oldZ, int newX, int newY)
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
            if (uiGame.Gamer1 == uiGame.Queue)
            {
                uiGame = moveItem.MoveWhite();
                uiGame.Queue = uiGame.Gamer2;
            }
            else if (uiGame.Gamer2 == uiGame.Queue)
            {
                uiGame = moveItem.MoveBlack();
                uiGame.Queue = uiGame.Gamer1;
            }
            if (!repository.Update(new SrzJson().srz(uiGame), pg.Id)) 
                return new JsonResult(default(Move));

            return new JsonResult(uiGame.Move);
        }


        [HttpPost]
        public JsonResult DumMove(int gameID, int oldX, int oldY, int oldZ, int newX, int newY)
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

            if (!repository.Update(new SrzJson().srz(uiGame), pg.Id))
                return new JsonResult(default(Move));

            return new JsonResult(uiGame.Move);
        }

        [HttpPost]
        public JsonResult CkeckGame(int gameId)
        {
            var pg = repository.GetByColumNameFist("GameId", gameId).Item1;
            //any stone win
            if (!pg.BlackCoordinate.Contains("X") || !pg.WhiteCoordinate.Contains("X"))
            {
                var gamesRepostory = new GamesRepository();
                var g = gamesRepostory.GetByColumNameFist("Id", gameId).Item1;
                g.Status = GameStatus.Close;
                if (!pg.WhiteCoordinate.Contains("X")) g.WinUser = g.AcceptUser;
                if (!pg.BlackCoordinate.Contains("X")) g.WinUser = g.RequestUser;
                var b = gamesRepostory.Update(g, g.Id);
                return new JsonResult("Win User:" + g.WinUser);
            }
            return new JsonResult(pg);
        }

    }

}
