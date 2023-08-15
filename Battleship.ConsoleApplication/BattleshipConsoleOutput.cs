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
    
    public void OutputCurrentStateOfBoard(IBoardViewModel boardViewModel)
    {
        var stringBuilder = new StringBuilder();
        
        AddLettersForBoard(stringBuilder);
        AddBoardRepresentation(boardViewModel, stringBuilder);
        AddLettersForBoard(stringBuilder);

        Console.WriteLine(stringBuilder.ToString());
    }

    private static void AddBoardRepresentation(IBoardViewModel boardViewModel, StringBuilder stringBuilder)
    {
        var panels = boardViewModel.GetBoardPanelsViewModels();
        
        var panelRepresentation = panels.Select(row => row.Select(panel => panel switch
        {
            { StatusValue: PanelStatusValue.Hit } => "✕",
            { StatusValue: PanelStatusValue.Miss } => "+",
            { StatusValue: PanelStatusValue.Default } => "•",
            _ => throw new ArgumentOutOfRangeException(nameof(panel.StatusValue), "Unexpected status value.")
        })).ToArray();

        var rowCount = 1;
        foreach (var row in panelRepresentation)
        {
            var spaceCount = 1 - rowCount / 10;
            stringBuilder.Append($"{rowCount}{new string(' ', spaceCount)}");
            
            foreach (var panel in row)
            {
                stringBuilder.Append($" {panel}");
            }

            stringBuilder.Append($" {rowCount}");
            rowCount++;
            stringBuilder.AppendLine();
        }
    }

    private static void AddLettersForBoard(StringBuilder stringBuilder)
    {
        const int initialSpaceCount = 2;
        stringBuilder.Append(new string(' ', initialSpaceCount));
            
        for (var c = 'A'; c <= 'J'; c++)
            stringBuilder.Append($" {c}");

        stringBuilder.AppendLine();
    }

    public void OutputGameMessage(NotEmptyString message)
    {
        Console.WriteLine(message.ToString());
    }
}