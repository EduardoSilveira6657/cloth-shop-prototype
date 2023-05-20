using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TorsoItem", menuName = "ScriptableObjects/Torso ItemObject", order = 1)]
    public class ItemObjectTorso : ItemObject
    {
        public Sprite armsViewItemSprite;


        public override List<Sprite> GetSprites()
        {
            var baseSprites = base.GetSprites();
            baseSprites.Add(armsViewItemSprite);
            return baseSprites;
        }
    }
}
