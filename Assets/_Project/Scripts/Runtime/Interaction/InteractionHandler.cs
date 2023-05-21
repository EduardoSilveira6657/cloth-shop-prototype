using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using UnityEngine;

namespace _Project.Scripts.Runtime.Interaction
{
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] InteractionType interactionType;
        
        bool isInRange = false;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var requestInteractionEvent = InteractionEvents.RequestInteraction;
                requestInteractionEvent.InteractionType = interactionType;
                CustomEventManager.Broadcast(requestInteractionEvent);
            }
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            isInRange = true;
            var enteredInteractionRangeEvent = InteractionEvents.EnteredInteractionRange;
            enteredInteractionRangeEvent.InteractionType = interactionType;
            CustomEventManager.Broadcast(enteredInteractionRangeEvent);
            Debug.Log("IN Range");
        }
        void OnTriggerExit2D(Collider2D other)
        {
            isInRange = false;
            var exitedInteractionRangeEvent = InteractionEvents.ExitedInteractionRange;
            exitedInteractionRangeEvent.InteractionType = interactionType;
            CustomEventManager.Broadcast(exitedInteractionRangeEvent);
            Debug.Log("Out of Range");
        }
        void OnMouseEnter()
        {
            Debug.Log("Mouse Entered hover");
        }

        void OnMouseExit()
        {
            Debug.Log("Mouse Exit hover");
        }

        void OnMouseUp()
        {
            if(!isInRange) return;
            var requestInteractionEvent = InteractionEvents.RequestInteraction;
            requestInteractionEvent.InteractionType = interactionType;
            CustomEventManager.Broadcast(requestInteractionEvent);
        }
    }
}
