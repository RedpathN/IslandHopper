using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text platformText;

    private void Update()
    {
        platformText.text = "Items Collected \n\n" + DisplayText("Wood", woodCollected, woodNeeded) + DisplayText("Rope", ropeCollected, ropeNeeded) + DisplayText("Cloth", clothCollected, clothNeeded) + DisplayText("Food", foodCollected, foodNeeded);
        if (woodCollected >= woodNeeded && ropeCollected >= ropeNeeded && clothCollected >= clothNeeded && foodCollected >= foodNeeded) 
        {
            collectedAll = true;
        }

        if (collectedAll)
        {
            platformText.text = "Ready to set sail!";
        }
    }

    private string DisplayText(string name, float collected, float needed)
    {
        string allText = name + ": " + collected.ToString() + "/" + needed.ToString() + "\n";
        return allText;
    }
}
