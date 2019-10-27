using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilEnemyController : MonoBehaviour
{
    private TargetCollection tc;
    private void Start()
    {
        tc = FindObjectOfType<TargetCollection>();
    }

}
