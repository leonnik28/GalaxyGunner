using UnityEngine;

public class Wall : MonoBehaviour, IWall
{
    public bool IsWall()
    {
        return true;
    }
}
