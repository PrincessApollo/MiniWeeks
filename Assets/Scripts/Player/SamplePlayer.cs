using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
    ***
    Not meant to make it into any part of the final product
*/
public class SamplePlayer : Player
{
    //new ControllScheme();
    private void Start()
    {
        print(Controls.Scheme.keys[CtlrPrefix + "-Punch"]);
    }
    public override void Hit(string source = "Unknown")
    {
        Debug.Log($"{gameObject.name} was hit by {source}");
        if (!blocking)
            this.Respawn();
    }

    public override void Punch()
    {
        Collider2D[] results = new Collider2D[100];
        ReachRegion.OverlapCollider(new ContactFilter2D(), results);
        foreach (Collider2D item in results.Where(x => (x != null && x.gameObject.TryGetComponent<Player>(out Player p))))
        {
            if (item.gameObject.TryGetComponent<Player>(out Player p))
                p.Hit(gameObject.name);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), Controls.Scheme.keys[CtlrPrefix + "-Punch"])))
        {
            Punch();
        }
        blocking = Input.GetKey(KeyCode.Z);

    }
}
