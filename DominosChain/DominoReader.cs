using System.Text.RegularExpressions;

namespace DominosChain;

public static class DominoReader
{
    public static List<DominoStone>? ReadDominosFromFile(string filePath, out string errorMessage)
    {
        errorMessage = String.Empty;
        string fileContent;
        try
        {
            // Read all file content
            fileContent = File.ReadAllText(filePath);
        }
        catch (FileNotFoundException e)
        {
            errorMessage = e.Message;
            return null;
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
            return null;
        }

        return ReadDominosFromString(fileContent, out errorMessage);
    }

    public static List<DominoStone>? ReadDominosFromString(string inputString, out string errorMessage)
    {
        errorMessage = string.Empty;
        // Regex pattern to match domino in the format a|b
        // There is no range of possible values on the domino stones
        string pattern = @"(\d+)\|(\d+)";

        MatchCollection matches = Regex.Matches(inputString, pattern);

        if (matches.Count == 0)
        {
            errorMessage = "No dominos were found in the input text. The domino format a|b is expected with any separator between dominos";
            return null;
        }

        List<DominoStone> dominoStones = new List<DominoStone>();
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