using Battleship.Core.Input;
using Battleship.Core.Models;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.Output;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Shot;

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
        
        _output.OutputGameMessage("Win!");
    }

    private void PlayRound()
    {
        var coordinates = _input.GetCoordinatesFromUser();
        var shotResult = RegisterShot(coordinates);
        var shotMessage = shotResult switch
        {
            { ShotResultEnum: ShotResultEnum.Hit } => "Hit!",
            { ShotResultEnum: ShotResultEnum.Miss } => "Miss!",
            { ShotResultEnum: ShotResultEnum.Sunk } => "Sunk!",
            _ => throw new ArgumentOutOfRangeException("Invalid shot status!"),
        };

        _output.OutputGameMessage(shotMessage);
        _output.OutputCurrentStateOfBoard(_board);
    }

    private ShotResult RegisterShot(Coordinates coordinates)
    {
        var panel = _board.GetPanelAtCoords(coordinates);
        if (panel is null)
            throw new Exception();
        
        return panel.RegisterShot();
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