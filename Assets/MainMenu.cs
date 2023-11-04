using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject title;
    public GameObject backButton;
    public GameObject creditsButtonClickArea;
    public GameObject backButtonClickArea;
    public GameObject passButtonClickArea;
    public GameObject startButtonClickArea;

    public EventTrigger eventT;

    public CircleWipeController wipeController;

    bool pointerOverUI;

    void Start()
    {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void PlayGame()
    {
        PressControllerMenu.instance.Press();
        PressControllerMenu.instance.LevelWin();
        eventT.enabled = false;
        StartCoroutine(CircleWhipe());
        wipeController.FadeOut();
        wipeController.offset.x -= 0.012f;
        wipeController.offset.y += .34f;
        wipeController.duration = 3.5f;
        title.SetActive(false);
        
    }

    public void Credits()
    {
        PressControllerMenu.instance.ScreenUp();
        PressControllerMenu.instance.shake.start = true;
        anim.SetTrigger("Credits");
        StartCoroutine(WaitForCreditsDown());
        title.SetActive(false);
        creditsButtonClickArea.SetActive(false);
        passButtonClickArea.SetActive(false);
        startButtonClickArea.SetActive(false);
    }

    public void BackCredits()
    {
        PressControllerMenu.instance.ScreenUp();
        PressControllerMenu.instance.shake.start = true;
        anim.SetTrigger("CreditsUp");
        StartCoroutine(WaitForCreditsUp());
        backButton.SetActive(false);
        backButtonClickArea.SetActive(false);
        creditsButtonClickArea.SetActive(true);
        passButtonClickArea.SetActive(true);
        startButtonClickArea.SetActive(true);
    }

    public void Exit()
    {
        PressControllerMenu.instance.PompkinCackling();
        wipeController.FadeOut();
        wipeController.offset.x += .32f;
        wipeController.offset.y -= .2f;
        title.SetActive(false);
        StartCoroutine(CircleWhipeExit());
    }

    private IEnumerator WaitForCreditsUp() {
        yield return new WaitForSeconds(.5f);
        title.SetActive(true);
        anim.ResetTrigger("CreditsUp");
    }

    private IEnumerator WaitForCreditsDown() {
        yield return new WaitForSeconds(.2f);
        backButton.SetActive(true);
        backButtonClickArea.SetActive(true);
        anim.ResetTrigger("Credits");
    }

    private IEnumerator CircleWhipe() {
        yield return new WaitForSeconds(3.5f);
        PressControllerMenu.instance.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    private IEnumerator CircleWhipeExit() {
        yield return new WaitForSeconds(4.5f);        
        Application.Quit();
    }
}

