using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaterRise : MonoBehaviour
{
    public bool IsRising = true;
    public float WaterStartHeight;
    public float WaterEndHeight;
    public float RiseTimeMinutes;

    public Material NoCoastlineMaterial;

    public GameObject Terrain;

    private float timeSinceStart = 0;
    private float framesPerSec = 60.0f;
    private float secsPerMin = 60.0f;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(transform.position.x, WaterStartHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var waterRiseSpeed = (WaterEndHeight - WaterStartHeight) / (secsPerMin * RiseTimeMinutes);

        timeSinceStart += Time.deltaTime;

        if (Application.isPlaying && IsRising)
        {
            if (transform.position.y < WaterEndHeight)
            {
                transform.position = transform.position += new Vector3(0.0f, waterRiseSpeed, 0.0f) * Time.deltaTime;
            }
        }

        GetComponent<Renderer>().sharedMaterial.SetFloat("_WaterHeight", transform.position.y);
        NoCoastlineMaterial.SetFloat("_WaterHeight", transform.position.y);

        if (Terrain != null)
        {
            Terrain.GetComponent<Terrain>().materialTemplate.SetFloat("_WaterHeight", transform.position.y);
        }
    }
}
