using UnityEngine;

public enum EPointerType
{
    SmartPointer,
    ManualPointer
}
public class PlayerSettings
{
    //Defualt type=SmartPointer
    public static EPointerType pointerType=EPointerType.SmartPointer;
}
