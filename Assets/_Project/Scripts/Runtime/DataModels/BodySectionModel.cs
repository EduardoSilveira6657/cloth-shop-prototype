using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Runtime.DataModels
{
    public class BodySectionModel : MonoBehaviour
    {
        [SerializeField] BodySectionType bodySectionType;
        [SerializeField] BodyOrientationType bodyOrientationType;
        [SerializeField] List<SpriteRenderer> bodySectionParts;

        public BodySectionType BodySectionType => bodySectionType;

        public void SetBodySectionItemSprites(List<Sprite> itemSprites)
        {
            var spriteIndex = bodySectionType == BodySectionType.Arms ? (int)BodyOrientationType.Extra : (int)bodyOrientationType;
            for (int i = 0; i < bodySectionParts.Count; i++)
            {
                if (bodySectionParts[i] == null || itemSprites[spriteIndex] == null)
                {
                    bodySectionParts[i].sprite = null;
                    continue;
                }
                bodySectionParts[i].sprite = itemSprites[spriteIndex];
            }
        }
    }
}