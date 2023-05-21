using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Interaction;
using _Project.Scripts.Runtime.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Runtime.Events
{
    public static class InteractionEvents
    {
        public static readonly RequestInteractionEvent RequestInteraction = new RequestInteractionEvent(); 
        public static readonly EnteredInteractionRangeEvent EnteredInteractionRange = new EnteredInteractionRangeEvent();
        public static readonly ExitedInteractionRangeEvent ExitedInteractionRange = new ExitedInteractionRangeEvent();
        public static readonly NotInInteractionRangeEvent NotInInteractionRange = new NotInInteractionRangeEvent();
        public static readonly BuyItemFromShopEvent BuyItemFromShop = new BuyItemFromShopEvent();
        public static readonly PlayerSellItemEvent PlayerSellItem = new PlayerSellItemEvent();
    }

    public class EnteredInteractionRangeEvent : GameEvent
    {
        public InteractionType InteractionType;
        public GameObject GameObject;
    }

    public class ExitedInteractionRangeEvent : GameEvent
    {
        public InteractionType InteractionType ;
        public GameObject GameObject;
    }

    public class RequestInteractionEvent : GameEvent
    {
        public InteractionType InteractionType;
        public GameObject GameObject;
    }
    
    public class NotInInteractionRangeEvent : GameEvent
    {
        public InteractionType InteractionType;
        public GameObject GameObject;
    }
    
    
    public class BuyItemFromShopEvent : GameEvent
    {
        public ItemObject ItemObjectBought;
        
        public BuyItemFromShopEvent(ItemObject itemObjectBought = null)
        {
            ItemObjectBought = itemObjectBought;
        }
    }
    public class PlayerSellItemEvent : GameEvent
    {
        public readonly ItemObject ItemObjectSoldByPlayer;
        
        public PlayerSellItemEvent(ItemObject itemObjectSoldByPlayer = null)
        {
            ItemObjectSoldByPlayer = itemObjectSoldByPlayer;
        }
    }
    
    public class PlayerEquipItemEvent : GameEvent
    {
        public readonly ItemObject ItemObjectEquippedByPlayer;
        
        public PlayerEquipItemEvent(ItemObject itemObjectEquippedByPlayer = null)
        {
            ItemObjectEquippedByPlayer = itemObjectEquippedByPlayer;
        }
    }
}
