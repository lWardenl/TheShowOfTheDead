using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPref;
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPref[selectedCharacter];
        GameObject clone = Instantiate(prefab,spawnPoint.position, Quaternion.identity);
    }
}
