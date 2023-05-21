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
        [SerializeField] ItemLibrary allItems;
        
        [SerializeField] List<BodySectionModel> bodySections;

        void Start()
        {
            SetupPlayerItems();
            CustomEventManager.AddListener<PlayerEquipItemEvent>(OnEquipItem);
        }

        void OnDestroy()
        {
            CustomEventManager.RemoveListener<PlayerEquipItemEvent>(OnEquipItem);
        }

        void SetupPlayerItems()
        {
            foreach (var item in allItems.itemLibrary)
            {
                if(!item.IsEquipped) continue;
                SetBodyItemSprites(item.bodySectionType, item.GetSprites());
            }
        }

        void OnEquipItem(PlayerEquipItemEvent evt)
        {
            SetBodyItemSprites(evt.ItemObjectEquippedByPlayer.bodySectionType, evt.ItemObjectEquippedByPlayer.GetSprites());
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