using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slider : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private FloatVariable charge;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.fillAmount = Mathf.Clamp01(charge.value);
    }
}
