using System.Linq;
using UnityEngine;
using PrincessApollo.Controls;
using Extensions;

/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
    ***
    Not meant to make it into any part of the final product
*/
public class SamplePlayer : Player
{
    public override void Hit(Player source = null)
    {
        if (!Blocking)
        {

            Debug.Log($"{gameObject.name} blocked a hit by {source.name}");
            this.Respawn();
            base.Hit(source);
        }
        else
        {
            OnBlocked();
        }
    }

    public override void Punch()
    {
        Collider2D[] results = new Collider2D[100];
        ReachRegion.OverlapCollider(new ContactFilter2D(), results);
        foreach (Collider2D pCollider in results.Where(x => (x != null && x.gameObject.TryGetComponent<Player>(out Player p))))
        {
            if (pCollider.gameObject.TryGetComponent<Player>(out Player p))
                foreach (Collider2D rCollider in results.Where(x => (x != null && x.gameObject.TryGetComponent<ReachRegion>(out ReachRegion r))))
                {
                    if (rCollider.gameObject.TryGetComponent<ReachRegion>(out ReachRegion r) && p.Blocking)
                        p.Hit(this);
                }
        }
        base.Punch();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey(ControlSet + "-Punch")))
        {
            Punch();
        }
        Blocking = Input.GetKey(Controls.Scheme.GetCodeFromKey(ControlSet + "-Block"));
    }
}
