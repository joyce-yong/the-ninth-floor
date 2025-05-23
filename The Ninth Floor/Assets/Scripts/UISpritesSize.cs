using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISpritesSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image targetImage;

    public Sprite normalSprite;
    public Vector2 normalSize = new Vector2(167.1534f, 35.7486f);
    public Vector2 normalOffset = Vector2.zero;

    public Sprite highlightedSprite;
    public Vector2 highlightedSize = new Vector2(167.1534f, 54.7752f);
    public Vector2 highlightedOffset = new Vector2(0, -15);

    public Sprite pressedSprite;

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool isHovered = false;
    private bool isPressed = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (!isPressed)
        {
            ApplyState(highlightedSprite, highlightedSize, highlightedOffset);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (!isPressed)
        {
            ApplyState(normalSprite, normalSize, normalOffset);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        ApplyState(pressedSprite, normalSize, normalOffset);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;

        if (isHovered)
        {
            ApplyState(highlightedSprite, highlightedSize, highlightedOffset);
        }
        else
        {
            ApplyState(normalSprite, normalSize, normalOffset);
        }
    }

    private void ApplyState(Sprite sprite, Vector2 size, Vector2 offset)
    {
        targetImage.sprite = sprite;
        targetImage.preserveAspect = true;
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = originalPosition + offset;
    }
    private void OnDisable()
    {
        isHovered = false;
        isPressed = false;
        ApplyState(normalSprite, normalSize, normalOffset);
    }


    private void Start()
    {
        rectTransform = targetImage.rectTransform;
        originalPosition = rectTransform.anchoredPosition;
        ApplyState(normalSprite, normalSize, normalOffset);
    }

    void Update()
    {
    }
}
