using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using _Project.Scripts.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Runtime.DataModels
{
    public class BodyItemManager : MonoBehaviour
    {
        [SerializeField] ItemLibrary equippedItems;
        
        [SerializeField] List<BodySectionModel> bodySections;

        void Start()
        {
            SetupPlayerItems();
            CustomEventManager.AddListener<PlayerEquipItemEvent>(OnEquipItem);
            CustomEventManager.AddListener<PlayerSellItemEvent>(OnPlayerSellItem);
        }

        void OnDestroy()
        {
            CustomEventManager.RemoveListener<PlayerEquipItemEvent>(OnEquipItem);
            CustomEventManager.RemoveListener<PlayerSellItemEvent>(OnPlayerSellItem);
        }

        void SetupPlayerItems()
        {
            foreach (var item in equippedItems.itemLibrary)
            {
                item.IsEquipped = true;
                SetBodyItemSprites(item.bodySectionType, item.GetSprites());
            }
        }

        void OnEquipItem(PlayerEquipItemEvent evt)
        {
            var lastEquipped = equippedItems.itemLibrary.Find(item => item.IsEquipped && item.bodySectionType == evt.ItemObjectEquippedByPlayer.bodySectionType);
            if (lastEquipped != null)
            {
                lastEquipped.IsEquipped = false;
                equippedItems.itemLibrary.Remove(lastEquipped);
            }
            evt.ItemObjectEquippedByPlayer.IsEquipped = true;
            equippedItems.itemLibrary.Add(evt.ItemObjectEquippedByPlayer);
            SetBodyItemSprites(evt.ItemObjectEquippedByPlayer.bodySectionType, evt.ItemObjectEquippedByPlayer.GetSprites());
        }
        
        void OnPlayerSellItem(PlayerSellItemEvent evt)
        {
            var lastEquipped = equippedItems.itemLibrary.Find(item => item.IsEquipped && item.bodySectionType == evt.ItemObjectSoldByPlayer.bodySectionType);
            if (lastEquipped != null)
            {
                lastEquipped.IsEquipped = false;
                equippedItems.itemLibrary.Remove(lastEquipped);
            }
            SetBodyItemSprites(lastEquipped.bodySectionType,new List<Sprite>(){null,null,null,null});
        }

        public void SetBodyItemSprites(BodySectionType bodySectionToChange ,List<Sprite> itemSprites)
        {
            var bodySectionsFound = bodySections.FindAll(bodySectionModel => bodySectionModel.BodySectionType == bodySectionToChange);
            
            if (bodySectionToChange == BodySectionType.Torso)
            {
                bodySectionsFound.AddRange(bodySections.FindAll(bodySectionModel => bodySectionModel.BodySectionType == BodySectionType.Arms));
            }
            
            if(bodySectionsFound.Count == 0) return;
            Debug.Log("Setting up cloths");
            foreach (var bodySection in bodySectionsFound)
            {
                if(bodySection == null) continue;
                bodySection.SetBodySectionItemSprites(itemSprites);
            }
        }
    }
}