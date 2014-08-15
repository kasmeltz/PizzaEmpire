namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;

	public class Draggable : MonoBehaviour 
	{
		public GUIElementEnum DraggedElement;

		public void Drag(Vector3 screenPosition)
		{
			screenPosition = Camera.main.ScreenToWorldPoint(screenPosition);
			Vector3 position = transform.position;
			position.x = screenPosition.x;
			position.y = screenPosition.y;
			transform.position = position;
		}
	}
}