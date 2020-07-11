using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] private float _secondsBetweenSpawns = 3f;
    [SerializeField] private EnemyMovement _enemyPrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private Transform _enemyParerntTransform;

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnemyMovement newEnemy = Instantiate(this._enemyPrefab, this._spawnLocation.position, Quaternion.identity);
            newEnemy.transform.parent = this._enemyParerntTransform;
            yield return new WaitForSeconds(this._secondsBetweenSpawns);
        }
    }
}
