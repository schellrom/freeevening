using UnityEngine;
using System.Collections;

public class MultiResolutionUtil : MonoBehaviour 
{
	public float uiBaseWidth = 320.0f;
	public float uiBaseHeight = 480.0f;

	// resolution setting
	void Start () 
//	void Update ()
	{
		UIRoot root = gameObject.GetComponent<UIRoot> ();
		if (root != null)
		{
			root.manualHeight = (int)uiBaseHeight;
			root.minimumHeight = (int)0.0f;
			root.maximumHeight = (int)200000.0f;
		}
		Camera cam = gameObject.GetComponentInChildren<Camera> ();
		if (cam != null) 
		{
			float perX = uiBaseWidth / Screen.width;
			float perY = uiBaseHeight / Screen.height;
			float v = (perX > perY) ? perX : perY;
			cam.orthographicSize = v;
		}
		Destroy (gameObject.GetComponent<MultiResolutionUtil>());
	}
}
