namespace KS.PizzaEmpire.Common.GameLogic
{
    using BusinessObjects;
    using System;

    /// <summary>
    /// Represents the arguments that will be passed
    /// to an event raised in GamePlayerLogic
    /// </summary>
    public class GamePlayerLogicEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerLogicEventArgs class
        /// </summary>
        public GamePlayerLogicEventArgs(GamePlayer player, WorkInProgress item)
        {
            Player = player;
            WorkItem = item;
        }

        /// <summary>
        /// Creates a new instance of the GamePlayerLogicEventArgs class
        /// </summary>
        public GamePlayerLogicEventArgs(GamePlayer player, ItemQuantity item)
        {
            Player = player;
            ItemQuantity = item;
       }

        /// <summary>
        /// Creates a new instance of the GamePlayerLogicEventArgs class
        /// </summary>
        public GamePlayerLogicEventArgs(GamePlayer player, int value)
        {
            Player = player;
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the GamePlayerLogicEventArgs class
        /// </summary>
        public GamePlayerLogicEventArgs(GamePlayer player)
        {
            Player = player;
        }

        /// <summary>
        /// The GamePlayer the event pertains to
        /// </summary>
        public GamePlayer Player { get; protected set; }

        /// <summary>
        /// The item the event pertains to
        /// </summary>
        public ItemQuantity ItemQuantity { get; protected set; }

        /// <summary>
        /// The work item the event pertains to
        /// </summary>
        public WorkInProgress WorkItem { get; protected set; }

        /// <summary>
        /// An integer value the event pertains to
        /// </summary>
        public int? Value { get; protected set; }
    }
}
