using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Transform[] spawnPoints;
    GameObject[] objects;
    float minSpawnTime = 2f;
    float maxSpawnTime = 4f;
    float minObjectSpeed = 5f;
    float maxObjectSpeed = 5f;
    bool isWorking = true;
    int currentSpawnCount = 0;
    GameObject instance;
    public AudioSource spawnSource;
    public AudioClip spawnClip;


    void Update () {

        if(LevelSetup.instance.gameEnd == true)
        {
            isWorking = false;
        }

    }

    IEnumerator Start ()
    {
        minSpawnTime = LevelSetup.instance.minSpawnTime;
        maxSpawnTime = LevelSetup.instance.maxSpawnTime;
        minObjectSpeed = LevelSetup.instance.minObjectSpeed;
        maxObjectSpeed = LevelSetup.instance.maxObjectSpeed;
        spawnPoints = LevelSetup.instance.spawnPoints;
        objects = LevelSetup.instance.objects;

        yield return StartCoroutine(Spawn());
    }
   
    IEnumerator Spawn ()
    {
        while(isWorking)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            
            if(currentSpawnCount != objects.Length)
            {
                instance = Instantiate(objects[currentSpawnCount], spawnPoints[0]);
                instance.GetComponentInChildren<Destroyable>().speed = Random.Range(minObjectSpeed, maxObjectSpeed);

                if (!spawnSource.isPlaying) 
                { 
                    spawnSource.clip = spawnClip;
                    spawnSource.Play(); 
                } 

                currentSpawnCount++;
            }            
        }
    }
}
