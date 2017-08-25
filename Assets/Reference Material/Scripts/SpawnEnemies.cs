using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    public bool spawnMore;
    public GameObject bowEnemy;
    public GameObject starEnemy;
    public float spawnDelay;
    public float radius;
    public bool gameStarted;
    public float minSpawnDelay;
    public float spawnDelayChange;
    private float ninjaSpawnChance;
    public float ninjaChance;
    public float ninjaScoreNeeded;
    public TextMesh scoreText;

    public float score;
    // Use this for initialization
    void Start ()
    {
        spawnMore = false;
        gameStarted = false;
        ninjaSpawnChance = 0;
        score = 0;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update ()
    {
        if (spawnMore && gameStarted)
            StartCoroutine(spawnEnemy());
        if (score == ninjaScoreNeeded)
            ninjaSpawnChance = ninjaChance;
	}

    public void setScore(int change)
    {
        score += change;
        scoreText.text = "Score: " + score;
    }

    public IEnumerator spawnEnemy()
    {
        spawnMore = false;
        float angle = Random.Range(0, 1.0f)*Mathf.PI*2;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        float enemyType = Random.Range(0, 1.0f);
        if (score == ninjaScoreNeeded)
            enemyType = 0;
        if (enemyType < ninjaSpawnChance)
            Instantiate(starEnemy, new Vector3(x, -0.04f, z), Quaternion.identity);
        else
            Instantiate(bowEnemy, new Vector3(x, -0.04f, z), Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        spawnMore = true;
        if (spawnDelay > minSpawnDelay)
            spawnDelay -= spawnDelayChange;
    }
}
