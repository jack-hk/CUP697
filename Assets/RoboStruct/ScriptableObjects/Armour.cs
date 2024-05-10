using UnityEngine;

namespace RoboStruct
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RobotArmour", order = 1)]
    public class Armour : RobotPart
    {
        [field: SerializeField] public float HitPoints { get; private set; } = 200;
        [field: SerializeField] public float ForceMultiplier { get; private set; } = 1;

    }
}
