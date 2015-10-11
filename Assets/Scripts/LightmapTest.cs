using UnityEngine;
using System.Collections;

public class LightmapTest : MonoBehaviour
{

	public Renderer m_visual;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("hello");
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("hello");
		LightmapData[] data = LightmapSettings.lightmaps;
		Debug.Log (data.Length);
		m_visual.material.mainTexture = data [0].lightmapFar;
	}
}

