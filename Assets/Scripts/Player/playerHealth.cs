using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    private float timeOut;
    // Start is called before the first frame update

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (timeOut > 0) { timeOut -= Time.deltaTime; }
        else if(timeOut < 0) { timeOut = 0; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack" && timeOut == 0)
        {
            timeOut = 1f;   
            print("Collided");
            healthBar.fillAmount -= 0.2f;

            currentHealth -= 20;
            if(currentHealth < 0)
            {
                healthBar.fillAmount = 0;
                currentHealth = 0;
                gameObject.SetActive(false);
                Invoke(nameof(Reset), 0.1f);
            }
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
