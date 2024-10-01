using System.Text.RegularExpressions;

namespace DominosChain;

public static class DominoReader
{
    public static List<DominoStone> ReadDominosFromFile(string filePath)
    {
        List<DominoStone> dominoStones = new List<DominoStone>();

        // Read all file content
        string fileContent = File.ReadAllText(filePath);

        // Regex pattern to match domino in the format a|b
        // There is no range of possible values on the domino stones
        string pattern = @"(\d+)\|(\d+)";
        
        MatchCollection matches = Regex.Matches(fileContent, pattern);

        // Loop through each match and create DominoStone objects
        foreach (Match match in matches)
        {
            int a = int.Parse(match.Groups[1].Value);
            int b = int.Parse(match.Groups[2].Value);
            dominoStones.Add(new DominoStone(a, b));
        }

        return dominoStones;
    }
}