using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class LanternController : MonoBehaviour
{
    public Sprite initialSprite;
    public Sprite changedSprite;
    private Light2D light_;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        light_ = transform.GetChild(0).GetComponent<Light2D>();
    }

    public void Change(bool enabled)
    {
        light_.enabled = enabled;

        if (enabled)
        {
            image.sprite = changedSprite;
        }
        else
        {
            image.sprite = initialSprite;
        }
    }
}
