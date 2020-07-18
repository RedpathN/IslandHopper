using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Randomize : MonoBehaviour
{
    public bool RotateAroundYAxis = true;
    public bool IsRandomized = false;
    public bool ScaleAxesSeparately = false;

    public Vector2 XScaleRange = new Vector2(1, 2);
    public Vector2 YScaleRange = new Vector2(1, 2);
    public Vector2 ZScaleRange = new Vector2(1, 2);

    public List<GameObject> prefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (!IsRandomized) {
            var index = Random.Range(0, prefabs.Count);
            var subObject = Instantiate(prefabs[index], transform);

            var xscale = Random.Range(XScaleRange.x, XScaleRange.y);
            var yscale = Random.Range(YScaleRange.x, YScaleRange.y);
            var zscale = Random.Range(ZScaleRange.x, ZScaleRange.y);
            Vector3 scaleVector;
            if (ScaleAxesSeparately)
            {
                scaleVector = new Vector3(xscale, yscale, zscale);
            }
            else
            {
                scaleVector = new Vector3(xscale, xscale, xscale);
            }
            subObject.transform.localScale = new Vector3(subObject.transform.localScale.x * scaleVector.x, subObject.transform.localScale.y * scaleVector.y, subObject.transform.localScale.z * scaleVector.z);

            // Quaternius assets are rotated around Z axis...
            var rotateAxis = new Vector3(0, 0, 1);
            if (RotateAroundYAxis)
            {
                rotateAxis = new Vector3(0, 1, 0);
            }

            subObject.transform.Rotate(rotateAxis, Random.Range(0f, 360f));

            IsRandomized = true;
        }
    }
}
