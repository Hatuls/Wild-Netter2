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
    [SerializeField]GameObject Player;
    [SerializeField]Camera mainCam;

    Dictionary<Vector3, GameObject> spawnQueue = new Dictionary<Vector3, GameObject>();
    Dictionary<Vector3, QueuedEnemy> queuedEnemies = new Dictionary<Vector3, QueuedEnemy>();
    List<Vector3> queuedPoints = new List<Vector3>();



    private void Awake()
    {
        _enemyManager = EnemyManager.GetInstance();
        _enemyManager._enemySpawner = this;




        SetSpawner();
    }
    void Start()
    {
    



    }
    public void Spawnmobs()
    {

    }
    public void SpawnBeast(int Rank,Vector3 WorldPosition)
    {
       
        

        if (CheckIfOnView(WorldPosition))
        {
            Instantiate(EnemyDic[spawnSequence[Rank]], WorldPosition, Quaternion.identity);
        }
        else
        {
            spawnQueue.Add(WorldPosition, EnemyDic[spawnSequence[Rank]]);
            queuedEnemies.Add(WorldPosition, new QueuedEnemy(WorldPosition, Quaternion.identity));
            queuedPoints.Add(WorldPosition);

           
            
        }

        

        
    }
    public bool CheckIfOnView(Vector3 position)
    {
        Vector3 screenPoint = mainCam.WorldToViewportPoint(position);

        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }



    private void FixedUpdate()
    {
        CheckQueuedEnemies();
    }
    public void CheckQueuedEnemies()
    {
        foreach(Vector3 position in queuedPoints)
        {
            if (CheckIfOnView(position))
            {
                Instantiate(spawnQueue[position], queuedEnemies[position].worldPosition, queuedEnemies[position].spawnQuaternion);
                queuedEnemies.Remove(position);
                spawnQueue.Remove(position);
                queuedPoints.Remove(position);
            }
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
public class QueuedEnemy
{
    public Vector3 worldPosition;
    public Quaternion spawnQuaternion;
    public QueuedEnemy(Vector3 _worldPosition, Quaternion _spawnQuaternion)
    {
        worldPosition = _worldPosition;
        spawnQuaternion = _spawnQuaternion;
        
    }
}
