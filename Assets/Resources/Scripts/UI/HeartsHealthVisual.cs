using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour
{
    [SerializeField] private CanvasScaler m_canvasScaler;
    [SerializeField] private Sprite m_heartFull;
    [SerializeField] private Sprite m_heartHalf;
    [SerializeField] private Vector2 m_distanceBetween;

    public void InitHealth(uint health)
    {
        int i = 0;
        for (; i < health / 2; ++i)
        {
            CreateHeartImage(new Vector2(m_distanceBetween.x * i, 0), 2);
        }
        if (health % 2 == 1)
            CreateHeartImage(new Vector2(m_distanceBetween.x * i, 0), 1);
    }

    public void UpdateHealth(uint health)
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Heart")
                Destroy(child.gameObject);
        }
        InitHealth(health);
    }

    private Image CreateHeartImage(Vector2 anchoredPosition, int health)
    {
        GameObject heartObject = new GameObject("Heart", typeof(Image));

        heartObject.transform.SetParent(transform);
        heartObject.transform.localPosition = Vector3.zero;

        heartObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50) * m_canvasScaler.scaleFactor;

        Image heartImage = heartObject.GetComponent<Image>();
        if (health == 2) heartImage.sprite = m_heartFull; else if (health == 1) heartImage.sprite = m_heartHalf;

        return heartImage;
    }
}
