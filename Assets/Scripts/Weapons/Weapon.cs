using UnityEngine;
/*
    Written by F
    Licensed according to license in Assets/Licenses/Licence-T.txt
*/
public class Weapon : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D trigger) {
        if(trigger.gameObject.TryGetComponent<Player>(out Player p)){
            p.Hit();
        }
        
    }
}
