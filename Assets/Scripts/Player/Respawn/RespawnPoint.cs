using System.Linq;
using UnityEngine;
/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
*/
public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Player p;
    //Gets point corresponding to a player, only picks first result if many exist
    public static RespawnPoint GetPoint(Player player) => FindObjectsOfType<RespawnPoint>().Where(RespawnPoint => RespawnPoint.p == player).First(); 
}
public static class PlayerRespawnExtension
{
    public static void Respawn(this Player p)
    {
        Debug.Log($"Player {p} respawned at {RespawnPoint.GetPoint(p)} at location {RespawnPoint.GetPoint(p).transform.position}");
        p.transform.position = RespawnPoint.GetPoint(p).transform.position;
    }
}