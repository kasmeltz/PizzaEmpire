using KS.PizzaEmpire.Business.Logic;

namespace GameLogic.GamePlayerLogic
{
    /// <summary>
    /// Represents the game logic that has to do with Game Players
    /// </summary>
    public class GamePlayerManager
    {
        /// <summary>
        /// Creates a brand new game player - e.g. a Game Player who is logging in for
        /// the first time with no persistant state
        /// </summary>
        /// <returns></returns>
        public static GamePlayer CreateNewGamePlayer()
        {
            GamePlayer player = new GamePlayer();
            player.Coins = 1000;
            player.Coupons = 5;            
            return player;
        }
    }
}
