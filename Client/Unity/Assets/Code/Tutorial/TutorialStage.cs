namespace KS.PizzaEmpire.Unity
{	
	/// <summary>
	/// Represents an item which defines a stage in a tutorial
	/// </summary>
	public class TutorialStage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.TutorialStage"/> class.
		/// </summary>
		public TutorialStage (){}
	
		public TutorialGUI GUI { get; set; }
		public GamePlayerStateCheck PlayerStateCheck { get; set; }
		public GUIEventCheck GUIEventCheck { get; set; }
	}
}