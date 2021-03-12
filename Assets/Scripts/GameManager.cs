using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject PlayerPrefab;
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    int _score = 0;
    public int score

    {
        get { return _score; }
        set
        {
            _score = value;
            Debug.Log("Current Score is " + _score);
        }
    }

    public int maxLives = 3;
    int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {

                //respawn code
            }
            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives <= 0)
            {

                SceneManager.LoadScene("Game Over");
                score = 0;
                lives = maxLives;
            }
            Debug.Log("Current Lives are " + _lives);
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level")
            {
                SceneManager.LoadScene("Title Screen");
            }
            else if (SceneManager.GetActiveScene().name == "Title Screen")
            {
                SceneManager.LoadScene("Level");
            }
            else if (SceneManager.GetActiveScene().name == "Game Over")
            {
                SceneManager.LoadScene("Title Screen");
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
