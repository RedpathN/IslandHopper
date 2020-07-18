using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampPlatformText : MonoBehaviour
{
    public Text UIText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 UIpos = this.transform.position;
        UIText.transform.position = UIpos;
        UIText.transform.rotation = this.transform.rotation;
    }
}
