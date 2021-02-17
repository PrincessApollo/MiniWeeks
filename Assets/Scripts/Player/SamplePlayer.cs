using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayer : Player
{
    public override void Hit()
    {
        Debug.Log("Ouch!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
