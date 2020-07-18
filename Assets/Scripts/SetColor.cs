using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject water;
    Renderer rend;
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();//.material.SetVector("_Color", new Vector4(0, 0.2f, 0.7f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
        {

        }
    }
}
