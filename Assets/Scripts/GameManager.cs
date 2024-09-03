using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is null!");
            }
            return instance;
        }
    }

    public bool HasKey { get; set; }

    private GameObject ActivePopUp;
    private Animator PopupAnim;

    public bool isPaused = false;

    public bool hasJump = false;
    public bool hasSword = false;

    private Player player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.E))
        {
            Unpause();
        }
    }

    public void Pause(GameObject Popup)
    {
        ActivePopUp = Popup;
        isPaused = true;

        PopupAnim = ActivePopUp.GetComponent<Animator>();
        PopupAnim.updateMode = AnimatorUpdateMode.UnscaledTime;

        Time.timeScale = 0;

        ActivePopUp.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        ActivePopUp.SetActive(false);
        isPaused = false;
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //public void Save()
    //{
    //    PlayerPrefs.SetInt("Jump", BoughtJump);
    //    PlayerPrefs.SetInt("Sword", BoughtSword);
    //}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            StartCoroutine(WaitAndInitialize());
        }
    }

    private IEnumerator WaitAndInitialize()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player != null)
        {
            if (hasJump)
            {
                player.hasDoubleJump = true;
            }
            if (hasSword)
            {
                player.SwipeAnim(1.5f);
            }
        }
        else
        {
            Debug.LogError("Player не знайдено.");
        }
    }
}