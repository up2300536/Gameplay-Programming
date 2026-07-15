using UnityEditor.EditorTools;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController1>())
        {
            FindObjectOfType<GameManager>().SpawnNextRoom();
        }
    }
}
