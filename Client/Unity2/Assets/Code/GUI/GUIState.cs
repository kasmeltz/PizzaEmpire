namespace KS.PizzaEmpire.Unity
{
    using Common.BusinessObjects;
	using Common.ObjectPool;
	
    /// <summary>
    /// Represents the state of a GUI item
    /// </summary>
    public class GUIState : IResetable
    {
        /// <summary>
        /// Creates a new instance of the GUIState class
        /// </summary>
        public GUIState()
        {
            Available = true;
            Visible = true;
			StateCheck = new GamePlayerStateCheck();
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
        
		/// <summary>
		/// Copies the state from another instance
		/// </summary>
		/// <param name="other">The GUIState to copy from</param>
		public void CopyFrom(GUIState other)
		{
			Available = other.Available;
			Visible = other.Visible;
			StateCheck.CopyFrom(other.StateCheck);
		}
        
		#region IResetable
		
        public void Reset()
        {
        	Available = false;
        	Visible = false;
        	StateCheck.Reset();        
        }
        
        #endregion
    }
}
