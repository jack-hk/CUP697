using UnityEngine;

namespace RoboStruct
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RobotWeapon", order = 1)]
    public class Weapon : RobotPart
    {
        [field: SerializeField] public float Mass { get; private set; } = 1;
        [field: SerializeField] public bool LimitAngle { get; private set; } = false;
    }
}
