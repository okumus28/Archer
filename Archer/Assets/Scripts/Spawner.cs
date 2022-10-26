using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public bool start = false;
    public List<Enemy> enemies;

    public float spawnTime;
    float _spawnTime;

    public List<Enemy> enemiesLevel;

    int changeLevel = 1;

    public List<Enemy> spawnerEnemy;
    Attribiutes atr;

    [SerializeField]int speed = 1;
    public Text speedText;

    void Start()
    {

        atr = GameObject.FindObjectOfType<Attribiutes>();
        _spawnTime = spawnTime;

        for (int i = 0; i < enemies.Count / 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int a = Random.Range(0, enemiesLevel.Count);

                enemiesLevel[a].level = changeLevel;
                enemiesLevel.Remove(enemiesLevel[a]);
            }
            changeLevel++;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].damage = (int)(enemies[i].GetComponent<BoxCollider2D>().size.x * 2) / 2 * enemies[i].level;
            enemies[i].exp = Random.Range(2, 5) * enemies[i].damage * enemies[i].level * 2;
            enemies[i].gold = enemies[i].damage * Random.Range(2, 4) * enemies[i].level * 3;
            enemies[i].healt = enemies[i].damage * 50 /** enemies[i].level*/;
            enemies[i].speed = Random.Range(50, 120);
        }

        SpawnerEnemies(1);

    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            return;
        }

        _spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0)
        {
            for (int i = 0; i < Random.Range(1, atr.level); i++)
            {
                int posY = Random.Range(-4, 2);
                int randEnemy = Random.Range(0, spawnerEnemy.Count);
                Enemy enemy = Instantiate(spawnerEnemy[randEnemy], new Vector3(11, transform.position.y + posY , 0), Quaternion.identity);
                enemy.transform.rotation = Quaternion.Euler(0,180,0);
                enemy.meshRenderer.sortingOrder = -posY;
            }
            spawnTime = 5 - (0.15f * atr.level);
            _spawnTime = spawnTime / speed;
        }
    }

    public void SpawnerEnemies(int level)
    {
        spawnerEnemy.Clear();
        for (int i = 0; i < enemies.Count; i++)
        {
            int x = enemies[i].level;
            if (x == level || x+1 == level || x-1 ==level)
            {
                spawnerEnemy.Add(enemies[i]);
            }
        }
    }

    public void SetStart()
    {
        start = true;
    }

    public void SpeedButton()
    {
        speed *= 2;
        if (speed > 8)
        {
            speed = 1;
        }

        speedText.text = "x" + speed;
    }
}
