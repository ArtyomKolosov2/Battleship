using Battleship.Core.Game.Services;
using Battleship.Core.Input;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.Output;
using Battleship.Core.ValueObjects.Shot;

namespace Battleship.Core.Game;

public class GameEngine
{
    private readonly IBattleshipInput _input;
    private readonly IBattleshipOutput _output;
    private readonly IShipPositionGenerator _shipPositionGenerator;
    private readonly IShipFactory _shipFactory;
    private readonly Board _board;
    private readonly List<Ship> _ships;

    public GameEngine(IBattleshipInput input, IBattleshipOutput output, IShipPositionGenerator shipPositionGenerator, IShipFactory shipFactory)
    {
        _input = input;
        _output = output;
        _shipPositionGenerator = shipPositionGenerator;
        _shipFactory = shipFactory;
        _board = new Board();
        _ships = new List<Ship>();
    }
    
    public void StartGame()
    {
        InitGameObjects();
        
        _output.OutputCurrentStateOfBoard(_board);

        StartGameProcess();
    }

    private void StartGameProcess()
    {
        while (!AllShipsAreDestroyed)
            PlayRound();
        
        _output.OutputGameMessage("All ships are destroyed! You won!");
    }

    private void PlayRound()
    {
        var coordinates = _input.GetCoordinatesFromUser();
        var shotResult = _board.RegisterShot(coordinates);
        
        var shotMessage = shotResult switch
        {
            { IsFailure: true } => shotResult.Error!.Message,
            { Data.ShotResultValue: ShotResultValue.Hit } => $"Hit, {shotResult.Data!.Ship!.Name}!",
            { Data.ShotResultValue: ShotResultValue.Miss } => "Miss!",
            { Data.ShotResultValue: ShotResultValue.Sunk } => $"Sunk, {shotResult.Data!.Ship!.Name}!",
            _ => throw new ArgumentOutOfRangeException(nameof(shotResult.Data.ShotResultValue), "Unexpected shot status!"),
        };

        _output.OutputGameMessage(shotMessage);
        _output.OutputCurrentStateOfBoard(_board);
    }
    

    private void InitGameObjects()
    {
        _board.InitBoard();
         _ships.AddRange(_shipFactory.BuildShips());
        _shipPositionGenerator.AddShipsToBoard(_board, _ships);
    }
    
    private bool AllShipsAreDestroyed => _ships.All(x => x.IsDestroyed);
}

