using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoboStruct
{
    /// <summary>
    /// Handles the game data such as weapons, armours, propellers.
    /// </summary>
    public class GameData : MonoBehaviourSingletonPersistent<GameData>
    {
        [field: SerializeField] public Weapon[] LockedWeapons { get; private set; }
        [field: SerializeField] public Armour[] LockedArmours { get; private set; }
        [field: SerializeField] public Propeller[] LockedPropellers { get; private set; }
    }
}
