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
        {
            if (Input.GetKeyDown(KeyCode.E))
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
        
}
