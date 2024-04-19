namespace CardMatch
{
    public class GameStats
    {
        /// <summary>
        /// LayoutType,
        /// We can add any layout here 
        /// </summary>
        public enum LayoutType
        {
            Grid2x2 = 0,
            Grid2x3 = 1,
            Grid5x6 = 2
        }

        public static LayoutType layoutType { get; set; }
    }
}
