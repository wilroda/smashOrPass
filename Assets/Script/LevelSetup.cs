using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSetup : MonoBehaviour
{
    public static LevelSetup instance;

    [Header ("Level Time | Level Objective | Level Transition Time")]
    public float time;
    public int pompkinsToSquash;
    public float transitionTime;

    [Header ("Press Setup")]

    public int healthPoints = 1;
    public float animationSpeed = 10f;
    public float coolDownTime = 2f;

    [Header ("Spawner Setup")]
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 2f;
    public float minObjectSpeed = 5f;
    public float maxObjectSpeed = 5f;
    public Transform[] spawnPoints;
    public GameObject[] objects;

    [Header ("Other")]

    [HideInInspector]
    public bool gameEnd = false;

    
    public TMP_Text timeText;
    
    public TMP_Text objectiveText;
    
    int squashedPompkins;
    
    public Animator scoreBoardWin;
    
    public Animator scoreBoardLoose;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Time Countdown
        time -= Time.deltaTime;
        timeText.text = time.ToString("F0");

        // Pompkin Squash Count
        squashedPompkins = PressController.instance.pompkinSquashed;
        objectiveText.text = squashedPompkins + " / " + pompkinsToSquash;

        // Level Lost - Press Damaged or Time = 0 & Level Win - Pompkin Squash Objective Met
        if(healthPoints == 0 && !gameEnd)
        {
            gameEnd = true;
            Lost();
        } else if(time <= 0f && !gameEnd)
        {
            gameEnd = true;
            Lost();
        } else if(squashedPompkins == pompkinsToSquash && !gameEnd)
        {
            gameEnd = true;
            Win();
        }
    }

    private IEnumerator WaitForSceneReload() {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitForNextSceneLoad() {
        yield return new WaitForSeconds(transitionTime);
        if(SceneManager.GetActiveScene().buildIndex == 8)
        {
            SceneManager.LoadScene(0);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void Lost() 
    {
        StartCoroutine(WaitForSceneReload());
        PressController.instance.LevelLost();
        scoreBoardLoose.SetTrigger("Score");
    }

    void Win()
    {
        if(healthPoints == 0)
        {
            Lost();
        }
        
        StartCoroutine(WaitForNextSceneLoad());
        PressController.instance.LevelWin();
        scoreBoardWin.SetTrigger("Score");
    }

    
}
