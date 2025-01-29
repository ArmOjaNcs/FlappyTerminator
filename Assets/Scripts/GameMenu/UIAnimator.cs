using DG.Tweening;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private CanvasGroup _canvasGroup;
    private Vector3 _defaultScale;
    private Tween _show;
    private Tween _hide;

    private void Awake()
    {
        _canvasGroup = _rectTransform.GetComponent<CanvasGroup>();
        _defaultScale = _rectTransform.localScale;

        _show = _rectTransform.DOScale(_defaultScale, 1).From(0).SetAutoKill(false)
            .OnComplete(() => _canvasGroup.interactable = true);
        
        _hide = _rectTransform.DOScale(0, 1).From(_defaultScale).SetAutoKill(false)
            .OnComplete(() => gameObject.SetActive(false)).SetAutoKill(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _show.Restart();
    }

    public void Hide()
    {
        _canvasGroup.interactable = false;
        _hide.Restart();
    }
}