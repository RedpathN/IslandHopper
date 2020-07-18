using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.position = transform.position += new Vector3(0.0f, WaterRiseSpeed, 0.0f);

        GetComponent<Renderer>().sharedMaterial.SetFloat("_WaterHeight", transform.position.y);
        Terrain.GetComponent<Terrain>().materialTemplate.SetFloat("_WaterHeight", transform.position.y);
    }
}
