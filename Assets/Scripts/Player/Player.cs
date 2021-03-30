using Extensions;
using UnityEngine;
/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
*/
[RequireComponent(typeof(PlayerMovement), typeof(AudioSource))]
public class Player : MonoBehaviour
{
    //public Weapon EquipedWeapon;
    public Collider2D ReachRegion;
    public bool Blocking { get; protected set; }
    public PlayerMovement.KeySets ControlSet;
    public PlayerMovement Movement;
    [Header("Audio")]
    [Tooltip("This is the audio source for playing sounds of punches and the like")]
    public AudioSource AudioSource;
    [Tooltip("This clip is played when the olayer punches")]
    public AudioClip PunchClip;
    [Tooltip("This clip is played whenever this player is blocked")]
    public AudioClip BlockClip;
    protected virtual void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        Movement = GetComponent<PlayerMovement>();
    }
    public virtual void Punch()
    {
        AudioSource.PlayOneShot(PunchClip);

    }
    public virtual void Hit(Player source = null)
    {
        Debug.Log($"{gameObject.name} was hit by {source.name}");


    }
    public virtual void OnBlocked()
    {
        AudioSource.PlayOneShot(BlockClip);

    }
    protected virtual void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x * Movement.savedDir, transform.localScale.y);
    }
}