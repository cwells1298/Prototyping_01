using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public AudioSource bm;
    // Start is called before the first frame update
    void Start()
    {
        bm.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
