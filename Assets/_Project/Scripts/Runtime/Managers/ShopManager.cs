using _Project.Scripts.Runtime.ScriptableObjects;
using _Project.Scripts.Runtime.UI_Controllers;
using UnityEngine;

namespace _Project.Scripts.Runtime.Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] ItemLibrary shopInventory;
        [SerializeField] ShopUIController shopUIController;

        void Start()
        {
            shopUIController.InitializeUI(shopInventory);
        }

        void AddItemToShop(ItemObject itemObject)
        {
            shopInventory.itemLibrary.Add(itemObject);
        }
    
        void RemoveItemFromShop(ItemObject itemObject)
        {
            shopInventory.itemLibrary.Remove(itemObject);
        }
    }
}
