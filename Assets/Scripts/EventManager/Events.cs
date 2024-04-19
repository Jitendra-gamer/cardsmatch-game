
namespace CardMatch
{
    public class Events
    {
        //CardClick
        public const string CardClicked = "CardClicked";

        //Score
        public const string MatchSuccessfull = "MatchSuccessfull";
        public const string MatchFailed = "MatchFailed";

        //UI
        public const string GridInitialize = "GridInitialize";
        public const string ScoreUpdateUI = "ScoreUpdateUI";
        public const string TurnsUpdateUI = "TurnsUpdateUI";
        public const string GameWin = "GameWin";
        public const string RestartGame = "RestartGame";
    }
}