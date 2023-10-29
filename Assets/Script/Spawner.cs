using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] destroyables;
    public float spawnTime = 2f;
    public float minObjectSpeed = 5f;
    public float maxObjectSpeed = 5f;
    public bool isWorking = true;
    public AudioSource spawnSource;
    public AudioClip spawnClip;
    GameObject instance;
    int currentSpawn = 0;

    IEnumerator Start ()
    {
        yield return StartCoroutine(Spawn());
    }

    void Update () {

    }
   
    IEnumerator Spawn ()
    {
        while(isWorking)
        {
            yield return new WaitForSeconds (spawnTime);
            
            if(currentSpawn != destroyables.Length)
            {
                instance = Instantiate(destroyables[currentSpawn], spawnPoints[0]);
                instance.GetComponentInChildren<Destroyable>().speed = Random.Range (minObjectSpeed, maxObjectSpeed);

                if (!spawnSource.isPlaying) 
                { 
                    spawnSource.clip = spawnClip;
                    spawnSource.Play(); 
                } 

                currentSpawn++;
            }            
        }
    }
}
