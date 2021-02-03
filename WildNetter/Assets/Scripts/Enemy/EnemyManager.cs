using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public Habitat currentHabitat;
  
    public EnemyList _enemyList;
    public EnemySpawner _enemySpawner;
    [Tooltip("insert % between 0-100")]
    [Range(0, 100)]
    [SerializeField] float wildSpawnChance;

    public override void Init()
    {
 WildSpawnChance();
    }



    
    
        
    public void WildSpawnChance()
    {
        float random = Random.Range(0, 99);
        if(random < wildSpawnChance)
        {
            Debug.Log("grill won");
          GetBeastSettings(Difficulty.Easy, Size.Small, random,Vector3.zero,0);
        }
        else
        {

            Debug.Log("grill lost");
        }
    }
    public void SpawnEnemy(bool isBeast)
    {
        
        
    }
    public void GetBeastSettings(Difficulty enemyType,Size enemySize,float spawnRank,Vector3 worldSpanwPoint,float spawnDelay)
    {
        spawnRank = Mathf.Clamp(spawnRank,1, 100);
        float rankStep = 100 / _enemySpawner.EnemyDic.Count;
        
        
        for (int x = 0; x<= 100/rankStep; x++)
        {
         
            for (float y= x*rankStep; y < (x + 1 )* rankStep; y++)
            {
               
                if (y == spawnRank)
                {

                    if (spawnDelay <= 0)
                    {
                    _enemySpawner.SpawnBeast(x, worldSpanwPoint);
                    }
                    else
                    {
                        StartCoroutine(DelayedSpawn(x, worldSpanwPoint, spawnDelay));
                    }
                    
                   
                }
            }
        }
        

    }
    IEnumerator DelayedSpawn(int monsterQueue, Vector3 worldSpawnPoint, float timeToSpawn)
    {
        yield return new WaitForSeconds(timeToSpawn);
        _enemySpawner.SpawnBeast(monsterQueue, worldSpawnPoint);
        yield return null;
    }

}
