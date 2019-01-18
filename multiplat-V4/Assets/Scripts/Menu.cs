using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string lvlToStart;
    // Update is called once per frame
    void Update()
    {
        if (InputManager.Gass())
        {
            SceneManager.LoadScene(lvlToStart);
        }
    }
}
