using UnityEngine;

public class VoidReset : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController1 player = other.GetComponent<PlayerController1>();

        if (player != null)
        {
            player.ResetToSpawn();
        }
    }
}
