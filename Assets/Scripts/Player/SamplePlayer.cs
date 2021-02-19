using UnityEngine;
/*
    Written by F
    Licensed according to license in Assets/Licenses/Licence-T.txt
    ***
    Not meant to make it into any part of the final product
*/
public class SamplePlayer : Player
{
    public override void Hit()
    {
        Debug.Log("Ouch!");
    }
}
