using System.Collections.Generic;
using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using _Project.Scripts.Runtime.Extensions;
using _Project.Scripts.Runtime.Interaction;
using _Project.Scripts.Runtime.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Runtime.UI
{
    public class ShopUIController : MonoBehaviour
    {
        [SerializeField] ItemLibrary shopInventory;
        [SerializeField] InteractionType interactionType = InteractionType.Shop;
        [SerializeField] CanvasGroup shopCanvasGroup;
        [SerializeField] CanvasGroup shopInteractionWindowCanvasGroup;
        [SerializeField] CanvasGroup notInRangeWindowCanvasGroup;
        [SerializeField] RectTransform buttonsRoot;
        [SerializeField] ItemButtonPool uiItemButtonPool;
        
        
        List<ItemButtonUI> _itemButtonUIs = new List<ItemButtonUI>();
        Tween _notInRangeWarningTween;
        bool _shopIsOpen = false;

        void Start()
        {
            CustomEventManager.AddListener<RequestInteractionEvent>(OnRequestInteraction);
            CustomEventManager.AddListener<EnteredInteractionRangeEvent>(OnEnteredInteractionRange);
            CustomEventManager.AddListener<ExitedInteractionRangeEvent>(OnExitedInteractionRange);
            CustomEventManager.AddListener<NotInInteractionRangeEvent>(OnNotInInteractionRange);
            CustomEventManager.AddListener<BuyItemFromShopEvent>(OnBuyItem);
            CustomEventManager.AddListener<PlayerSellItemEvent>(OnSellItem);
            InitializeUI(shopInventory);
            shopInteractionWindowCanvasGroup.FadeOut(0.1f);
        }

        void Update()
        {
            if(_shopIsOpen && Input.GetKeyDown(KeyCode.Escape)) CloseShop();
        }

        void OnDestroy()
        {
            CustomEventManager.RemoveListener<RequestInteractionEvent>(OnRequestInteraction);
            CustomEventManager.RemoveListener<EnteredInteractionRangeEvent>(OnEnteredInteractionRange);
            CustomEventManager.RemoveListener<ExitedInteractionRangeEvent>(OnExitedInteractionRange);
            CustomEventManager.RemoveListener<NotInInteractionRangeEvent>(OnNotInInteractionRange);
            CustomEventManager.RemoveListener<BuyItemFromShopEvent>(OnBuyItem);
            CustomEventManager.RemoveListener<PlayerSellItemEvent>(OnSellItem);
        }


        void OnRequestInteraction(RequestInteractionEvent evt)
        {
            if(_shopIsOpen) return;
            if (interactionType != evt.InteractionType) return;
            OpenShop();
        }


        void OnEnteredInteractionRange(EnteredInteractionRangeEvent evt)
        {
            if (interactionType != evt.InteractionType || _shopIsOpen || shopInteractionWindowCanvasGroup.alpha > 0.1f) return;
            shopInteractionWindowCanvasGroup.FadeIn(0.5f);
        }
        void OnExitedInteractionRange(ExitedInteractionRangeEvent evt)
        {
            shopInteractionWindowCanvasGroup.FadeOut(0.1f);
            CloseShop();
        }
    
        void OnNotInInteractionRange(NotInInteractionRangeEvent obj)
        {
            notInRangeWindowCanvasGroup.DOKill();
            notInRangeWindowCanvasGroup.FadeIn(0.5f);
            if(_notInRangeWarningTween != null) _notInRangeWarningTween.Kill();
            _notInRangeWarningTween = DOVirtual.DelayedCall(1f, () => { notInRangeWindowCanvasGroup.FadeOut(0.1f); });
        }
        
        public void InitializeUI(ItemLibrary itemsInTheShop)
        {
            foreach (var item in itemsInTheShop.itemLibrary)
            {
                var itemButtonUI = uiItemButtonPool.Pool.Get();
                itemButtonUI.InitializeUI(item, true, buttonsRoot);
                _itemButtonUIs.Add(itemButtonUI);
            }
        }
        
        public void OpenShop()
        {
            if(_shopIsOpen) return;
            _shopIsOpen = true;
            shopCanvasGroup.FadeIn(0.5f);
            shopInteractionWindowCanvasGroup.FadeOut(0.1f);
            CustomEventManager.Broadcast(new OpenShopEvent());
        }
        
        public void CloseShop()
        {
            _shopIsOpen = false;
            shopCanvasGroup.FadeOut(0.1f);
            shopInteractionWindowCanvasGroup.FadeIn(0.5f);
            CustomEventManager.Broadcast(new CloseShopEvent());
        }

        void OnSellItem(PlayerSellItemEvent evt)
        {
            var soldItem = uiItemButtonPool.Pool.Get();
            soldItem.InitializeUI(evt.ItemObjectSoldByPlayer, true, buttonsRoot);
            AddItemToShop(evt.ItemObjectSoldByPlayer);
            _itemButtonUIs.Add(soldItem);
        }

        void OnBuyItem(BuyItemFromShopEvent evt)
        {
            var boughtItem = _itemButtonUIs.Find(itemButton => itemButton.ItemObjectReference == evt.ItemObjectBought);
            if(boughtItem == null) return;
            
            RemoveItemFromShop(evt.ItemObjectBought);
            _itemButtonUIs.Remove(boughtItem);
            uiItemButtonPool.Pool.Release(boughtItem);
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


