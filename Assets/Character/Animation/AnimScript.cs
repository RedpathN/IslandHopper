using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{

    Animator AnimCtrl;

    // Start is called before the first frame update
    void Start()
    {
        AnimCtrl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //if movement key is pressed, swap state to running
        if (Input.GetKeyDown(KeyCode.W))
        {
            AnimCtrl.SetInteger("Condition", 1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            AnimCtrl.SetInteger("Condition", 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AnimCtrl.SetInteger("Condition", 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AnimCtrl.SetInteger("Condition", 1);
        }

        if (!Input.anyKey)
        {
            AnimCtrl.SetInteger("Condition", 0);
        }

    }
}
