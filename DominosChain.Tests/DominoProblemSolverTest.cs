using System;
using System.Collections.Generic;
using DominosChain;
using JetBrains.Annotations;
using Xunit;

namespace DominosChain.Tests;

[TestSubject(typeof(DominoProblemSolver))]
public class DominoProblemSolverTest
{

    [Fact]
    public void Solve_ShouldThrowException_WhenOddNumberOfValues()
    {
        string input =
            "[6|1], [6|2], [1|1], [4|4], [4|3], [1|2], [6|4], [1|3], [2|2], [3|6], [2|3], [3|3], [1|4], [4|5]";
        string error = String.Empty;
        List<DominoStone> stones = DominoReader.ReadDominosFromString(input, out error);
        if(stones == null || error != String.Empty)
            Assert.Fail("Parse input error");

        DominoProblemSolver ps = new DominoProblemSolver(stones);
        var exception = Assert.Throws<DominoProblemException>(() => ps.Solve());
        Assert.Contains("occurs", exception.Message);
    }
    
    [Fact]
    public void Solve_ChainCreationImpossible()
    {
        string input =
            "[3|1], [1|2], [3|2], [4|5], [5|6] [6|4]";
        string error = String.Empty;
        List<DominoStone> stones = DominoReader.ReadDominosFromString(input, out error);
        if(stones == null || error != String.Empty)
            Assert.Fail("Parse input error");

        DominoProblemSolver ps = new DominoProblemSolver(stones);
        Assert.Null(ps.Solve());
    }
    
    [Theory]
    [InlineData("[6|1], [6|2], [1|1], [4|4], [4|3], [1|2], [6|4], [1|3], [2|2], [3|6], [2|3], [3|3], [1|4], [4|2], [6|6], [5|6], [6|5]", 17)]
    [InlineData("[1|2], [2|3], [3|4], [4|5], [5|6], [6|7], [7|8], [8|9], [9|10], [10|11], [11|12], [12|13], [13|14], [14|15], [15|16], [16|17], [17|18], [18|19], [19|20], [20|1]", 20)]
    [InlineData("(8|2), (5|9), (9|4), (3|5), (5|7), (8|3), (1|8), (7|5), (4|6), (9|6), (6|3), (2|4), (9|2), (7|4), (6|8), (3|8), (6|7), (4|3), (8|5), (2|7), (3|9), (1|6), (7|9), (5|1), (4|1)", 25)]
    public void Solve_ChainCreatedSuccessfully(string input, int amount)
    {
        string error = String.Empty;
        List<DominoStone> stones = DominoReader.ReadDominosFromString(input, out error);
        if(stones == null || error != String.Empty)
            Assert.Fail("Parse input error");

        DominoProblemSolver ps = new DominoProblemSolver(stones);
        var chain = ps.Solve();
        Assert.NotNull(chain);
        Assert.Equal(chain.Count, amount);
        DominoStone firstStone = chain[0];
        DominoStone nextStone = chain[1];
        if(nextStone.A != firstStone.B && nextStone.B != firstStone.B)
            firstStone.TurnOver();
        DominoStone prevStone = firstStone;
        for (int i = 1; i<amount; i++)
        {
            nextStone = chain[i];
            if (nextStone.A != prevStone.B)
            {
                nextStone.TurnOver();
            }
            if (nextStone.A != prevStone.B)
            {
                Assert.Fail($"Error in the solution, stone {prevStone} does not match stone {nextStone}");
            }

            prevStone = nextStone;
        }
        Assert.True(nextStone.B == firstStone.A);
    }
}