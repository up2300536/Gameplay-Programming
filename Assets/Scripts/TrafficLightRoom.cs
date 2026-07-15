using UnityEngine;

public class TrafficLightRoom : MonoBehaviour
{
    public Transform spawnPoint;
    public TrafficLightPuzzle trafficLightPuzzle;

    public void EnterRoom(PlayerController1 player)
    {
        player.spawnPosition = spawnPoint.position;
        player.ResetToSpawn();

        trafficLightPuzzle.SetPlayer(player);
    }
}
