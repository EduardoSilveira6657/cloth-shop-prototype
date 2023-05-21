using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using _Project.Scripts.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
    public class ItemButtonUIPlayer : ItemButtonUI
    {
        [SerializeField] Image equippedButtonImage;

        void OnPlayerEquipItem(PlayerEquipItemEvent evt)
        {
            if(evt.ItemObjectEquippedByPlayer.bodySectionType != _itemObjectReference.bodySectionType) return;
            if(evt.ItemObjectEquippedByPlayer != _itemObjectReference) equippedButtonImage.gameObject.SetActive(false);
        }
        void OnPlayerSellItem(PlayerSellItemEvent evt)
        {
            if(_itemObjectReference == null) return;
            if(evt.ItemObjectSoldByPlayer.bodySectionType == _itemObjectReference.bodySectionType) return;
            if(evt.ItemObjectSoldByPlayer == _itemObjectReference) equippedButtonImage.gameObject.SetActive(false);
        }

        public override void InitializeUI(ItemObject itemObject, bool isBuy = false, RectTransform parent = null)
        {
            base.InitializeUI(itemObject, isBuy, parent);
            if(itemObject.IsEquipped) equippedButtonImage.gameObject.SetActive(true);
            CustomEventManager.AddListener<PlayerEquipItemEvent>(OnPlayerEquipItem);
            CustomEventManager.AddListener<PlayerSellItemEvent>(OnPlayerSellItem);
            CustomEventManager.AddListener<OpenShopEvent>(OnShopOpen);
            CustomEventManager.AddListener<CloseShopEvent>(OnShopClose);
        }

        public void EquipItem()
        {
            var equipEvent = new PlayerEquipItemEvent(_itemObjectReference);
            CustomEventManager.Broadcast(equipEvent);
            equippedButtonImage.gameObject.SetActive(true);
        }

        public override void SellItem()
        {
            base.SellItem();
        }

        public override void Dispose()
        {
            base.Dispose();
            equippedButtonImage.gameObject.SetActive(false);
            CustomEventManager.RemoveListener<PlayerEquipItemEvent>(OnPlayerEquipItem);
            CustomEventManager.RemoveListener<PlayerSellItemEvent>(OnPlayerSellItem);
            CustomEventManager.RemoveListener<OpenShopEvent>(OnShopOpen);
            CustomEventManager.RemoveListener<CloseShopEvent>(OnShopClose);
        }

        protected override void OnResponse(CurrentAmountOfCoinsResponseEvent evt)
        {
            
        }
        
        void OnShopOpen(OpenShopEvent evt)
        {
            actionButtons[1].gameObject.SetActive(true);
        }
        void OnShopClose(CloseShopEvent evt)
        {
            actionButtons[1].gameObject.SetActive(false);
        }
    }
}
