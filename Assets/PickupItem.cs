using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{ 

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
            
             gameObject.GetComponent<KeyPrompt>().Show();

        //if (this.gameObject.tag == "Platform") 
        //{
        //    //Debug.Log("On the platform");
        //    other.GetComponent<PlayerController>().onPlatform = true;
        //    if (Input.GetKey(KeyCode.Space))
        //    {
        //        if (other.GetComponent<PlayerController>().totalInventory > 0)
        //        {
        //            //Jukebox.Instance.PlaySFX("PutDownItem3");

        //            float woodCollected = other.gameObject.GetComponent<PlayerController>().woodInventory;
        //            gameObject.GetComponent<BoatPlatformController>().woodCollected += woodCollected;
        //            other.gameObject.GetComponent<PlayerController>().woodInventory = 0;

        //            float ropeCollected = other.gameObject.GetComponent<PlayerController>().ropeInventory;
        //            gameObject.GetComponent<BoatPlatformController>().ropeCollected += ropeCollected;
        //            other.gameObject.GetComponent<PlayerController>().ropeInventory = 0;

        //            float foodCollected = other.gameObject.GetComponent<PlayerController>().foodInventory;
        //            gameObject.GetComponent<BoatPlatformController>().foodCollected += foodCollected;
        //            other.gameObject.GetComponent<PlayerController>().foodInventory = 0;

        //            float clothCollected = other.gameObject.GetComponent<PlayerController>().clothInventory;
        //            gameObject.GetComponent<BoatPlatformController>().clothCollected += clothCollected;
        //            other.gameObject.GetComponent<PlayerController>().clothInventory = 0;
        //        }

        //    }

        //}

        {
            if (Input.GetKey(KeyCode.E))
            {

                if (this.gameObject.tag == "Food")
                {
                    other.gameObject.GetComponent<PlayerController>().foodInventory +=1;
                    Destroy(gameObject);

                }
                else if (this.gameObject.tag == "Wood")
                {
                    other.gameObject.GetComponent<PlayerController>().woodInventory++;
                    Destroy(gameObject);

                }
                else if (this.gameObject.tag == "Rope")
                {
                    other.gameObject.GetComponent<PlayerController>().ropeInventory+= 1;
                    Destroy(gameObject);

                }
                else if (this.gameObject.tag == "Cloth")
                {
                    other.gameObject.GetComponent<PlayerController>().clothInventory++;
                    Destroy(gameObject);

                }

                else if (this.gameObject.tag == "PowerUp")
                {
                    other.gameObject.GetComponent<PlayerController>().SpeedBoost = true;
                    Destroy(gameObject);

                }
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


}
