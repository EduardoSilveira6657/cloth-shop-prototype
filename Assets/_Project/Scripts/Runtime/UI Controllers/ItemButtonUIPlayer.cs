using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using _Project.Scripts.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI_Controllers
{
    public class ItemButtonUIPlayer : ItemButtonUI
    {
        [SerializeField] Image equippedButtonImage;
        
        
        public override void InitializeUI(ItemObject itemObject, bool isBuy = false, RectTransform parent = null)
        {
            base.InitializeUI(itemObject, isBuy, parent);
            if(itemObject.IsEquipped) equippedButtonImage.gameObject.SetActive(true);
        }

        public void EquipItem()
        {
            var equipEvent = new PlayerEquipItemEvent(_itemObjectReference);
            CustomEventManager.Broadcast(equipEvent);
        }
    }
}
