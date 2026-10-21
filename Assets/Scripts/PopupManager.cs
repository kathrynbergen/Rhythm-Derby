using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private Image popupImage;
    
    [SerializeField] private Sprite homeRunSprite;
    [SerializeField] private Sprite singleSprite;
    [SerializeField] private Sprite strikeSprite;

    void Start()
    {
        SetImageAlpha(0f);
    }
    public void AccuracyPopup(Accuracy accuracy)
    {
        SetImageAlpha(1f);
        switch (accuracy)
        {
            case Accuracy.HomeRun:
                popupImage.sprite = homeRunSprite;
                break;
            case Accuracy.Strike:
                popupImage.sprite = strikeSprite;
                break;
            case Accuracy.Single:
                popupImage.sprite = singleSprite;
                break;
            default:
                Debug.Log("Unknown accuracy, can't create popup for accuracy "+accuracy);
                break;
        }
    }
    private void SetImageAlpha(float alphaValue)
    {
        Color tempColor = popupImage.color;
        tempColor.a = alphaValue;
        popupImage.color = tempColor;
    }
}
