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
        platformText.text = "Drop Items Here! \n \nItems Collected \n" + DisplayText("Wood", woodCollected, woodNeeded) + DisplayText("Rope", ropeCollected, ropeNeeded) + DisplayText("Cloth", clothCollected, clothNeeded) + DisplayText("Food", foodCollected, foodNeeded);
        if (woodCollected >= woodNeeded && ropeCollected >= ropeNeeded && clothCollected >= clothNeeded && foodCollected >= foodNeeded) 
        {
            collectedAll = true;
        }

        if (collectedAll)
        {
            platformText.text = "Ready to set sail!";
            if (!fireworkActivated)
            {
                Instantiate(fireworks, this.transform.position, this.transform.rotation);
                fireworkActivated = true;
                Invoke("nextLevel", 5f);
                
            } 
        }
    }

    private string DisplayText(string name, float collected, float needed)
    {
        string allText = name + ": " + collected.ToString() + "/" + needed.ToString() + "\n";
        return allText;
    }

    void nextLevel() {
        SceneManager.LoadScene("Small Demo 2");
    }

}
