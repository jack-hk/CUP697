using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Unlockable robot parts are parts of the robot the player and enemy can equip and use.
    /// </summary>
    public class RobotPart : ScriptableObject, IIdentifiable
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Sprite PreviewSprite { get; private set; }
        public bool IsLockedForThePlayer { get; private set; }
    }
}
