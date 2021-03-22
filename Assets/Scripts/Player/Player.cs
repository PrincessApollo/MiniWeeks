using System.Linq;
using Extensions;
using PrincessApollo.Controls;
using UnityEngine;
/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
*/
public class Player : MonoBehaviour
{
    [Header("Hits")]
    public Weapon EquipedWeapon;
    [SerializeField] protected bool blocking;
    public Collider2D ReachRegion;
    [Header("Audio")]
    public AudioClip ParryAudioClip;
    public AudioClip PunchAudioClip;
    [Header("Controls")]
    public PlayerMovement.KeySets ControlSet;
    AudioSource audioSource;
    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public virtual void Punch()
    {
        Collider2D[] results = new Collider2D[100];
        ReachRegion.OverlapCollider(new ContactFilter2D(), results);
        foreach (Collider2D item in results.Where(x => (x != null && x.gameObject.TryGetComponent<Player>(out Player p))))
        {
            if (item.gameObject.TryGetComponent<Player>(out Player p))
                p.Hit(gameObject.name);
        }
    }
    public virtual void Hit(string source = "Unknown")
    {
        Debug.Log($"{gameObject.name} was hit by {source}");
        if (!blocking)
        {
            audioSource.PlayOneShot(PunchAudioClip);
            this.Respawn();
        }
        else
        {
            this.BlockedHit(source);
        }
    }
    public virtual void BlockedHit(string source = "Unknown")
    {
        Debug.Log($"{gameObject.name} blocked a hit by {source}");
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey(ControlSet + "-Punch")))
        {
            Punch();
        }
        blocking = Input.GetKey(Controls.Scheme.GetCodeFromKey(ControlSet + "-Block"));
    }
}