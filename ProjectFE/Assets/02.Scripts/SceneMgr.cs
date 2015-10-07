using UnityEngine;
using System.Collections;

public class SceneMgr : MonoBehaviour 
{
	public void OnTitleScene ()
	{
		Application.LoadLevel ("Title");
	}

	public void OnGame1Scene ()
	{
		Application.LoadLevel ("Game_3Match");
	}

	public void OnGame2Scene ()
	{
		Application.LoadLevel ("Game_ColorMatch");
	}

	public void OnGame3Scene ()
	{
		Application.LoadLevel ("Game_LinkMatch");
	}

	public void OnGame4Scene ()
	{
		Application.LoadLevel ("Game_PuzzleDragon");
	}
}
