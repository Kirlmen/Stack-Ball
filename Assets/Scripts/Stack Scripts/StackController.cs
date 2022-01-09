using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{

    [SerializeField] private StackPartController[] stackPartControllers = null;

    public void ShatterAllParts()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            FindObjectOfType<PlayerHandler>().IncreaseBrokenStacks();
        }

        foreach (StackPartController obj in stackPartControllers)
        {
            obj.Shatter();
        }
        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
