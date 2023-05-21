using _Project.Scripts.Runtime.ScriptableObjects;
using _Project.Scripts.Runtime.UI;
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

        public void AddItemToShop(ItemObject itemObject)
        {
            shopInventory.itemLibrary.Add(itemObject);
        }
    
        public void RemoveItemFromShop(ItemObject itemObject)
        {
            shopInventory.itemLibrary.Remove(itemObject);
        }
    }
}
