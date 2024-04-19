
namespace CardMatch
{
    public interface ICard
    {
        int GetCardID();
        bool IsFlipped();
        void Flip();
        void Match();
    }
}