using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Runtime.UI
{
    public class EconomyUIController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI coinsText;
        
        string PLAYER_COIN_AMOUNT = "player_coin_amount";
        
        int _playerCoins;

        public int PlayerCoins
        {
            get
            {
                _playerCoins = PlayerPrefs.GetInt(PLAYER_COIN_AMOUNT, 200);                
                return _playerCoins;
            } 
            set
            {
                _playerCoins = value;
                PlayerPrefs.SetInt(PLAYER_COIN_AMOUNT, _playerCoins);
                coinsText.text = _playerCoins.ToString();
            }
        }
        void Start()
        {
            coinsText.text = PlayerCoins.ToString();
            CustomEventManager.AddListener<BuyItemFromShopEvent>(OnBuyItem);
            CustomEventManager.AddListener<PlayerSellItemEvent>(OnSellItem);
            CustomEventManager.AddListener<RequestCurrentAmountOfCoinsEvent>(OnRequestCurrentAmountOfCoins);
        }

        void OnDestroy()
        {
            CustomEventManager.RemoveListener<BuyItemFromShopEvent>(OnBuyItem);
            CustomEventManager.RemoveListener<PlayerSellItemEvent>(OnSellItem);
        }
        
        void OnRequestCurrentAmountOfCoins(RequestCurrentAmountOfCoinsEvent evt)
        {
            CustomEventManager.Broadcast(new CurrentAmountOfCoinsResponseEvent(PlayerCoins));
        }
        
        void OnBuyItem(BuyItemFromShopEvent evt)
        {
            PlayerCoins -= evt.ItemObjectBought.itemValue;
        }
        void OnSellItem(PlayerSellItemEvent evt)
        {
            PlayerCoins += evt.ItemObjectSoldByPlayer.itemValue;
        }
    }
}
