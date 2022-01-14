using UnityEngine;

public enum EPointerType
{
    SmartPointer,
    ManualPointer
}
[System.Serializable]
public class PlayerOptionSettings:MonoBehaviour
{
    //Defualt type=SmartPointer
    public static EPointerType pointerType=EPointerType.SmartPointer;
}
