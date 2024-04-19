using UnityEngine;

namespace CardMatch
{
    public class GameUtility
    {
        internal static Vector2 GetGridType()
        {
            Vector2 gridLayout = new Vector2(0, 0);
            switch (GameStats.layoutType)
            {
                case GameStats.LayoutType.Grid2x2:
                    gridLayout = new Vector2(2, 2);
                    break;

                case GameStats.LayoutType.Grid2x3:
                    gridLayout = new Vector2(2, 3);
                    break;

                case GameStats.LayoutType.Grid5x6:
                    gridLayout = new Vector2(5, 6);
                    break;
            }

            return gridLayout;
        }
    }
}
