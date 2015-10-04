using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
	public static PickupManager Instance {
		get;
		private set;
	}

	public GameObject m_PickupOrbPrefab;

	public GameObject[] m_Guns;

	private GameObject m_Pickups;

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}
		
		Instance = this;
		
		//don't destroy between scenes
		DontDestroyOnLoad (gameObject);
	}

	public void CreatePickup(GameObject item) {
		IEquippable e = item.GetComponent<IEquippable> ();

		if (e == null) {
			return;
		}

		GameObject container = Instantiate (m_PickupOrbPrefab, item.transform.position - Vector3.Scale(item.transform.forward, new Vector3(1,0,1))*3, item.transform.rotation) as GameObject;
		container.transform.parent = m_Pickups.transform;
		item.transform.parent = container.transform;
		item.transform.localPosition = Vector3.zero;
		item.transform.localRotation = Quaternion.identity;

		container.GetComponent<PickupOrb> ().m_Object = item;

	}

	// Use this for initialization
	void Start ()
	{
		m_Pickups = new GameObject ("Pickups");

		for (int i = 0; i < 20; i++) {
			GameObject gun = Instantiate (m_Guns [Random.Range (0, m_Guns.Length)], new Vector3(Random.Range(-15, 15), 1, Random.Range(-15, 15)), Quaternion.identity) as GameObject;
			CreatePickup (gun);
		}
	}
}

