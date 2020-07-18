using UnityEngine;
using System.Collections;

public class KeyPrompt : MonoBehaviour
{

    public float DisplayHeight;
    public GameObject keyPrompt;

    private float timeSinceShown;
    private float HideAfterSeconds = 0.05f;

    // Use this for initialization
    void Start()
    {
        keyPrompt = Instantiate(keyPrompt, transform);
        keyPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPrompt.activeSelf)
        {
            timeSinceShown += Time.deltaTime;

            if (timeSinceShown > HideAfterSeconds)
            {
                keyPrompt.SetActive(false);
                timeSinceShown = 0;
            }
        }
    }

    public void Show()
    {
        keyPrompt.SetActive(true);
        timeSinceShown = 0;
    }
}
