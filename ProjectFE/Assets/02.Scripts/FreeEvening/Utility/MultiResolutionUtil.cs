using UnityEngine;

namespace FreeEvening.Utility
{
	/// <summary>NGUI 상에서 화면 해상도를 맞추는 기능</summary>
	public class MultiResolutionUtil : MonoBehaviour 
	{
		public float uiBaseWidth = 320.0f;
		public float uiBaseHeight = 480.0f;
		
#region - MonoBehaviour Methods
		void Start () 
		{
			// resolution setting
			UIRoot _root = gameObject.GetComponent<UIRoot> ();
			if (_root != null)
			{
				_root.manualHeight = (int)uiBaseHeight;
				_root.minimumHeight = (int)0.0f;
				_root.maximumHeight = (int)200000.0f;
			}
			Camera _cam = gameObject.GetComponentInChildren<Camera> ();
			if (_cam != null) 
			{
				float perX = uiBaseWidth / Screen.width;
				float perY = uiBaseHeight / Screen.height;
				float v = (perX > perY) ? perX : perY;
				_cam.orthographicSize = v;
			}
			Destroy (gameObject.GetComponent<MultiResolutionUtil>());
		}
#endregion
	}
}