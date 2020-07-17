using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag != "Player")
        {
            return;
        }

       if(collider.tag == "Player")
        {
            
        }

    }



    

}
