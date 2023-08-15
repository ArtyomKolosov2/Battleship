using Battleship.Core.Exceptions;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Error;
using Battleship.Core.ValueObjects.Panel;
using Battleship.Core.ValueObjects.Shot;
using Battleship.Shared;

namespace Battleship.Core.Game;

public class Board : IBoardViewModelProvider
{
    public const int MaxRows = 10;
    public const int MaxColumns = 10;

    private readonly HashSet<Coordinates> _shots = new();
    private readonly Panel[,] _panels = new Panel[MaxRows, MaxColumns];
    
    internal void InitBoard()
    {
        if (_panels.Cast<Panel>().Any(x => x is not null))
            BoardAlreadyInitializedException.Throw();
            
        for (var row = 0; row < MaxRows; row++)
            for (var column = 0; column < MaxColumns; column++)
                _panels[row, column] = new Panel(row, column);
    }
    
    internal Result<Panel, GameError> GetPanelAtCoordinates(Coordinates coordinates)
    {
        if (_panels.Cast<Panel>().Any(x => x is null))
            BoardNotInitializedException.Throw();

        return _panels.GetValue(coordinates.Row, coordinates.Column) is Panel panel
            ? Result<Panel, GameError>.Success(panel)
            : Result<Panel, GameError>.Failure(GameError.WithMessage("Provided coordinates are invalid."));
    }
    
    internal Result<ShotResult, GameError> RegisterShot(Coordinates coordinates)
    {
        if (_shots.Contains(coordinates))
            return Result<ShotResult, GameError>.Failure(GameError.WithMessage("Cannot register shot on coordinates. It was already shot."));
        
        var panelResult = GetPanelAtCoordinates(coordinates);
        
        if (!panelResult.IsSuccess) 
            return Result<ShotResult, GameError>.Failure(panelResult.Error!);
        
        _shots.Add(coordinates);
        return Result<ShotResult, GameError>.Success(panelResult.Data!.RegisterShot());

    }

    public IEnumerable<IEnumerable<PanelViewModel>> GetBoardPanelsViewModels()
    {
        if (_panels.Cast<Panel>().Any(x => x is null))
            BoardNotInitializedException.Throw();
        
        var rowCount = _panels.GetLength(0);
        var colCount = _panels.GetLength(1);

        for (var i = 0; i < rowCount; i++)
            yield return Enumerable.Range(0, colCount).Select(j => new PanelViewModel(_panels[i, j])).ToList();
    }
}