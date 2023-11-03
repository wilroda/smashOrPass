using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject title;
    public GameObject backButton;
    public GameObject backButtonClick;

    public void PlayGame()
    {
        PressController.instance.pressAvailable = true;

    }

    public void Credits()
    {
        anim.SetTrigger("Credits");
        StartCoroutine(WaitForCreditsDown());
        title.SetActive(false);
    }

    public void BackCredits()
    {
        anim.SetTrigger("CreditsUp");
        StartCoroutine(WaitForCreditsUp());
        backButton.SetActive(false);
        backButtonClick.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator WaitForCreditsUp() {
        yield return new WaitForSeconds(.5f);
        title.SetActive(true);
    }

    private IEnumerator WaitForCreditsDown() {
        yield return new WaitForSeconds(.2f);
        backButton.SetActive(true);
        backButtonClick.SetActive(true);
    }
}
