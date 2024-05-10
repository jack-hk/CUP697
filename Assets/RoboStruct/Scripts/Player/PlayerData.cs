using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Persistent singleton that stores and handles player data between scenes.
    /// </summary>
    public class PlayerData : MonoBehaviourSingletonPersistent<PlayerData>
    {
        [field: SerializeField] public List<Weapon> UnlockedWeapons { get; private set; }
        [field: SerializeField] public List<Armour> UnlockedArmours { get; private set; }
        [field: SerializeField] public List<Propeller> UnlockedPropellers { get; private set; }
        [field: SerializeField] public Weapon EquippedWeapon { get; set; }
        [field: SerializeField] public Armour EquippedArmour { get; set; }
        [field: SerializeField] public Propeller EquippedPropeller { get; set; }
        public int CurrentLevelProgress { get; set; } = 1;
        public float WaveDifficultyScale { get; private set; }
        public ListExtensions<Weapon> WeaponCycler { get; private set; }
        public ListExtensions<Armour> ArmourCycler { get; private set; }
        public ListExtensions<Propeller> PropellerCycler { get; private set; }

        private void Start()
        {
            UnlockAndEquipDefaults();
            WeaponCycler = new ListExtensions<Weapon>(UnlockedWeapons);
            ArmourCycler = new ListExtensions<Armour>(UnlockedArmours);
            PropellerCycler = new ListExtensions<Propeller>(UnlockedPropellers);
        }

        public void UnlockItem<T>(T item, List<T> unlockedItems, T[] lockedItems) where T : IIdentifiable
        {
            T found = Array.Find(lockedItems, obj => obj.ID.Equals(item.ID));
            if (found != null)
            {
                if (unlockedItems.Exists(obj => obj.ID.Equals(found.ID)))
                {
                    Debug.Log($"Error: {typeof(T).Name} already unlocked: " + item);
                    return;
                }
                unlockedItems.Add(found);
            }
            else Debug.Log($"Error: could not find {typeof(T).Name}: " + item);
        }

        public void SubtractWaveDifficultyScale(float b)
        {
            WaveDifficultyScale -= b;
        }

        public void AdvanceWaveDifficultyScale()
        {
            WaveDifficultyScale++;
        }

        public void ClearData()
        {
            CurrentLevelProgress = 1;
            WaveDifficultyScale = 0;
            UnlockedWeapons.Clear();
            UnlockedArmours.Clear();
            UnlockedPropellers.Clear();
            UnlockAndEquipDefaults();
        }

        public void EquipNewSwitchedItems()
        {
            EquippedWeapon = WeaponCycler.CurrentItem; 
            EquippedArmour = ArmourCycler.CurrentItem; 
            EquippedPropeller = PropellerCycler.CurrentItem; 
        }

        private void UnlockAndEquipDefaults()
        {
            UnlockItem(GameData.Instance.LockedWeapons[0], UnlockedWeapons, GameData.Instance.LockedWeapons);
            UnlockItem(GameData.Instance.LockedArmours[0], UnlockedArmours, GameData.Instance.LockedArmours);
            UnlockItem(GameData.Instance.LockedPropellers[0], UnlockedPropellers, GameData.Instance.LockedPropellers);
            EquippedWeapon = UnlockedWeapons[0];
            EquippedArmour = UnlockedArmours[0];
            EquippedPropeller = UnlockedPropellers[0];
        }
    }

    public interface IIdentifiable
    {
        public int ID { get; }
    }
}