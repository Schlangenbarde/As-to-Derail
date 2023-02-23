using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Internal;
using Cinemachine;


public class Game : MonoBehaviour
{
    #region GLOBAL_VAR_LIST
    public GameObject Player;
    public CinemachineVirtualCamera PlayerCamera;
    public float WorldGravity = -9.81f;
    public LayerMask groundLayer;
    public ChatLog chatLog;
    public GameStart gameStart;
    public RandomTextureLoader textureLoader;
    public GetPathToStation pathRenderer;
    public Switchter switcher;
    public MapUI MapUI;
    public bool ESC_Active = true;
    #endregion

    static Game instance;

    private void Awake()
    {
        instance = this;
    }

    public static Game Get()
    {
        return instance;
    }

    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static GameObject Spawn(GameObject gameObject, Vector3 position, Vector3 rotation = default(Vector3))
    {
        Quaternion quaternion = Quaternion.Euler(rotation);
        return Instantiate(gameObject, position, quaternion);
    }

    public static void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public static void End()
    {
        LoadScene(0);
    }
}
