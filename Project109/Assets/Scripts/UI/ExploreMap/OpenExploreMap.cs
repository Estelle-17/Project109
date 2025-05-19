using UnityEngine;

public class OpenExploreMap : MonoBehaviour
{
    public GameObject exploreMapObject;
    bool isExploreMapOn;

    private void Start()
    {
        exploreMapObject.SetActive(false);
        isExploreMapOn = false;
    }

    public void OnClick_ChangeExploreMapUIActive()
    {
        if (exploreMapObject != null)
        {
            if (isExploreMapOn)
            {
                exploreMapObject.SetActive(false);
                isExploreMapOn = false;
            }
            else
            {
                exploreMapObject.SetActive(true);
                isExploreMapOn = true;
            }
        }
    }
}
