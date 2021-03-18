using System.Linq;
using UnityEngine;
using System.Collections.Generic;
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
    private void Awake()
    {
        //TODO: Move to another place - F
        Controls.DefaultScheme = new ControlScheme(new Dictionary<string, string>(){
                {"PlayerOne-Forward", nameof(KeyCode.W)},
                {"PlayerOne-Back", nameof(KeyCode.S)},
                {"PlayerOne-Left", nameof(KeyCode.A)},
                {"PlayerOne-Right", nameof(KeyCode.D)},
                {"PlayerOne-Punch", nameof(KeyCode.Space)},
                {"PlayerOne-Block", nameof(KeyCode.V)},
                {"PlayerOne-Dash", nameof(KeyCode.LeftShift)},
                {"PlayerTwo-Forward", nameof(KeyCode.UpArrow)},
                {"PlayerTwo-Back", nameof(KeyCode.DownArrow)},
                {"PlayerTwo-Left", nameof(KeyCode.LeftArrow)},
                {"PlayerTwo-Right", nameof(KeyCode.RightArrow)},
                {"PlayerTwo-Punch", nameof(KeyCode.Return)},
                {"PlayerTwo-Block", nameof(KeyCode.RightShift)},
                {"PlayerTwo-Dash", nameof(KeyCode.LeftControl)},
            });
        /* 
            Sätter default schemet i Controlls bc Controls is in another assembly se paketet "se.princessapollo.ControlScheme"
            eller https://github.com/FredrikAlHam/UnityControlSchemeFromJson.
            Eftersom repon är public så finns alla dess kommentarer här. se.princessapollo.ControlScheme skapar en json file som ger keycodes
            namn, på runtime så skapar den antingen en ny efter DefaultScheme eller läser en från hårdisken från en fil som heter
            controls.ctrl i data foldern av spelet // Fredrik (Author of se.princessapollo.ControlScheme)
            P.S. Unity Package Manager är en ******
        */
    }
    public override void Hit(string source = "Unknown")
    {
        Debug.Log($"{gameObject.name} was hit by {source}");
        if (!blocking)
        {
            Debug.Log($"{gameObject.name} blocked a hit by {source}");
            this.Respawn();
        }
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
        if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey(ControlSet + "-Punch")))
        {
            Punch();
        }
        blocking = Input.GetKey(Controls.Scheme.GetCodeFromKey(ControlSet + "-Block"));

    }
}
