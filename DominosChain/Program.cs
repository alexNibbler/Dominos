namespace DominosChain;

static class Program
{
    static void Main(string[] args)
    {
        // Path to the input file
        // Any format is supported as long as 2 values of a domino stone are separated by vertical delimiter "|"
        string filePath = "../../../dominos.txt";
        string error;
        List<DominoStone> dominos = DominoReader.ReadDominosFromFile(filePath, out error);
        if (dominos == null)
        {
            Console.WriteLine(error);
            return;
        }

        // Print the loaded domino stones
        foreach (var domino in dominos)
        {
            Console.WriteLine(domino);
        }

        try
        {
            DominoProblemSolver ps = new DominoProblemSolver(dominos);
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
        catch (DominoProblemException e)
        {
            Console.WriteLine("Cannot build circular chain from this set of domino stones.");
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}