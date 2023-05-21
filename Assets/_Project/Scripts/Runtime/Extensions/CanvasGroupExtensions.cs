using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Runtime.Extensions
{
    public static class CanvasGroupExtensions
    {
        public static void Hide(this CanvasGroup self)
        {
            self.alpha = 0;
            self.interactable = false;
            self.blocksRaycasts = false;
        }

        public static void Show(this CanvasGroup self)
        {
            self.alpha = 1;
            self.interactable = true;
            self.blocksRaycasts = true;
        }

        public static Tween FadeIn(this CanvasGroup self, float _time, float _startAlpha = 0)
        {
            self.DOKill();
            self.interactable = true;
            self.blocksRaycasts = true;
            return self.DOFade(1f, _time).SetEase(Ease.OutQuart).From(0);
        }

        public static Tween FadeOut(this CanvasGroup self, float _time)
        {
            self.DOKill();
            self.interactable = false;
            self.blocksRaycasts = false;
            return self.DOFade(0f, _time).SetEase(Ease.InQuart).From(1);
        }
        public static Tween ParallelFadeIn(this CanvasGroup self, float _time)
        {
            self.DOKill();
            self.interactable = true;
            self.blocksRaycasts = true;
            return self.DOFade(1f, _time).SetEase(Ease.OutQuart).From(0).SetUpdate(UpdateType.Normal, true);;
        }

        public static Tween ParallelFadeOut(this CanvasGroup self, float _time)
        {
            self.DOKill();
            self.interactable = false;
            self.blocksRaycasts = false;
            return self.DOFade(0f, _time).SetEase(Ease.InQuart).From(1).SetUpdate(UpdateType.Normal, true);;
        }
    
        public static void JustHide(this CanvasGroup self)
        {
            self.alpha = 0;
            self.interactable = false;
            self.blocksRaycasts = false;
        }

        public static void JustShow(this CanvasGroup self)
        {
            self.alpha = 1;
            self.interactable = false;
            self.blocksRaycasts = false;
        }
    }
}