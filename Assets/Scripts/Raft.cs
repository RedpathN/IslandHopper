using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raft : MonoBehaviour
{

    public GameObject RaftParent;
    private bool PlayerOnRaft = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            RaftParent.GetComponent<KeyPrompt>().Show();

            if (Input.GetKey(KeyCode.E))
            {
                nextLevel();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().onPlatform = false;
        }
    }

    void nextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Tutorial Level")
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (scene.name == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (scene.name == "Level 2")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
