using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text enemiesLeftText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> enemies;
    int enemyCount;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        enemyCount = enemies.Count;

        enemiesLeftText.text = "Enemies left: " + enemyCount;

        if(enemies.Count == 0)
        {
            gameManager.NextLevel();
        }
    }
}
