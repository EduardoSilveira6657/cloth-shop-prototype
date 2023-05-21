using _Project.Scripts.Runtime.CustomEventSystem;
using _Project.Scripts.Runtime.Events;
using _Project.Scripts.Runtime.Extensions;
using _Project.Scripts.Runtime.Interaction;
using _Project.Scripts.Runtime.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Runtime.UI_Controllers
{
    public class ShopUIController : MonoBehaviour
    {
        [SerializeField] InteractionType interactionType = InteractionType.Shop;
        [SerializeField] CanvasGroup shopCanvasGroup;
        [SerializeField] CanvasGroup shopInteractionWindowCanvasGroup;
        [SerializeField] CanvasGroup notInRangeWindowCanvasGroup;


        Tween _notInRangeWarningTween;

        void Start()
        {
            CustomEventManager.AddListener<RequestInteractionEvent>(OnRequestInteraction);
            CustomEventManager.AddListener<EnteredInteractionRangeEvent>(OnEnteredInteractionRange);
            CustomEventManager.AddListener<ExitedInteractionRangeEvent>(OnExitedInteractionRange);
            CustomEventManager.AddListener<NotInInteractionRangeEvent>(OnNotInInteractionRange);
        }
    
        void OnDestroy()
        {
            CustomEventManager.RemoveListener<RequestInteractionEvent>(OnRequestInteraction);
            CustomEventManager.RemoveListener<EnteredInteractionRangeEvent>(OnEnteredInteractionRange);
            CustomEventManager.RemoveListener<ExitedInteractionRangeEvent>(OnExitedInteractionRange);
            CustomEventManager.RemoveListener<NotInInteractionRangeEvent>(OnNotInInteractionRange);
        }


        void OnRequestInteraction(RequestInteractionEvent evt)
        {
            if (interactionType != evt.InteractionType) return;
            shopCanvasGroup.FadeIn(0.5f);
        }


        void OnEnteredInteractionRange(EnteredInteractionRangeEvent evt)
        {
            if (interactionType != evt.InteractionType) return;
            shopInteractionWindowCanvasGroup.FadeIn(0.5f);
        }
        void OnExitedInteractionRange(ExitedInteractionRangeEvent evt)
        {
            shopInteractionWindowCanvasGroup.FadeOut(0.5f);
        }
    
        void OnNotInInteractionRange(NotInInteractionRangeEvent obj)
        {
            notInRangeWindowCanvasGroup.DOKill();
            notInRangeWindowCanvasGroup.FadeIn(0.5f);
            if(_notInRangeWarningTween != null) _notInRangeWarningTween.Kill();
            _notInRangeWarningTween = DOVirtual.DelayedCall(1f, () => { notInRangeWindowCanvasGroup.FadeOut(0.5f); });
        }
        
        public void InitializeUI(ItemLibrary itemsInTheShop)
        {
            shopCanvasGroup.JustHide();
            shopInteractionWindowCanvasGroup.JustHide();
            notInRangeWindowCanvasGroup.JustHide();
        }

    }
}

