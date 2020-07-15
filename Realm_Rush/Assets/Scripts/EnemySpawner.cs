using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] private float _secondsBetweenSpawns = 3f;
    [SerializeField] private EnemyMovement _enemyPrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private Transform _enemyParerntTransform;
    [SerializeField] private Text _spawnedEnemies;
    [SerializeField] private AudioClip _spawnedEnemySfx;

    private int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.SpawnEnemy());
        this._spawnedEnemies.text = this._score.ToString();
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            this.AddScore();
            GetComponent<AudioSource>().PlayOneShot(this._spawnedEnemySfx);
            EnemyMovement newEnemy = Instantiate(this._enemyPrefab, this._spawnLocation.position, Quaternion.identity);
            newEnemy.transform.parent = this._enemyParerntTransform;
            yield return new WaitForSeconds(this._secondsBetweenSpawns);
        }
    }

    private void AddScore()
    {
        this._score++;
        this._spawnedEnemies.text = this._score.ToString();
    }
}
