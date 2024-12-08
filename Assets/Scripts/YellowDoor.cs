using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class YellowDoor : MonoBehaviour
{
    string tagToCheck = "Target";

    private void Start()
    {
        InvokeRepeating("CheckAndDestroy", 0f, 1f);
    }

    private void CheckAndDestroy()
    {
        if (GameObject.FindGameObjectsWithTag(tagToCheck).Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
