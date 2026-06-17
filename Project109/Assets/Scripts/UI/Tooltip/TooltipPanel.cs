using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TooltipPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _bodyText;
    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private int _characterWrapLimit = 80;
    [SerializeField] private float _preferredWidthValue = 350f;

    private void Awake()
    {
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

        // 3. 줄바꿈 처리
        if (_layoutElement != null)
        {
            int headerLength = _headerText != null && _headerText.gameObject.activeSelf ? _headerText.text.Length : 0;
            int bodyLength = _bodyText != null && _bodyText.gameObject.activeSelf ? _bodyText.text.Length : 0;
            
            if (headerLength > _characterWrapLimit || bodyLength > _characterWrapLimit)
            {
                _layoutElement.enabled = true;
                _layoutElement.preferredWidth = _preferredWidthValue;
            }
            else
            {
                _layoutElement.enabled = false;
            }
        }
    }
}
