using UnityEngine;
using System.Collections;

public class KeyPrompt : MonoBehaviour
{
    public GameObject keyPrompt;
    public bool StartActive = false;
    public float HideAfterSeconds = 0.05f;

    private float timeSinceShown;

    // Use this for initialization
    void Start()
    {
        keyPrompt = Instantiate(keyPrompt, transform);
        keyPrompt.SetActive(StartActive);
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
