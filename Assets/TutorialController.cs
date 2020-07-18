using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{


    public GameObject player;
    public Text tutorialText;
    public bool isDead = false;
    public GameObject Water;

    public float tutorialStage = 0;
    private float Inventory = 0;
    public bool onPlatform = false;

    private void Start()
    {
        tutorialText.text = "Use Mouse to look around \nUse WASD to move \nPress E to pick up objects";
        Water.GetComponent<WaterRise>().IsRising = false;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (!isDead)
        {
            Inventory = player.GetComponent<PlayerController>().totalInventory;
            onPlatform = player.GetComponent<PlayerController>().onPlatform;

            if (Inventory >= 1 && tutorialStage == 0)
            {
                tutorialText.text = "Carry as many items as you can to the platform";
                tutorialStage = 1;
            }

            if (Inventory == 4 && tutorialStage == 1)
            {
                tutorialText.text = "Carrying too many objects will slow you down. Press space to drop an item";
                tutorialStage = 2;
            }

            if (tutorialStage == 2 && Input.GetKey(KeyCode.Space))
            {
                tutorialStage = 3;
                tutorialText.text = "Head to the platform!";
            }

            if (tutorialStage ==3 && onPlatform)
            {
                tutorialStage = 4;
                tutorialText.text = "Press Space while standing on the platform to stash your items";
            }

            if (tutorialStage == 4 && Input.GetKey(KeyCode.Space) && onPlatform)
            {
                tutorialStage = 5;
                tutorialText.text = "When you've collected enough items, you can set sail! Be quick, the water is rising";
                Water.GetComponent<WaterRise>().IsRising = true;

            }
        }
        

        //Death------------------------------------------------
        if (isDead)
        {
            tutorialText.text = "You died. Press R to restart";
         
        }
        // ---------------------------------------------------------------------


    }


    /*
      private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wood" || collision.gameObject.tag == "Cloth" || collision.gameObject.tag == "Rope" || collision.gameObject.tag == "Food")
        {
            if (tutorialStage == 0)
            {
                tutorialText.text = "Press E to pickup an item";

            }
        }
    } 
    */

}
