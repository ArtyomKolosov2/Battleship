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

    private static void PrintBoard(IBoard board)
    {
        // ToDo: return array of strings
        var iteratedColumnsCount = 0;
        var rowCount = 1;
        foreach (var panel in board)
        {
            if (iteratedColumnsCount == 0)
                Console.Write($"{rowCount}  ");

            var output = panel.Status switch
            {
                PanelStatus.Hit => "✕",
                PanelStatus.Miss => "*",
                PanelStatus.Default => "•",
                _ => throw new ArgumentOutOfRangeException(nameof(panel.Status), "Unexpected status value.")
            };

            Console.Write($"{output} ");
            iteratedColumnsCount++;
            if (iteratedColumnsCount % Board.MaxColumns != 0)
                continue;

            Console.Write($" {rowCount}");
            Console.WriteLine();
            rowCount++;
            iteratedColumnsCount %= Board.MaxColumns;
        }
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