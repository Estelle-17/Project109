using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TooltipPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _bodyText;
    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private float _preferredWidthValue = 350f;

    private void Awake()
    {
        if (_headerText == null)
        {
            Transform child = transform.Find("HeaderText");
            if (child != null)
            {
                _headerText = child.GetComponent<TextMeshProUGUI>();
            }
        }
        if (_headerText != null)
        {
            _headerText.enableWordWrapping = true;
        }
        if (_bodyText == null)
        {
            Transform child = transform.Find("BodyText");
            if (child != null)
            {
                _bodyText = child.GetComponent<TextMeshProUGUI>();
            }
        }
        if (_bodyText != null)
        {
            _bodyText.enableWordWrapping = true;
        }
        if (_layoutElement == null)
        {
            _layoutElement = GetComponent<LayoutElement>();
            if (_layoutElement == null)
            {
                _layoutElement = gameObject.AddComponent<LayoutElement>();
            }
        }
    }

    public void Setup(string header, string body)
    {
        // 1. 헤더 세팅
        if (_headerText != null)
        {
            if (string.IsNullOrEmpty(header))
            {
                _headerText.gameObject.SetActive(false);
            }
            else
            {
                _headerText.gameObject.SetActive(true);
                _headerText.text = header;
            }
        }
        // 2. 본문 세팅
        if (_bodyText != null)
        {
            if (string.IsNullOrEmpty(body))
            {
                _bodyText.gameObject.SetActive(false);
            }
            else
            {
                _bodyText.gameObject.SetActive(true);
                _bodyText.text = body;
            }
        }
        // 3. 줄바꿈 처리 및 크기 고정
        if (_layoutElement != null)
        {
            _layoutElement.enabled = true;
            _layoutElement.preferredWidth = _preferredWidthValue;
        }
    }
}
