namespace BaC
{
    public class RandomGuesser : IGuesser
    {
        public override string ToString()
        {
            return "Random Selection";
        }  

        public Code Guess(Code[] codes)
        {
            return Solver.RandomPick(codes);
        }
    }
}
