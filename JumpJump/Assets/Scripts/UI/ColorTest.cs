using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorTest : MonoBehaviour
{
    public Color c;
    private Image _img;
    // Start is called before the first frame update
    void Start()
    {
        _img = transform.GetComponent<Image>();
        _img.color = c;
    }
}
