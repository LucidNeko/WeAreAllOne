using UnityEngine;
using System.Collections;

public class TempContainer : MonoBehaviour
{

	private static TempContainer m_Instance;
	public static TempContainer Instance {
		get { 
			if(m_Instance == null) {
				GameObject go = new GameObject("Temp Container");
				m_Instance = go.AddComponent<TempContainer>();
				return m_Instance;
			} else {
				return m_Instance;
			}
		}
		private set { m_Instance = value; }
	}
}

