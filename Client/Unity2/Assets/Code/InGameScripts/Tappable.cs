namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
	public class Tappable : MonoBehaviour 
	{
		public GUIElementEnum TappedElement;
		protected bool isTapped;

		public virtual void Tap()
		{
			Debug.Log ("Tappable Tap!");

			TutorialManager.Instance.TryAdvance (
				new GUIEvent { GEvent = GUIEventEnum.Tap, Element = TappedElement });
		}

		public virtual void UnTap()
		{
			Debug.Log ("Tappable UnTap!");
		}
	}
}