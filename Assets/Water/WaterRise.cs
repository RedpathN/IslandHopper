using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaterRise : MonoBehaviour
{

    public float WaterStartHeight;
    public float WaterEndHeight;
    public float RiseTimeMinutes;

    public GameObject Terrain;

    private float WaterRiseSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var framesPerSec = 60.0f;
        var secsPerMin = 60.0f;
        WaterRiseSpeed = (WaterEndHeight - WaterStartHeight) / (framesPerSec * secsPerMin * RiseTimeMinutes);

        transform.position = new Vector3(transform.position.x, WaterStartHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            transform.position = transform.position += new Vector3(0.0f, WaterRiseSpeed, 0.0f);
        }

        GetComponent<Renderer>().sharedMaterial.SetFloat("_WaterHeight", transform.position.y);

        if (Terrain != null)
        {
            Terrain.GetComponent<Terrain>().materialTemplate.SetFloat("_WaterHeight", transform.position.y);
        }
    }
}
