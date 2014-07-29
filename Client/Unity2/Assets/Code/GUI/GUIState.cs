namespace KS.PizzaEmpire.Unity
{
    using Common.BusinessObjects;

    /// <summary>
    /// Represents the state of a GUI item
    /// </summary>
    public class GUIState
    {
        /// <summary>
        /// Creates a new instance of the GUIState class
        /// </summary>
        public GUIState()
        {
            Available = true;
            Visible = true;
        }

        /// <summary>
        /// Whether the item is available for interaction if it is visisble
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// Whether the item should be drawn 
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// The GamePlayerStateCheck that should be used to determine if
        /// the element is available
        /// </summary>
        public GamePlayerStateCheck StateCheck { get; set; }

        /// <summary>
        /// Updates the Available status based on the current state of the provided GamePlayer
        /// </summary>
        public void Update(GamePlayer player)
        {
            Available = true;

            if (StateCheck != null)
            {
                if (!StateCheck.CheckAll(player))
                {
                    Available = false;
                }
            }
        }
    }
}
