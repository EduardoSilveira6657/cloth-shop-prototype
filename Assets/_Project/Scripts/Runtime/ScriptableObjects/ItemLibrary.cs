using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item Library", menuName = "ScriptableObjects/Item Library", order = 0)]
    [Serializable]
    public class ItemLibrary : ScriptableObject
    {
        public List<ItemObject> itemLibrary;
        

        
        public void CreateItem()
        {
            
        }
    }
}
