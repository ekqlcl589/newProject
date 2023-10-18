using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetManager : MonoBehaviour
{
    private GameObject currentTarget;
    private List<GameObject> potentialTargets = new List<GameObject>();

    public void SetCurrentTarget(GameObject newTarget)
    {
        currentTarget = newTarget;
    }

    public void AddPotentialTarget(GameObject target)
    {
        if (!potentialTargets.Contains(target))
        {
            potentialTargets.Add(target);
        }
    }

    public void RemovePotentialTarget(GameObject target)
    {
        potentialTargets.Remove(target);
    }

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }
}
