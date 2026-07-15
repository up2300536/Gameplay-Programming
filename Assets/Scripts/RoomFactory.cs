using UnityEngine;

public class RoomFactory : MonoBehaviour
{
    public GameObject standard;
    public GameObject easyParkour;
    public GameObject hardParkour;
    public GameObject maze1;
    public GameObject maze2;
    public GameObject parkourMaze1;
    public GameObject parkourMaze2;

    public GameObject CreateRoom(RoomType type)
    {
        switch (type)
        {
            case RoomType.Standard: return standard;
            case RoomType.EasyParkour: return easyParkour;
            case RoomType.HardParkour: return hardParkour;
            case RoomType.Maze1: return maze1;
            case RoomType.Maze2: return maze2;
            case RoomType.ParkourMaze1: return parkourMaze1;
            case RoomType.ParkourMaze2: return parkourMaze2;
        }

        return null;
    }
}
