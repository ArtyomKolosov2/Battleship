using Battleship.Core.Input;
using Battleship.Core.Models;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.Output;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Error;
using Battleship.Core.ValueObjects.Shot;
using Battleship.Shared;

namespace Battleship.Core.Game;

public class GameEngine
{
    private readonly IBattleshipInput _input;
    private readonly IBattleshipOutput _output;
    private readonly Board _board;
    private readonly List<Ship> _ships;

    public GameEngine(IBattleshipInput input, IBattleshipOutput output)
    {
        _input = input;
        _output = output;
        _board = new Board();
        _ships = new List<Ship>();
    }

    // Add game results object
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
        var shotResult = RegisterShot(coordinates);
        
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
    
    private Result<ShotResult, GameError> RegisterShot(Coordinates coordinates)
    {
        var panelResult = _board.GetPanelAtCoords(coordinates);
        
        return panelResult.IsSuccess 
            ? Result<ShotResult, GameError>.Success(panelResult.Data!.RegisterShot()) 
            : Result<ShotResult, GameError>.Failure(panelResult.Error!);
    }

    private void InitGameObjects()
    {
        _board.InitBoard();
        CreateShips();
        _board.AddShipsToBoard(_ships);
    }

    private void CreateShips()
    {
        _ships.AddRange(new Ship[]
        {
            new Models.Battleship(),
            new Destroyer(),
            new Destroyer(),
        });
    }

    private bool AllShipsAreDestroyed => _ships.All(x => x.IsDestroyed);
}