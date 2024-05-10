using UnityEngine;

namespace RoboStruct
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RobotPropeller", order = 1)]
    public class Propeller : RobotPart
    {
        [field: SerializeField] public float SpeedMultiplier { get; private set; } = 1;
    }
}
