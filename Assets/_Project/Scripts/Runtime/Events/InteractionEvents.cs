using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Interaction;
using UnityEngine;

namespace _Project.Scripts.Runtime.Events
{
    public static class InteractionEvents
    {
        public static readonly RequestInteractionEvent RequestInteraction = new RequestInteractionEvent(); 
        public static readonly EnteredInteractionRangeEvent EnteredInteractionRange = new EnteredInteractionRangeEvent();
        public static readonly ExitedInteractionRangeEvent ExitedInteractionRange = new ExitedInteractionRangeEvent();
        public static readonly NotInInteractionRangeEvent NotInInteractionRange = new NotInInteractionRangeEvent();
    }

    public class EnteredInteractionRangeEvent : GameEvent
    {
        public InteractionType InteractionType;
    }

    public class ExitedInteractionRangeEvent : GameEvent
    {
        public InteractionType InteractionType ;
    }

    public class RequestInteractionEvent : GameEvent
    {
        public InteractionType InteractionType;
    }
    
    public class NotInInteractionRangeEvent : GameEvent
    {
        public InteractionType InteractionType;
    }
}
