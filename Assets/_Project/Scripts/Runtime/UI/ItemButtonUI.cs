using System.Collections.Generic;
using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using _Project.Scripts.Runtime.Extensions;
using _Project.Scripts.Runtime.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
    public class ItemButtonUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemValueText;
        [SerializeField] Image itemImage;
        [SerializeField] protected List<Button> actionButtons;
        [SerializeField] CanvasGroup itemButtonCanvasGroup;
        
        protected ItemObject _itemObjectReference;
        
        
        public ItemObject ItemObjectReference => _itemObjectReference;

        void Start()
        {
            CustomEventManager.AddListener<CurrentAmountOfCoinsResponseEvent>(OnResponse);
        }

        public virtual void InitializeUI(ItemObject itemObject, bool isBuy = false, RectTransform parent = null)
        {
            _itemObjectReference = itemObject;
            itemNameText.text = itemObject.itemName;
            itemValueText.text = itemObject.itemValue.ToString();
            itemImage.sprite = itemObject.frontViewItemSprite;
            actionButtons[isBuy? 0 : 1].gameObject.SetActive(true);
            itemButtonCanvasGroup.FadeIn(0.1f);
            if(parent != null) transform.SetParent(parent);
            transform.localScale = Vector3.one;
            CustomEventManager.Broadcast(new RequestCurrentAmountOfCoinsEvent());
        }

        public virtual void Dispose()
        {
            _itemObjectReference = null;
            itemNameText.text = "";
            itemImage.sprite = null;
            actionButtons.ForEach(button => button.gameObject.SetActive(false));
            itemButtonCanvasGroup.FadeOut(0.1f);
            transform.SetParent(null);
            CustomEventManager.RemoveListener<CurrentAmountOfCoinsResponseEvent>(OnResponse);
        }
        
        public void BuyItem()
        {
            var buyEvent = new BuyItemFromShopEvent(_itemObjectReference);
            CustomEventManager.Broadcast(buyEvent);
            Debug.Log("Buying item");
        }
        
        public virtual void SellItem()
        {
            var sellEvent = new PlayerSellItemEvent(_itemObjectReference);
            CustomEventManager.Broadcast(sellEvent);
            Debug.Log("Selling item");
        }
        
        protected virtual void OnResponse(CurrentAmountOfCoinsResponseEvent evt)
        {
            var canBuy = evt.CurrentAmount >= _itemObjectReference.itemValue;
            itemValueText.color = canBuy ? Color.white : Color.red;
            actionButtons[0].interactable = canBuy;
        }
    }
}
