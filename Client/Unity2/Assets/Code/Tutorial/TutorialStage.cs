﻿namespace KS.PizzaEmpire.Unity
{	
	using System;
	using UnityEngine;
	
	/// <summary>
	/// Represents an item which defines a stage in a tutorial
	/// </summary>
	public class TutorialStage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.TutorialStage"/> class.
		/// </summary>
		public TutorialStage (){}
	
		public Action Render { get; set; }
		public GamePlayerStateCheck PlayerStateCheck { get; set; }
		public GUIEvent GUIEvent { get; set; }
		public bool ShowNextButton { get; set; }
	}
}