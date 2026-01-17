using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraDescriptionManager : MonoBehaviour
{
    [SerializeField] private GameObject extraDescriptionUI;
    [SerializeField] private GameObject MasteryPointDescriptionUI;

    private MasteryPointHandler masteryPointDescriptionUI;

    List<GameObject> extraDescriptionUIs = new List<GameObject>();

    public void SetMasteryPointDescription(string cardType, int maxMasteryPoint)
    {
        ClearMasteryPointDescription();

        //숙련도 UI 생성
        masteryPointDescriptionUI = Instantiate(MasteryPointDescriptionUI, gameObject.transform).GetComponent<MasteryPointHandler>();
        masteryPointDescriptionUI.SetMasteryDescription(cardType, maxMasteryPoint);
    }

    public void ClearMasteryPointDescription()
    {
        if (masteryPointDescriptionUI)
        {
            Destroy(masteryPointDescriptionUI.gameObject);
        }
    }

    public void SetExtraDescription(List<ExtraDescription> newExtraDescription)
    {
        //추가 설명 UI 초기화
        ClearExtraDescription();

        //ExtraDescription UI 생성
        foreach (ExtraDescription extraDescription in newExtraDescription)
        {
            ExtraDescriptionHandler descriptionHandler = Instantiate(extraDescriptionUI, gameObject.transform).GetComponent<ExtraDescriptionHandler>();
            descriptionHandler.UpdateExtraDescription(extraDescription);
            //descriptionHandler.gameObject.SetActive(false);

            extraDescriptionUIs.Add(descriptionHandler.gameObject);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void ClearExtraDescription()
    {
        foreach (GameObject descriptionObject in extraDescriptionUIs)
        {
            Destroy(descriptionObject);
        }
        extraDescriptionUIs.Clear();
    }

    public void ShowExtraDescription()
    {
        foreach (GameObject descriptionObject in extraDescriptionUIs)
        {
            descriptionObject.SetActive(true);
        }
    }

    public void HideExtraDescription()
    {
        if (masteryPointDescriptionUI)
        {
            masteryPointDescriptionUI.gameObject.SetActive(false);
        }
        foreach (GameObject descriptionObject in extraDescriptionUIs)
        {
            descriptionObject.SetActive(false);
        }
    }
}
