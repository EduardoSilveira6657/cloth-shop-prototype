using _Project.Scripts.Runtime.DataModels;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemObject", order = 1)]
    public class ItemObject : ScriptableObject
    {
        public string itemName;
        public int itemValue;
        public BodySectionType bodySectionType;
    
        public Sprite frontViewItemSprite;
        public Sprite backViewItemSprite;
        public Sprite sideViewItemSprite;

        #if UNITY_EDITOR
        void OnValidate()
        {
            itemName = name;
            if(itemValue == 0) itemValue = Random.Range(25, 100);
        }
        #endif
    }
}
