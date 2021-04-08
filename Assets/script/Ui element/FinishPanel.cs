using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class FinishPanel : MonoBehaviour
{
    [SerializeField]
    GameObject circle,
        ContainerCircle,
        CartesianPlane,
        InitialVelocity,
        InitialLaunchAngle,
        HorizonatalExpectedDisplacementByUser,
        submitButton,
        Ymax,
        Xmax,
        Ymiddle,
        Xmiddle;
    public UILineRenderer line,XDisplacementIndicator;
    // Start is called before the first frame update
    public void ResetSimulation()
    {
        circle.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        CartesianPlane.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        InitialVelocity.GetComponentInChildren<InputField>().text = "";
        InitialLaunchAngle.GetComponentInChildren<InputField>().text = "";
        HorizonatalExpectedDisplacementByUser.GetComponentInChildren<InputField>().text = "";

        Ymax.GetComponent<Text>().text = "300";
        Xmax.GetComponent<Text>().text = "600";

        Ymiddle.GetComponent<Text>().text = "150";
        Xmiddle.GetComponent<Text>().text = "300";

        ContainerCircle.GetComponent<RectTransform>().localScale = Vector3.one;
        circle.transform.Find("border").GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1);
        line.Points = new Vector2[0];
        line.LineThickness = 10;

        XDisplacementIndicator.Points = new Vector2[0];
        XDisplacementIndicator.LineThickness = 2;
        XDisplacementIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -30f);

        ContainerCircle.transform.Find("X").GetComponent<RectTransform>().anchoredPosition = new Vector2(
            0f,
            -30f
            );
        ContainerCircle.transform.Find("X").GetComponent<RectTransform>().localScale = Vector3.one;

        circle.GetComponent<Circle>().BorderScreen = false;
        submitButton.SetActive(true);

        gameObject.SetActive(false);
    }
}
