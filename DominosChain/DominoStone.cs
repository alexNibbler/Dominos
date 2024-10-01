namespace DominosChain;

public class DominoStone
{
    public int A { get; set; }
    public int B { get; set; }

    public DominoStone(int a, int b)
    {
        A = a;
        B = b;
    }

    public override string ToString()
    {
        return $"[{A}|{B}]";
    }
    
    public void TurnOver()
    {
        (A, B) = (B, A);
    }
}