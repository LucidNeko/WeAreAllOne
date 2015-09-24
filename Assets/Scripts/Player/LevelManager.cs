using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour, ILevelManager {

	public static LevelManager Instance {
		get;
		private set;
	}

	public int m_NumPlayers = 1;

	public GameObject m_PlayerPrefab;
	public GameObject m_CameraPrefab;

	private int m_PlayerCount = 0;
	private static readonly int m_MaxPlayers = 4;
	private GameObject[] m_Players = new GameObject[m_MaxPlayers];
	private Camera[] m_Cameras = new Camera[m_MaxPlayers];

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;

		//don't destroy between scenes
		DontDestroyOnLoad (gameObject);
	}

	void Start() {
		for (int i = 0; i < m_NumPlayers; i++) {
			CreatePlayer<KeyboardControl>().transform.position = new Vector3(0,0,0);
		}
//		CreatePlayer<KeyboardControl>().transform.position = new Vector3(0,0,0);
//		CreatePlayer<KeyboardControl>().transform.position = new Vector3(0,0,-5);
//		CreatePlayer<KeyboardControl>().transform.position = new Vector3(0,0,-10);
//		CreatePlayer<KeyboardControl>().transform.position = new Vector3(0,0,-15);
	}

	public GameObject CreatePlayer<T>() where T : Component, IControl {
		if (m_PlayerCount < m_MaxPlayers) {
			//create player
			GameObject player = Instantiate(m_PlayerPrefab);
			Camera camera = Instantiate(m_CameraPrefab).GetComponent<Camera>();
			m_Players[m_PlayerCount] = player;
			m_Cameras[m_PlayerCount] = camera;
			player.layer = LayerMask.NameToLayer("Player " + m_PlayerCount);
			foreach(Transform t in player.GetComponentsInChildren<Transform>(true)) {
				t.gameObject.layer = player.layer;
			}
			camera.cullingMask = camera.cullingMask & ~(1 << player.layer);
			m_PlayerCount++;

			//attach control
			player.AddComponent<T>();

			//setup camera
			camera.GetComponent<FPSCameraScript>().m_Target = player.transform.FindChild("Camera Target");
			camera.GetComponent<FPSCameraScript>().m_Control = player.GetComponent<IControl>();

			//adjust cameras
			UpdateCameras();

			//set random color TODO: Remove
			player.GetComponentInChildren<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f,1f));
			
			return player;
		}
		return null;
	}

	public Camera GetCamera(GameObject player) {
		for (int i = 0; i < m_MaxPlayers; i++) {
			if(m_Players[i] == player) {
				return m_Cameras[i];
			}
		}
		return null;
	}
	
	private void UpdateCameras() {
		if(m_PlayerCount == 1) {
			m_Cameras[0].rect = new Rect(0,0,1,1);
		} else if(m_PlayerCount == 2) {
			m_Cameras[0].rect = new Rect(0,0.5f,1,0.5f);
			m_Cameras[1].rect = new Rect(0,0,1,0.5f);
		} else if(m_PlayerCount == 3) {
			m_Cameras[0].rect = new Rect(0,0.5f,0.5f,0.5f);
			m_Cameras[1].rect = new Rect(0.5f,0.5f,0.5f,0.5f);
			m_Cameras[2].rect = new Rect(0,0,0.5f,0.5f);
		} else if(m_PlayerCount == 4) {
			m_Cameras[0].rect = new Rect(0,0.5f,0.5f,0.5f);
			m_Cameras[1].rect = new Rect(0.5f,0.5f,0.5f,0.5f);
			m_Cameras[2].rect = new Rect(0,0,0.5f,0.5f);
			m_Cameras[3].rect = new Rect(0.5f,0,0.5f,0.5f);
		}
//		if(m_PlayerCount == 1) {
//			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0,1,1);
//		} else if(m_PlayerCount == 2) {
//			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0.5f,1,0.5f);
//			m_Players[1].GetComponentInChildren<Camera>().rect = new Rect(0,0,1,0.5f);
//		} else if(m_PlayerCount == 3) {
//			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0.5f,0.5f,0.5f);
//			m_Players[1].GetComponentInChildren<Camera>().rect = new Rect(0.5f,0.5f,0.5f,0.5f);
//			m_Players[2].GetComponentInChildren<Camera>().rect = new Rect(0,0,0.5f,0.5f);
//		} else if(m_PlayerCount == 4) {
//			m_Players[0].GetComponentInChildren<Camera>().rect = new Rect(0,0.5f,0.5f,0.5f);
//			m_Players[1].GetComponentInChildren<Camera>().rect = new Rect(0.5f,0.5f,0.5f,0.5f);
//			m_Players[2].GetComponentInChildren<Camera>().rect = new Rect(0,0,0.5f,0.5f);
//			m_Players[3].GetComponentInChildren<Camera>().rect = new Rect(0.5f,0,0.5f,0.5f);
//		}
	}

}
