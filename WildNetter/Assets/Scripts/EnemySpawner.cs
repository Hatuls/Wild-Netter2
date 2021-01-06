using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum EnemyType {Hobogoblin,DireWolf,Deathdog };
public class EnemySpawner : MonoBehaviour
{
    public Habitat currentHabitat;
    EnemyManager _enemyManager;
    public Transform[] spawnPoints;
    public Dictionary<EnemyType, GameObject> EnemyDic= new Dictionary<EnemyType, GameObject>();
    [SerializeField] List<EnemyType> spawnSequence;
    EnemyType typeSelected;
    

    private void Awake()
    {
       
    }
    void Start()
    {
        _enemyManager = EnemyManager.GetInstance();
        SetSpawner();
       

    
    }
    public void Spawnmobs()
    {

    }
    public void SpawnBeast()
    {


        for (int i = 0; i < spawnSequence.Count; i++)
        {
            Instantiate(EnemyDic[spawnSequence[i]], spawnPoints[i].position, Quaternion.identity);
        }
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBeast();
            
        }   
    }
    public void SetSpawner()
    {
        currentHabitat = _enemyManager.currentHabitat;

        foreach (GameObject found in _enemyManager._enemyList.EnemyPrefabs)
        {
            Enemy LoadedEnemy;

            LoadedEnemy = found.GetComponent<Enemy>();
            for (int i = 0; i < LoadedEnemy._enemySO.habitats.Length; i++)
            {
                if (LoadedEnemy._enemySO.habitats[i] == currentHabitat)
                {
                    EnemyDic.Add(LoadedEnemy.enemytype, found);
                    spawnSequence.Add(LoadedEnemy.enemytype);

                }
            }



        }
    }
}
