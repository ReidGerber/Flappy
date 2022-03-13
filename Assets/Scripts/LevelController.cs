using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    string nextLevelName;

    Monster[] monsters;

    void OnEnable()
    {
        monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead())
        {
            GoToNextLevel();
        }

    }

    void GoToNextLevel()
    {
        //Debug.Log("all monsters are dead, go to level " + nextLevelName);
        SceneManager.LoadScene(nextLevelName);

    }

    bool MonstersAreAllDead()
    {
        foreach (var monster in monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
