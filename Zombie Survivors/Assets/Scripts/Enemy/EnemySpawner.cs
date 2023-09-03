using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    private float _timeUntilSpawn;

    private HealthController _healthController;

    void Awake()
    {
        SetTimeUntilSpawn();
        _healthController = GameObject.Find("Player").GetComponent<HealthController>();

        if(_healthController == null)
        {
            Debug.LogError("Health Controller in EnemySpawner is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;
        
        if(_healthController.GetCurrentHealth() == 0)
        {
            return;
        }
        else if(_timeUntilSpawn <= 0)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

}