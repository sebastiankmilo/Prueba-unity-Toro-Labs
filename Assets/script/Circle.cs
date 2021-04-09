using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Circle : MonoBehaviour
{
    [SerializeField] GameObject Container;
    [SerializeField] Text Ymax, Ymiddle, Xmax, Xmiddle;
    public UILineRenderer line, XDisplacementIndicator;
    GameObject X;
    public int Count { get => _count; set => _count = value; }
    int _count=0;
    public bool BorderScreen { 
        get => _borderScreen;
        set { _borderScreen = value; }
    }
    bool _borderScreen;
    // Start is called before the first frame update
    void Start()
    {
        _borderScreen = false;
        X = Container.transform.Find("X").gameObject;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int factor = 2;
        Debug.Log("Se acabo la pantalla");
        _count += 1;
        Container.GetComponent<RectTransform>().localScale = new Vector3(
            Container.GetComponent<RectTransform>().localScale.x / factor,
            Container.GetComponent<RectTransform>().localScale.y / factor,
            Container.GetComponent<RectTransform>().localScale.z
            ); ;
        modifyEscale(Ymax,2);
        modifyEscale(Xmax, 2);
        modifyEscale(Ymiddle, 2);
        modifyEscale(Xmiddle, 2);
        gameObject.transform.Find("border").GetComponent<RectTransform>().localScale=new Vector3(
            gameObject.transform.Find("border").GetComponent<RectTransform>().localScale.x*factor,
            gameObject.transform.Find("border").GetComponent<RectTransform>().localScale.y * factor,
            gameObject.transform.Find("border").GetComponent<RectTransform>().localScale.x
            );
        X.GetComponent<RectTransform>().localScale = new Vector3(
            X.GetComponent<RectTransform>().localScale.x * factor,
            X.GetComponent<RectTransform>().localScale.y * factor,
            X.GetComponent<RectTransform>().localScale.x
            );
        X.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            X.GetComponent<RectTransform>().anchoredPosition.x,
            X.GetComponent<RectTransform>().anchoredPosition.y * factor
            );
        line.LineThickness = line.LineThickness * factor;
        XDisplacementIndicator.LineThickness = XDisplacementIndicator.LineThickness * factor ;
        XDisplacementIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            XDisplacementIndicator.GetComponent<RectTransform>().anchoredPosition.x,
            XDisplacementIndicator.GetComponent<RectTransform>().anchoredPosition.y*factor
            );
            

        _borderScreen = true;
    }
    void modifyEscale(Text text, int factor)
    {
        text.text = (float.Parse(text.text) * factor).ToString();
    }
}
