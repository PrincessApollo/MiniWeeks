using System.Collections.Generic;
using PrincessApollo.Controls;
using UnityEngine;
public class DefaultKeys
{
    [RuntimeInitializeOnLoadMethod]
    static void OnLoad()
    {
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
}