using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObtainText : PooledPrefab<ItemObtainText>
{
    [SerializeField] new RectTransform transform;

    [SerializeField] Image icon;
    [SerializeField] TMP_Text text;
    [SerializeField] float disappearTime = 1.0f;
    [SerializeField] float disappearMoveUp = 200.0f;
    Tween tween;
    protected override void OnCreate()
    {
        base.OnCreate();
        transform.SetParent(MainCanvas.Instance.transform, false);
    }
    public void Set(Item item, int count, Vector2 pos)
    {
        icon.sprite = item.data.itemIcon;
        text.text = $"{item.data.itemName.text} x{count}";
        transform.position = Camera.main.WorldToScreenPoint(pos);
        icon.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        float counter = 0.0f;
        DOTween.To(() => counter, value => counter = value, 1.0f, disappearTime).OnUpdate(() =>
        {
            transform.position = Camera.main.WorldToScreenPoint(pos) + Vector3.up * disappearMoveUp * counter;
            icon.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - counter);
            text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - counter);
        }).OnComplete(() =>
        {
            Release();
        });
    }
}