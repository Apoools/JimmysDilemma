using UnityEngine;

public enum ChairType { Left, Right, Center }

public class ChairPoint : MonoBehaviour
{
    public Transform sitPoint;
    public ChairType chairType;
    public bool flipSprite = false;
}
