using UnityEngine;
/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
*/
public abstract class Player : MonoBehaviour
{
    public Weapon EquipedWeapon;
    public Collider2D ReachRegion;
    public PlayerMovement.KeySets ControlSet;
    [SerializeField] protected bool blocking;
    public abstract void Punch();
    public abstract void Hit(string source = "Unknown");
}