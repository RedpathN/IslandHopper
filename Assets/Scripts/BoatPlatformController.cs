using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoatPlatformController : MonoBehaviour
{
    public float woodCollected = 0;
    public float ropeCollected = 0;
    public float clothCollected = 0;
    public float foodCollected = 0;

    public float woodNeeded = 5;
    public float ropeNeeded = 5;
    public float clothNeeded = 5;
    public float foodNeeded = 5;

    public bool collectedAll = false;
    private bool fireworkActivated = false;

    public Text platformText;
    public ParticleSystem fireworks;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Application.Quit();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        platformText.text = "Drop Items Here! \n \nItems Collected \n" + DisplayText("Wood", woodCollected, woodNeeded) + DisplayText("Rope", ropeCollected, ropeNeeded) + DisplayText("Cloth", clothCollected, clothNeeded) + DisplayText("Food", foodCollected, foodNeeded);
        if (woodCollected >= woodNeeded && ropeCollected >= ropeNeeded && clothCollected >= clothNeeded && foodCollected >= foodNeeded) 
        {
            collectedAll = true;
        }

        if (collectedAll)
        {
            platformText.text = "Ready to set sail! \n\n\n" + DisplayText("Wood", woodCollected, woodNeeded) + DisplayText("Rope", ropeCollected, ropeNeeded) + DisplayText("Cloth", clothCollected, clothNeeded) + DisplayText("Food", foodCollected, foodNeeded); ;
            if (!fireworkActivated)
            {
                Jukebox.Instance.PlaySFX("Victory");
                Instantiate(fireworks, this.transform.position, this.transform.rotation);
                fireworkActivated = true;
                
                
            } 
        }
    }

    private string DisplayText(string name, float collected, float needed)
    {
        string allText = name + ": " + collected.ToString() + "/" + needed.ToString() + "\n";
        if (collected >= needed)
        {
            allText = "<color=green>" + allText + "</color>";
        }
        return allText;
    }

    void nextLevel() {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Tutorial Level")
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
