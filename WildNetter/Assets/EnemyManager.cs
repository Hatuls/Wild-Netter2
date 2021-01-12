using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Habitat currentHabitat;
    static EnemyManager _instance;
    public EnemyList _enemyList;
    public EnemySpawner _enemySpawner;
    



    public static EnemyManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new EnemyManager();

        }
        return _instance;
    }
    private void Awake()
    {
        _instance = this;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
    private void Start()
    {
        
    }
    
    
        
    
    public void SpawnEnemy(bool isBeast)
    {
        
        
    }
    public void GetBeastSettings(Difficulty enemyType,Size enemySize,float spawnRank)
    {
        spawnRank = Mathf.Clamp(spawnRank,1, 100);
        float rankStep = 100 / _enemySpawner.EnemyDic.Count;
        
        
        for (int x = 1; x>= 100/rankStep; x++)
        {

            for (float y= x*rankStep; y < x*rankStep+1; y++)
            {
                if (y == spawnRank)
                {
                    _enemySpawner.SpawnBeast(x);
                }
            }
        }
        

    }

}
