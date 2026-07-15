using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Transform roomParent;
    public PlayerController1 player;

    public RoomFactory factory;

    private List<RoomType> roomPool = new List<RoomType>();
    private GameObject currentRoom;

    void Start()
    {
        BuildRoomPool();
        SpawnNextRoom();
    }

    void BuildRoomPool()
    {
        roomPool.Clear();

        foreach (RoomType type in System.Enum.GetValues(typeof(RoomType)))
        {
            roomPool.Add(type);
        }

        Shuffle(roomPool);
    }

    void Shuffle(List<RoomType> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public void SpawnNextRoom()
    {
        if (currentRoom != null)
            Destroy(currentRoom);

        if (roomPool.Count == 0)
            BuildRoomPool();

        RoomType nextRoomType = roomPool[0];
        roomPool.RemoveAt(0);

        GameObject prefab = factory.CreateRoom(nextRoomType);

        Debug.Log("Loaded prefab: " + prefab);

        if (prefab == null)
        {
            Debug.LogError("Prefab is NULL! Check RoomFactory assignments.");
            return;
        }

        currentRoom = Instantiate(prefab, Vector3.zero, Quaternion.identity, roomParent);

        TrafficLightRoom room = currentRoom.GetComponent<TrafficLightRoom>();

        if (room != null)
        {
            room.EnterRoom(player);
        }
        else
        {
            Debug.LogError("Room prefab missing TrafficLightRoom!");
        }
    }
}
