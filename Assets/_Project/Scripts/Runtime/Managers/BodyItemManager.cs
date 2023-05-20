using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Runtime.DataModels
{
    public class BodyItemManager : MonoBehaviour
    {
        [SerializeField] List<BodySectionModel> bodySections;
        
        public void SetBodyItemSprites(BodySectionType bodySectionToChange ,List<Sprite> itemSprites)
        {
            var bodySection = bodySections.Find(bodySectionModel => bodySectionModel.BodySectionType == bodySectionToChange);
            
            if(bodySection == null) return;
            
            bodySection.SetBodySectionItemSprites(itemSprites);
        }
    }
}