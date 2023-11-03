using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyable : MonoBehaviour
{
    public bool isEnemy = false;
    public bool isStart = false;
    public float speed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speed, 0, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        if(isEnemy)
        {
            if(other.gameObject.layer == 6)
            {
                PressController.instance.ParticlePlay(GetComponentInChildren<PumpkinVisualRandomizer>());
                PressController.instance.PompkinCackling();
                PressController.instance.PompkinBoom();
                PressController.instance.pompkinSquashed +=1;
                Object.Destroy(transform.parent.gameObject);
            } else if(other.gameObject.layer == 8)
            {
                Object.Destroy(transform.parent.gameObject);
            }
        } else
        {
            if(other.gameObject.layer == 6)
            {
                PressController.instance.ParticlePlay(GetComponentInChildren<PumpkinVisualRandomizer>());
                PressController.instance.PompkinScream();
                PressController.instance.PompkinSmash();
                PressController.instance.pompkinSquashed +=1;
                Object.Destroy(transform.parent.gameObject);
                if(isStart)
                {
                    LevelSetupMenu.instance.smashed = true;
                }
            } else if(other.gameObject.layer == 8)
            {
                Object.Destroy(transform.parent.gameObject);
            }
        }
    }
}
