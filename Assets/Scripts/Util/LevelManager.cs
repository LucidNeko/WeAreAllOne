using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour, ILevelManager {

	public static LevelManager Instance {
		get;
		private set;
	}

	public GameObject m_PlayerPrefab;

	public GameObject[] m_SpawnPoints;
	public GUIStyle[] m_Styles;

	private int m_PlayerCount = 0;
	private static readonly int m_MaxPlayers = 4;
	private GameObject[] m_Players = new GameObject[m_MaxPlayers];

	private int m_NumControllersConnected = 0;

	private bool m_Respawning = false;

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;

		//don't destroy between scenes
		DontDestroyOnLoad (gameObject);

		m_Styles = new GUIStyle[m_SpawnPoints.Length];
		for (int i = 0; i < m_Styles.Length; i++) {
			Texture2D t = new Texture2D(1, 1);
			t.wrapMode = TextureWrapMode.Repeat;
			t.SetPixel(0, 0, m_SpawnPoints[i].GetComponent<PlayerSpawner>().m_Color);
			t.Apply();
			m_Styles[i] = new GUIStyle();
			m_Styles[i].normal.background = t;
		}
	}

	void Start() {
		string[] joysticks = Input.GetJoystickNames ();

//		CreatePlayer<KeyboardControl> ().transform.position = Vector3.zero;

		for(int i = 0; i < joysticks.Length; i++) {
			GameObject player = CreatePlayer<PS4Control>();
			m_Players[i].GetComponent<PS4Control>().m_Player = i;
		}

		for (int i = m_PlayerCount; i < m_MaxPlayers; i++) {
			CreatePlayer<KeyboardControl> ();
		}


//		if (joysticks.Length == 0) {
//			CreatePlayer<KeyboardControl> ();
//			CreatePlayer<KeyboardControl> ();
//		} else {
//			for(int i = 0; i < joysticks.Length; i++) {
//				GameObject player = CreatePlayer<PS4Control>();
//				m_Players[i].GetComponent<PS4Control>().m_Player = i;
//			}
//		}


	}

	void Update() {
		//watch for new controllers

		if (!m_Respawning) {
			int[] teams = GetTeamInfo ();
			for (int i = 0; i < teams.Length; i++) {
				if (teams [i] == 4) {
					StartCoroutine (Respawn ());
				}
			}
		}
	}

	IEnumerator Respawn() {
		m_Respawning = true;
		yield return new WaitForSeconds (5);
		System.Random rnd = new System.Random();
		int[] numbers = Enumerable.Range(0, 4).OrderBy(r => rnd.Next()).ToArray();

		for(int i = 0; i < numbers.Length; i++) {
			m_SpawnPoints[numbers[i]].GetComponent<PlayerSpawner>().Spawn(m_Players[i]);
		}

		m_Respawning = false;
	}

	public GameObject CreatePlayer<T>() where T : Component, IControl {
		if (m_PlayerCount < m_MaxPlayers) {
			//create player
			GameObject player = Instantiate(m_PlayerPrefab);
			Camera camera = player.GetComponentInChildren<Camera>();
			m_Players[m_PlayerCount] = player;
			player.layer = LayerMask.NameToLayer("Player " + m_PlayerCount);

			//attach control
			player.AddComponent<T>();

			m_SpawnPoints[m_PlayerCount].GetComponent<PlayerSpawner>().Spawn(player);

			m_PlayerCount++;

			//adjust cameras
			UpdateCameras();

			return player;
		}
		return null;
	}
	
	private void UpdateCameras() {
		if(m_PlayerCount == 1) {
			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0,1,1);
		} else if(m_PlayerCount == 2) {
			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0.5f,1,0.5f);
			m_Players[1].GetComponentInChildren<Camera>().rect = new Rect(0,0,1,0.5f);
		} else if(m_PlayerCount == 3) {
			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0.5f,0.5f,0.5f);
			m_Players[1].GetComponentInChildren<Camera>().rect = new Rect(0.5f,0.5f,0.5f,0.5f);
			m_Players[2].GetComponentInChildren<Camera>().rect = new Rect(0,0,0.5f,0.5f);
		} else if(m_PlayerCount == 4) {
			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0.5f,0.5f,0.5f);
			m_Players[1].GetComponentInChildren<Camera>().rect = new Rect(0.5f,0.5f,0.5f,0.5f);
			m_Players[2].GetComponentInChildren<Camera>().rect = new Rect(0,0,0.5f,0.5f);
			m_Players[3].GetComponentInChildren<Camera>().rect = new Rect(0.5f,0,0.5f,0.5f);
		}
	}

	void OnGUI() {
		int[] teams = GetTeamInfo ();

		float height = 10;

		float x = 0;
		float y = Screen.height / 2 - height / 2;

		for (int i = 0; i < teams.Length; i++) {
			float width = Screen.width * teams[i]/m_PlayerCount;

			Rect rect = new Rect (x, y, width, height);
			GUI.Label (rect, GUIContent.none, m_Styles[i]);

			x += width;
		}
	}

	private int[] GetTeamInfo() {
		int[] teams = new int[m_MaxPlayers];
		for(int i = 0; i < m_PlayerCount; i++) {
			for(int team = 0; team < m_MaxPlayers; team++) {
				if(m_Players[i].GetComponent<PlayerStats>().HomeSpawn == m_SpawnPoints[team].GetComponent<PlayerSpawner>()) {
					teams[team]++;
				}
			}
		}
		return teams;
	}
}
