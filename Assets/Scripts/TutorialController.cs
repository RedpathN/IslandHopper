using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public float levelNumber = 0;
    public GameObject player;
    public Text tutorialText;
    public bool isDead = false;
    public GameObject Water;
    public GameObject playerSphere;

    public float tutorialStage = 0;
    private float Inventory = 0;
    public bool onPlatform = false;
    private float initSpeed;
    private float timeSinceShown = 0;

    private void Start()
    {
        tutorialText.text = "Use Mouse to look around \nUse WASD to move \nPress E to pick up objects";
        Water.GetComponent<WaterRise>().IsRising = false;
        initSpeed = player.GetComponent<PlayerController>().maxSpeed;
        tutorialText.text = "";

    }
    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            if (levelNumber == 0)
            {
                Inventory = player.GetComponent<PlayerController>().totalInventory;
                onPlatform = player.GetComponent<PlayerController>().onPlatform;

                if (Inventory >= 1 && tutorialStage == 0)
                {
                    tutorialText.text = "Carry as many items as you can to the platform";
                    tutorialStage = 1;
                }

                if (Inventory >= 4 && tutorialStage == 1)
                {
                    tutorialText.text = "Carrying too many objects will slow you down. Press space to drop an item";
                    tutorialStage = 2;


                }

                if (tutorialStage == 2)
                {
                    playerSphere.GetComponent<KeyPrompt>().Show();

                }

                if (tutorialStage == 2 && Input.GetKey(KeyCode.Space))
                {
                    tutorialStage = 3;
                    tutorialText.text = "Head to the platform!";
                }

                if (tutorialStage == 3 && onPlatform)
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

            if(levelNumber == 1)
            {
                Water.GetComponent<WaterRise>().IsRising = true;
                Inventory = player.GetComponent<PlayerController>().totalInventory;
                onPlatform = player.GetComponent<PlayerController>().onPlatform;
                

                if (player.GetComponent<PlayerController>().maxSpeed > initSpeed && tutorialStage == 0)
                {
                    tutorialText.text = "Picking these up will increase your speed";
                    tutorialStage = 1;
                }

                if (Inventory >= 1 && tutorialStage == 1)
                {
                    tutorialText.text = "Head to the platform, it will always be on the highest point of the island";
                    tutorialStage = 2;
                    timeSinceShown = 0;
                }

                if (tutorialStage == 2)
                {
                    timeSinceShown += Time.deltaTime;
                    Debug.Log(timeSinceShown);
                    if (timeSinceShown >= 7f)
                    {
                        tutorialText.text = "";
                        tutorialStage = 3;
                        
                    }

                }

            }
        }
        



    }



}
