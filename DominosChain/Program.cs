namespace DominosChain;

static class Program
{
    static void Main(string[] args)
    {
        // Path to the input file
        // Any format is supported as long as 2 values of a domino stone are separated by vertical delimiter "|"
        string filePath = "../../../dominoes.txt"; 
        List<DominoStone> dominos = DominoReader.ReadDominosFromFile(filePath);

        // Print the loaded domino stones
        foreach (var domino in dominos)
        {
            Console.WriteLine(domino);
        }

        DominoProblemSolver ps = new DominoProblemSolver(dominos);
        try
        {
            List<DominoStone>? chain = ps.Solve();
            
            if (chain == null)
            {
                Console.WriteLine("Cannot build circular chain from this set of domino stones.");
                return;
            }
            Console.WriteLine("The circular chain from the given domino stones:");
            DominoStone? prevStone = null;
            foreach (var nextStone in chain)
            {
                if (prevStone!= null && nextStone.A != prevStone.B)
                {
                    nextStone.TurnOver();
                    if (nextStone.A != prevStone.B)
                    {
                        Console.WriteLine(
                            $"Error in the solution, stone {prevStone} does not match stone {nextStone}");
                        return;
                    }
                }

                prevStone = nextStone;
                Console.WriteLine(nextStone);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}