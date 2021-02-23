using UnityEngine;
/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
*/
public class Weapon : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D trigger) {
        if(trigger.gameObject.TryGetComponent<Player>(out Player p)){
            p.Hit();
        }
        
    }
}
