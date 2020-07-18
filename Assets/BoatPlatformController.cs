using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        if (woodCollected >= woodNeeded && ropeCollected >= ropeNeeded && clothCollected >= clothNeeded && foodCollected >= foodNeeded) 
        {
            collectedAll = true;
        }
    }
}
