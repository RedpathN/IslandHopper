using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Randomize : MonoBehaviour
{

    public bool IsRandomized = false;

    public List<GameObject> prefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (!IsRandomized) {
            var index = Random.Range(0, prefabs.Count);
            var tree = Instantiate(prefabs[index], transform);

            var scale = Random.Range(1f, 1.7f);
            tree.transform.localScale *= scale;
            tree.transform.Rotate(new Vector3(0, 0, 1), Random.Range(0f, 360f));

            IsRandomized = true;
        }
    }
}
