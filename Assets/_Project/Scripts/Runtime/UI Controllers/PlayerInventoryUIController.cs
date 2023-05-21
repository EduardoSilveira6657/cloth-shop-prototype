using System.Collections.Generic;
using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using _Project.Scripts.Runtime.Extensions;
using _Project.Scripts.Runtime.Interaction;
using _Project.Scripts.Runtime.ScriptableObjects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Runtime.UI_Controllers
{
    public class PlayerInventoryUIController : MonoBehaviour
    {
        [SerializeField] ItemLibrary playerInventory;
        [SerializeField] InteractionType interactionType = InteractionType.Other;
        [SerializeField] CanvasGroup playerInventoryCanvasGroup;
        [SerializeField] RectTransform buttonsRoot;
        [SerializeField] ItemButtonPool uiItemButtonPool;
        
        
        List<ItemButtonUI> _itemButtonUIs = new List<ItemButtonUI>();
        Tween _notInRangeWarningTween;
        bool _inventoryIsOpen = false;

        void Start()
        {
            CustomEventManager.AddListener<BuyItemFromShopEvent>(OnBuyItem);
            CustomEventManager.AddListener<PlayerSellItemEvent>(OnSellItem);
            InitializeUI(playerInventory);
        }

        void Update()
        {
            if(_inventoryIsOpen && Input.GetKeyDown(KeyCode.Escape)) CloseInventory();
        }

        void OnDestroy()
        {
            CustomEventManager.RemoveListener<BuyItemFromShopEvent>(OnBuyItem);
            CustomEventManager.RemoveListener<PlayerSellItemEvent>(OnSellItem);
        }

        public void InitializeUI(ItemLibrary itemsInTheInventory)
        {
            foreach (var item in itemsInTheInventory.itemLibrary)
            {
                var itemButtonUI = uiItemButtonPool.Pool.Get();
                itemButtonUI.InitializeUI(item, false, buttonsRoot);
                _itemButtonUIs.Add(itemButtonUI);
            }
        }
        
        public void OpenInventory()
        {
            if(_inventoryIsOpen) return;
            _inventoryIsOpen = true;
            playerInventoryCanvasGroup.FadeIn(0.5f);
        }
        
        public void CloseInventory()
        {
            _inventoryIsOpen = false;
            playerInventoryCanvasGroup.FadeOut(0.5f);
        }

        void OnSellItem(PlayerSellItemEvent evt)
        {
            var boughtItem = _itemButtonUIs.Find(itemButton => itemButton.ItemObjectReference == evt.ItemObjectSoldByPlayer);
            if(boughtItem == null) return;
            
            RemoveItemFromInventory(evt.ItemObjectSoldByPlayer);
            _itemButtonUIs.Remove(boughtItem);
            uiItemButtonPool.Pool.Release(boughtItem);
        }

        void OnBuyItem(BuyItemFromShopEvent evt)
        {
            var soldItem = uiItemButtonPool.Pool.Get();
            soldItem.InitializeUI(evt.ItemObjectBought, false, buttonsRoot);
            AddItemToInventory(evt.ItemObjectBought);
            _itemButtonUIs.Add(soldItem);
        }
        
        void AddItemToInventory(ItemObject itemObject)
        {
            playerInventory.itemLibrary.Add(itemObject);
        }
    
        void RemoveItemFromInventory(ItemObject itemObject)
        {
            playerInventory.itemLibrary.Remove(itemObject);
        }
        
        
    }
}

