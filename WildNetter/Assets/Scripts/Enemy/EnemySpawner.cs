using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum EnemyType {Hobogoblin,DireWolf,Deathdog };
public class EnemySpawner : MonoSingleton<EnemySpawner>
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
    [SerializeField]float distance;



    
    public override void Init()
    {
        _enemyManager = EnemyManager._Instance;
        _enemyManager._enemySpawner = this;




        SetSpawner();
       
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
    public bool CheckDistance(Vector3 position)
    {
        float Offset=0;
        if (Player.transform.position.x > position.x)
        {
            Offset += 5;
        }
        return Vector3.Distance(position, Player.transform.position)<distance+Offset;
    }


    private void FixedUpdate()
    {
      
        CheckQueuedEnemies();
    }
    public void CheckQueuedEnemies()
    {
        if (queuedPoints == null || queuedPoints.Count ==0)
            return;
      
        foreach (Vector3 position in queuedPoints)
        {
            
            if (CheckDistance(position))
            {
                Debug.Log("onView");
                if (spawnQueue != null&& queuedEnemies!= null)
                {

                Instantiate(spawnQueue[position], queuedEnemies[position].worldPosition, queuedEnemies[position].spawnQuaternion);
                if (queuedEnemies.ContainsKey(position))
                 queuedEnemies.Remove(position);

                if (spawnQueue.ContainsKey(position))
                  spawnQueue.Remove(position);

                if (queuedPoints.Contains(position))
                   queuedPoints.Remove(position);
                }
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

    private void OnDrawGizmos()
    {
        
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

