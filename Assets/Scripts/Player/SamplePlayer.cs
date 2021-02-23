using UnityEngine;
/*
    Written by F
    Licensed according to license https://static.princessapollo.se/licenses/mit-t.txt
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
