using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform textContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationCurve;

    [SerializeField] private int startAmount;

    private float containerInitPosition;
    private float moveAmount;

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText($"{startAmount}");
        toUpdate.SetText($"{startAmount}");
        containerInitPosition = textContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }

    public void UpdateCount(int count)
    {
        toUpdate.SetText($"{count}");
        textContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetContainer(count));
    }

    private IEnumerator ResetContainer(int count)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{count}");
        Vector3 localPosition = textContainer.localPosition;
        textContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);

    }
}