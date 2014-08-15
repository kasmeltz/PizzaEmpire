namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
	public class Droppable : MonoBehaviour 
	{
		public Rect DropArea;
		
		public virtual void Drop(Vector3 screenPosition)
		{
			screenPosition = Camera.main.ScreenToWorldPoint(screenPosition);
			Vector3 position = transform.position;
			position.x = screenPosition.x;
			position.y = screenPosition.y;
			transform.position = position;
		}
	}
}
