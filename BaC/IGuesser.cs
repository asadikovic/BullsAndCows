namespace BaC
{
    public interface IGuesser
    {
        string ToString();
        Code Guess(Code[] codes);
    }
}
