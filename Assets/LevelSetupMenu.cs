using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSetupMenu : MonoBehaviour
{
    public static LevelSetupMenu instance;

    [Header ("Level Transition Time")]
    public float transitionTime = 2f;

    [Header ("Debug")]
    public bool smashed = false;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        // Level Lost - Press Damaged or Time = 0 & Level Win - Pompkin Squash Objective Met
        if(smashed)
        {
            Smash();
        }

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

    void Smash()
    { 
        StartCoroutine(WaitForNextSceneLoad());
        PressController.instance.LevelWin();
    }

    
}
