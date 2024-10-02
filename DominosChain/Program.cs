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
            if( error != null ) 
                Console.WriteLine(error);
            else
                Console.WriteLine("Error while reading input from file \"dominos.txt\"");
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
            foreach (var nextStone in chain)
            {
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