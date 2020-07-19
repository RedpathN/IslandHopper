using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Assign this to the Canvas, and call SetState() for every menu transition.
/// </summary>
public class ShowHideCanvas : MonoBehaviour
{
    [SerializeField]
    private string defaultState;

    void Awake()
    {
        SetState(defaultState);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Ask each UI object to show or hide according to the new state.
    /// </summary>
    /// <param name="state"></param>
    public void SetState(string state)
    {
        foreach (Transform child in gameObject.transform)
        {
            ShowHideUI showHideUI = child.gameObject.GetComponent<ShowHideUI>();
            if (showHideUI != null)
            {
                showHideUI.ShowOrHide(state);
            }
        }
    }
}
