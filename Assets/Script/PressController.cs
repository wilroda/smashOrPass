using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class PressController : MonoBehaviour
{
    public SquishPompkin particles;
    public static PressController instance;
    public int lives = 3;
    public float pressSpeed = 5f;
    public bool pressAvailable = true;
    public float pressCoolDownTime = 2f;
    public float coolDownCount = 0f;
    private bool pressed = false;

    public Image loadingImage;
    public TMP_Text loadingText;

    public Shake shake;
    public int pompkinSquashed = 0;
    Animator anim;
    bool gameStart = true;
    bool pressReady = true;

    public AudioClip[] scream;
    public AudioSource screamSource;
    public AudioSource pompkinSmashSource;
    public AudioSource pompkinCacklingSource;
    public AudioSource pompkinBoomSource;
    public AudioClip pompkinSmashClip;
    public AudioClip pompkinCacklingClip;
    public AudioClip pompkinBoomClip;
    public AudioSource pressSource;
    public AudioSource pressDingSource;
    public AudioSource levelSource;
    public AudioClip levelWinClip;
    public AudioClip levelLostClip;
    AudioClip screamClip;
    public AudioClip pressClip;
    public AudioClip pressDingClip;
    bool levelLostSoundPlayed = false;



    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {        
        anim = GetComponentInParent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {

        // Press gauge starts green and with OK sign!
        if(gameStart)
        {
            coolDownCount = pressCoolDownTime;
            loadingText.text = "OK";   
            loadingImage.fillAmount = 1f;
            loadingImage.color = new Color32(0,255,94,255);
        }

        if(pressed)
        {
            loadingImage.fillAmount = Map(coolDownCount, 0, pressCoolDownTime, 0f, 1f);
        }

        
        
        if(coolDownCount < pressCoolDownTime)
        {
            loadingImage.color = new Color32(255,22,0,255);
            loadingText.text = Mathf.RoundToInt(Map(coolDownCount, 0, pressCoolDownTime, 0f, 1f) * 100).ToString();
        }
        else
        {
            loadingText.text = "OK";
            loadingImage.color = new Color32(0,255,94,255);
        }

        if(loadingImage.fillAmount > 0.95f)
        {
            loadingImage.fillAmount = Mathf.Round(loadingImage.fillAmount);
        }
            

        if(loadingImage.fillAmount == 1f && !pressReady)
        {
            PressDingAudioClip();
            pressReady = true;
        }

        


        // pressSource.Pause();
        if(Input.GetButton("Fire1") && pressReady) {
            PressAudioClip();
            Usability();   
            pressed = true;
            pressReady = false;
            gameStart = false;
        } else 
        {
            anim.ResetTrigger("Pressing");
        }

        if(pressed)
        {
            coolDownCount += Time.deltaTime;
        }

        if(coolDownCount >= pressCoolDownTime)
        {
            pressed = false;
        }


    }

    void Usability()
    {
        if(pressAvailable == false)
        {
            return;
        }
        shake.start = true;
        coolDownCount = 0f; 
        anim.speed = pressSpeed;
        anim.SetTrigger("Pressing");

        StartCoroutine(StartCooldown());
    }

      public IEnumerator StartCooldown()
    {
        pressAvailable = false;

        yield return new WaitForSeconds(pressCoolDownTime);

        pressAvailable = true;
    }    

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 7)
        {
            lives -= 1;
        }
    }

    float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public void ParticlePlay(PumpkinVisualRandomizer ppvr)
    {
        particles.SmashIt(ppvr);
    }

    public void PompkinScream()
    {
        int index = Random.Range(0, scream.Length);
        screamClip = scream[index];
        screamSource.clip = screamClip;
        screamSource.Play();
    }

    public void PompkinSmash() 
    { 
        if (!pompkinSmashSource.isPlaying) 
        { 
            pompkinSmashSource.clip = pompkinSmashClip;
            pompkinSmashSource.Play(); 
        } 
    }

    public void PressAudioClip() 
    { 
        if (!pressSource.isPlaying) 
        { 
            pressSource.clip = pressClip;
            pressSource.Play(); 
        } 
    }

    public void PressDingAudioClip() 
    { 
        if (!pressDingSource.isPlaying) 
        { 
            pressDingSource.clip = pressDingClip;
            pressDingSource.Play(); 
        } 
    }

    public void PompkinCackling() 
    { 
        if (!pompkinCacklingSource.isPlaying) 
        { 
            pompkinCacklingSource.clip = pompkinCacklingClip;
            pompkinCacklingSource.Play(); 
        } 
    }

    public void PompkinBoom() 
    { 
        if (!pompkinBoomSource.isPlaying) 
        { 
            pompkinBoomSource.clip = pompkinBoomClip;
            pompkinBoomSource.Play(); 
        } 
    }

    public void LevelWin() 
    { 
        if (!levelSource.isPlaying) 
        { 
            levelSource.clip = levelWinClip;
            levelSource.Play(); 
        } 
    }

    public void LevelLost() 
    { 
        if(!levelLostSoundPlayed)
        {
            if (!levelSource.isPlaying) 
            { 
                levelSource.clip = levelLostClip;
                levelSource.Play(); 
                levelLostSoundPlayed = true;
            } 
        }
    }
}
