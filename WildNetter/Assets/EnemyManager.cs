using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Habitat currentHabitat;
    static EnemyManager _instance;
    public EnemyList _enemyList;



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
    private void Start()
    {
        
    }
    public void SpawnEnemy()
    {
        
    }

}
