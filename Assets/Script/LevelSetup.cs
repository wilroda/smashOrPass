using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSetup : MonoBehaviour
{
    public float sceneWaitTime;
    public TMP_Text timeText;
    public float levelTime;
    public TMP_Text objectiveText;
    public int pompkinsToSquash;
    int squashedPompkins;
    bool gameEnd;
    public Spawner spawner;

    public Animator scoreBoardWin;
    public Animator scoreBoardLoose;


    // Update is called once per frame
    void Update()
    {
        squashedPompkins = PressController.instance.pompkinSquashed;

        levelTime -= Time.deltaTime;
        timeText.text = levelTime.ToString("F0");

        if(PressController.instance.lives == 0)
        {
            
            StartCoroutine(WaitForSceneReload());
            PressController.instance.LevelLost();
            scoreBoardLoose.SetTrigger("Score");
            gameEnd = true;
            spawner.isWorking = false;
        } else if(levelTime <= 0f && !gameEnd)
        {
            
            StartCoroutine(WaitForSceneReload());
            PressController.instance.LevelLost();
            scoreBoardLoose.SetTrigger("Score");
            gameEnd = true;
            spawner.isWorking = false;
        }

        objectiveText.text = squashedPompkins + " / " + pompkinsToSquash;

        if(squashedPompkins == pompkinsToSquash && !gameEnd)
        {
            StartCoroutine(WaitForNextSceneLoad());
            PressController.instance.LevelWin();
            scoreBoardWin.SetTrigger("Score");
            gameEnd = true;
            spawner.isWorking = false;
        }
        
    }

    private IEnumerator WaitForSceneReload() {
        yield return new WaitForSeconds(sceneWaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitForNextSceneLoad() {
        yield return new WaitForSeconds(sceneWaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
