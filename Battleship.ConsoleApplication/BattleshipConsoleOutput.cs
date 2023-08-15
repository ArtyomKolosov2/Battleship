using System.Text;
using Battleship.Core.Game;
using Battleship.Core.Output;
using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Panel;

namespace Battleship.ConsoleApplication;

public class BattleshipConsoleOutput : IBattleshipOutput
{
    static BattleshipConsoleOutput()
    {
        Console.OutputEncoding = Encoding.UTF8; 
    }
    
    public void OutputCurrentStateOfBoard(IBoard board)
    {
        PrintLettersForBoard();

        Console.WriteLine();

        PrintBoard(board);

        PrintLettersForBoard();

        Console.WriteLine();
    }

    private static IEnumerable<IEnumerable<string>> PrintBoard(IBoard board)
    {
        var messageBuffList = new List<List<string>>();
        var iteratedColumnsCount = 0;
        var rowCount = 1;
        var panels = board.GetPanels();
        
        var panelRepresentation = panels.Select(row => row.Select(panel => panel.StatusValue switch
        {
            PanelStatusValue.Hit => "✕",
            PanelStatusValue.Miss => "*",
            PanelStatusValue.Default => "•",
            _ => throw new ArgumentOutOfRangeException(nameof(panel.StatusValue), "Unexpected status value.")
        }));
        
        /*foreach (var panel in board)
        {
            if (iteratedColumnsCount == 0)
                Console.Write($"{rowCount}  ");

            var output = panel.StatusValue switch
            {
                PanelStatusValue.Hit => "✕",
                PanelStatusValue.Miss => "*",
                PanelStatusValue.Default => "•",
                _ => throw new ArgumentOutOfRangeException(nameof(panel.StatusValue), "Unexpected status value.")
            };

            if (panel.IsOccupiedByShip)
                output = "+";

            Console.Write($"{output} ");
            iteratedColumnsCount++;
            if (iteratedColumnsCount % Board.MaxColumns != 0)
                continue;

            Console.Write($" {rowCount}");
            Console.WriteLine();
            rowCount++;
            iteratedColumnsCount %= Board.MaxColumns;
        }*/
    }

    private static void PrintLettersForBoard()
    {
        for (var i = 'A'; i <= 'J'; i++)
            Console.Write($" {i}");
    }

    public void OutputGameMessage(NotEmptyString message)
    {
        Console.WriteLine(message.ToString());
    }
}