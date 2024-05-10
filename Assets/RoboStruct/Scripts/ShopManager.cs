using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoboStruct
{
    /// <summary>
    /// Persistent singleton that handles shop functions that need to be persistent.
    /// </summary>
    public class ShopManager : MonoBehaviourSingleton<ShopManager>
    {
        [SerializeField] private Image ShopItem1;
        [SerializeField] private Image ShopItem2;
        [SerializeField] private Image ShopItem3;
        [SerializeField] private TextMeshProUGUI ShopItem1Text;
        [SerializeField] private TextMeshProUGUI ShopItem2Text;
        [SerializeField] private TextMeshProUGUI ShopItem3Text;

        [field: SerializeField] public Sprite LockedPartUI { get; private set; }

        [field: Header("Weapon UI")]
        [field: SerializeField] public Image CurrentWeaponUI { get; private set; }
        [field: SerializeField] public Image NextWeaponUI { get; private set; }
        [field: SerializeField] public Image PreviousWeaponUI { get; private set; }

        [field: Header("Armour UI")]
        [field: SerializeField] public Image CurrentArmourUI { get; private set; }
        [field: SerializeField] public Image NextArmourUI { get; private set; }
        [field: SerializeField] public Image PreviousArmourUI { get; private set; }

        [field: Header("Propeller UI")]
        [field: SerializeField] public Image CurrentPropellerUI { get; private set; }
        [field: SerializeField] public Image NextPropellerUI { get; private set; }
        [field: SerializeField] public Image PreviousPropellerUI { get; private set; }

        public List<Tuple<int, int>> pickedItems { get; private set; }

        public Action OnSwitchItem;
        public Action OnPickedShopItem;

        private void Start()
        {
            OnSwitchItem += UpdateItemsMenu;
            OnPickedShopItem += DisableItems;

            pickedItems = RandomiseShopItems();
            AssignShopItem1();
            AssignShopItem2();
            AssignShopItem3();
        }

        public void UpdateItemsMenu()
        {
            UpdateCurrentItems();
            UpdateNextItems();
            UpdatePreviousItems();
        }

        public List<Tuple<int, int>> RandomiseShopItems()
        {
            List<Tuple<int, int>> pickedStuffList = new List<Tuple<int, int>>();
            HashSet<Tuple<int, int>> pickedStuffSet = new HashSet<Tuple<int, int>>();

            int maxAttempts = 100;
            int attempts = 0;
            while (pickedStuffList.Count < 3 && attempts < maxAttempts)
            {
                int randomList = UnityEngine.Random.Range(1, 4);
                int randomItem = 0;
                switch (randomList)
                {
                    case 1:
                        randomItem = UnityEngine.Random.Range(0, GameData.Instance.LockedWeapons.Length);
                        break;
                    case 2:
                        randomItem = UnityEngine.Random.Range(0, GameData.Instance.LockedArmours.Length);
                        break;
                    case 3:
                        randomItem = UnityEngine.Random.Range(0, GameData.Instance.LockedPropellers.Length);
                        break;
                }
                Tuple<int, int> pickedStuff = new Tuple<int, int>(randomList, randomItem);

                if (!pickedStuffSet.Contains(pickedStuff))
                {
                    switch (pickedStuff.Item1)
                    {
                        case 1:
                            if (!PlayerData.Instance.UnlockedWeapons.Any(item => item == GameData.Instance.LockedWeapons[pickedStuff.Item2]))
                                AddUniqueItem(pickedStuffList, pickedStuffSet, pickedStuff);
                            break;
                        case 2:
                            if (!PlayerData.Instance.UnlockedArmours.Any(item => item == GameData.Instance.LockedArmours[pickedStuff.Item2]))
                                AddUniqueItem(pickedStuffList, pickedStuffSet, pickedStuff);
                            break;
                        case 3:
                            if (!PlayerData.Instance.UnlockedPropellers.Any(item => item == GameData.Instance.LockedPropellers[pickedStuff.Item2]))
                                AddUniqueItem(pickedStuffList, pickedStuffSet, pickedStuff);
                            break;
                    }
                }
                attempts++;
            }
            return pickedStuffList;
        }

        private void AddUniqueItem(List<Tuple<int, int>> pickedStuffList, HashSet<Tuple<int, int>> pickedStuffSet, Tuple<int, int> pickedStuff)
        {
            pickedStuffList.Add(pickedStuff);
            pickedStuffSet.Add(pickedStuff);
        }

        //NOTE: Unity Events do not support parameters like enums. So I have to manually create individual public methods for each cycler function...
        public void PickShopItem1()
        {
            switch (pickedItems[0].Item1)
            {
                case 1:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedWeapons[pickedItems[0].Item2], PlayerData.Instance.UnlockedWeapons, GameData.Instance.LockedWeapons);
                    break;
                case 2:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedArmours[pickedItems[0].Item2], PlayerData.Instance.UnlockedArmours, GameData.Instance.LockedArmours);
                    break;
                case 3:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedPropellers[pickedItems[0].Item2], PlayerData.Instance.UnlockedPropellers, GameData.Instance.LockedPropellers);
                    break;
            }
            OnPickedShopItem.Invoke();
        }

        public void PickShopItem2()
        {
            switch (pickedItems[1].Item1)
            {
                case 1:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedWeapons[pickedItems[1].Item2], PlayerData.Instance.UnlockedWeapons, GameData.Instance.LockedWeapons);
                    break;
                case 2:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedArmours[pickedItems[1].Item2], PlayerData.Instance.UnlockedArmours, GameData.Instance.LockedArmours);
                    break;
                case 3:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedPropellers[pickedItems[1].Item2], PlayerData.Instance.UnlockedPropellers, GameData.Instance.LockedPropellers);
                    break;
            }
            OnPickedShopItem.Invoke();
        }

        public void PickShopItem3()
        {
            switch (pickedItems[2].Item1)
            {
                case 1:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedWeapons[pickedItems[2].Item2], PlayerData.Instance.UnlockedWeapons, GameData.Instance.LockedWeapons);
                    break;
                case 2:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedArmours[pickedItems[2].Item2], PlayerData.Instance.UnlockedArmours, GameData.Instance.LockedArmours);
                    break;
                case 3:
                    PlayerData.Instance.UnlockItem(GameData.Instance.LockedPropellers[pickedItems[2].Item2], PlayerData.Instance.UnlockedPropellers, GameData.Instance.LockedPropellers);
                    break;
            }
            OnPickedShopItem.Invoke();
        }

        //NOTE: Unity Events do not support parameters like enums. So I have to manually create individual public methods for each cycler function...
        public void NextWeapon()
        {
            PlayerData.Instance.EquippedWeapon = PlayerData.Instance.WeaponCycler.Next();
            OnSwitchItem.Invoke();
        }

        public void PreviousWeapon()
        {
            PlayerData.Instance.EquippedWeapon = PlayerData.Instance.WeaponCycler.Previous();
            OnSwitchItem.Invoke();
        }

        public void NextArmour()
        {
            PlayerData.Instance.EquippedArmour = PlayerData.Instance.ArmourCycler.Next();
            OnSwitchItem.Invoke();
        }

        public void PreviousArmour()
        {
            PlayerData.Instance.EquippedArmour = PlayerData.Instance.ArmourCycler.Previous();
            OnSwitchItem.Invoke();
        }

        public void NextPropeller()
        {
            PlayerData.Instance.EquippedPropeller = PlayerData.Instance.PropellerCycler.Next();
            OnSwitchItem.Invoke();
        }

        public void PreviousPropeller()
        {
            PlayerData.Instance.EquippedPropeller = PlayerData.Instance.PropellerCycler.Previous();
            OnSwitchItem.Invoke();

        }

        private void UpdateCurrentItems()
        {
            CurrentWeaponUI.sprite = PlayerData.Instance.EquippedWeapon.PreviewSprite;
            CurrentArmourUI.sprite = PlayerData.Instance.EquippedArmour.PreviewSprite;
            CurrentPropellerUI.sprite = PlayerData.Instance.EquippedPropeller.PreviewSprite;
        }

        private void UpdateNextItems()
        {
            if (PlayerData.Instance.UnlockedWeapons.Count > 1) NextWeaponUI.sprite = PlayerData.Instance.WeaponCycler.GetNext().PreviewSprite;
            else NextWeaponUI.sprite = LockedPartUI;

            if (PlayerData.Instance.UnlockedArmours.Count > 1) NextArmourUI.sprite = PlayerData.Instance.ArmourCycler.GetNext().PreviewSprite;
            else NextArmourUI.sprite = LockedPartUI;

            if (PlayerData.Instance.UnlockedPropellers.Count > 1) NextPropellerUI.sprite = PlayerData.Instance.PropellerCycler.GetNext().PreviewSprite;
            else NextPropellerUI.sprite = LockedPartUI;
        }

        private void UpdatePreviousItems()
        {
            if (PlayerData.Instance.UnlockedWeapons.Count > 1) PreviousWeaponUI.sprite = PlayerData.Instance.WeaponCycler.GetPrevious().PreviewSprite;
            else PreviousWeaponUI.sprite = LockedPartUI;

            if (PlayerData.Instance.UnlockedArmours.Count > 1) PreviousArmourUI.sprite = PlayerData.Instance.ArmourCycler.GetPrevious().PreviewSprite;
            else PreviousArmourUI.sprite = LockedPartUI;

            if (PlayerData.Instance.UnlockedPropellers.Count > 1) PreviousPropellerUI.sprite = PlayerData.Instance.PropellerCycler.GetPrevious().PreviewSprite;
            else PreviousPropellerUI.sprite = LockedPartUI;
        }

        private void AssignShopItem1()
        {
            switch (pickedItems[0].Item1)
            {
                case 1:
                    Weapon weapon = GameData.Instance.LockedWeapons[pickedItems[0].Item2];
                    ShopItem1.sprite = weapon.PreviewSprite;
                    ShopItem1Text.text = weapon.Name;
                    break;
                case 2:
                    Armour armour = GameData.Instance.LockedArmours[pickedItems[0].Item2];
                    ShopItem1.sprite = armour.PreviewSprite;
                    ShopItem1Text.text = armour.Name;
                    break;
                case 3:
                    Propeller propeller = GameData.Instance.LockedPropellers[pickedItems[0].Item2];
                    ShopItem1.sprite = propeller.PreviewSprite;
                    ShopItem1Text.text = propeller.Name;
                    break;
            }
        }
        private void AssignShopItem2()
        {
            switch (pickedItems[1].Item1)
            {
                case 1:
                    Weapon weapon = GameData.Instance.LockedWeapons[pickedItems[1].Item2];
                    ShopItem2.sprite = weapon.PreviewSprite;
                    ShopItem2Text.text = weapon.Name;
                    break;
                case 2:
                    Armour armour = GameData.Instance.LockedArmours[pickedItems[1].Item2];
                    ShopItem2.sprite = armour.PreviewSprite;
                    ShopItem2Text.text = armour.Name;
                    break;
                case 3:
                    Propeller propeller = GameData.Instance.LockedPropellers[pickedItems[1].Item2];
                    ShopItem2.sprite = propeller.PreviewSprite;
                    ShopItem2Text.text = propeller.Name;
                    break;
            }
        }
        private void AssignShopItem3()
        {
            switch (pickedItems[2].Item1)
            {
                case 1:
                    Weapon weapon = GameData.Instance.LockedWeapons[pickedItems[2].Item2];
                    ShopItem3.sprite = weapon.PreviewSprite;
                    ShopItem3Text.text = weapon.Name;
                    break;
                case 2:
                    Armour armour = GameData.Instance.LockedArmours[pickedItems[2].Item2];
                    ShopItem3.sprite = armour.PreviewSprite;
                    ShopItem3Text.text = armour.Name;
                    break;
                case 3:
                    Propeller propeller = GameData.Instance.LockedPropellers[pickedItems[2].Item2];
                    ShopItem3.sprite = propeller.PreviewSprite;
                    ShopItem3Text.text = propeller.Name;
                    break;
            }
        }

        private void DisableItems()
        {
            ShopItem1.transform.parent.gameObject.SetActive(false);
            ShopItem2.transform.parent.gameObject.SetActive(false);
            ShopItem3.transform.parent.gameObject.SetActive(false);
        }
    }
}
