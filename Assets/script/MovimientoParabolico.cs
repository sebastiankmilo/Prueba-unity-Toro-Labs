using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MovimientoParabolico : MonoBehaviour
    
{
    // V_o = Velocidad inicial
    // Teta_o = Angulo de velocidad inicial
    // X =  Distancia que el usuario espera que recorra el circulo

    //Datos necesarios para calcular el movimiento parabolico
    [SerializeField]
    GameObject InitialVelocity,
               InitialLaunchAngle, 
               HorizonatalExpectedDisplacementByUser;
    float initial_velocity, initial_launch_angle;
    const float gravity = 9.8f;
    Vector2 Velocity;
    float time = 0;

    //Datos necesarios para simular el movimiento parabolico
    [SerializeField]
    GameObject circle;
    [SerializeField]
    GameObject CartesianPlane;
    
    public UILineRenderer line,XDisplacementIndicator;
    [SerializeField]
    List<Vector2> plot, XIndicator;
    [SerializeField]
    GameObject XPosition;
    [SerializeField]
    Text Xmax;
    Vector2 Circledisplacement;

    //Variable necesarias para validar la información
    List<GameObject> labels;
    List<string> errorsDescription;
    [SerializeField]
    GameObject ErrorPanel;
    [SerializeField]
    GameObject submitButton;


    //Variables necesarias para saber el desempeño en el reto

    float Displacement_x;
    [SerializeField]
    GameObject FinishPanel;

    bool finish=true;

    //Variables necesarias para resetear el entorno
    
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        Circledisplacement = Vector2.zero;
         labels = new List<GameObject>()
        {
            InitialVelocity,
            InitialLaunchAngle,
            HorizonatalExpectedDisplacementByUser
        };
        errorsDescription = new List<string>();
        ErrorPanel.SetActive(false);

        plot.Add(Vector2.zero);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!finish)
        {
            float fitVelocity = 2;
            if (Velocity.magnitude < 100)
            {
                fitVelocity = Mathf.CeilToInt(100 / Velocity.magnitude);
            }
            //else if (Velocity.magnitude < 500)
            //{
            //    fitVelocity = 1;
            //}
            //else if (Velocity.magnitude < 1000)
            //{
            //    fitVelocity = 0.5f;
            //}
            else
            {
                fitVelocity = 100 / Velocity.magnitude;
            }
            //fitVelocity = Mathf.CeilToInt(100 / Velocity.magnitude);
            Circledisplacement = Displacement(Velocity, time);
            int modified = Mathf.CeilToInt(float.Parse(Xmax.text) / 1000);
            Debug.Log(modified);
            time += Time.deltaTime *fitVelocity* (modified + circle.GetComponent<Circle>().Count);
            
            
            Circledisplacement = Displacement(Velocity, time);

            plot.Add(Circledisplacement);
            XIndicator.Add(new Vector2(
                Circledisplacement.x,
                XDisplacementIndicator.gameObject.GetComponent<RectTransform>().anchorMin.y
                ));

            //Paso matriz con vectores de posición para renderizar
            XDisplacementIndicator.Points = XIndicator.ToArray();
            line.Points = plot.ToArray();
            
            if (Circledisplacement.y > 0f)
            {
                circle.GetComponent<RectTransform>().anchoredPosition = Circledisplacement;
                XPosition.GetComponent<Text>().text = Circledisplacement.x.ToString();
                XPosition.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    Circledisplacement.x,
                    XPosition.GetComponent<RectTransform>().anchoredPosition.y
                    );
               
            }
            else
            {
                FinishSimulation();


            }
        }
         



    }
    float AngleToRadians(float Angle)
    {
        return (Angle * Mathf.Deg2Rad);
    }
    Vector2 Displacement(Vector2 Velocity,float time )
    {
        float displacement_x = Velocity.x * time;
        float displacement_y = time*((-0.5f * gravity * time)+Velocity.y);
        Vector2 displacement = new Vector2(
            displacement_x,
            displacement_y
            );
        return displacement;
    }

    public void StartSimulation()
    {
        bool errors=false;
        errorsDescription = new List<string>();
        foreach (var label in labels)
        {
            //Debug.Log(label.name);
            label.GetComponentInChildren<Forms>().EmptyInputChecker();
            string errorDescription = label.GetComponentInChildren<Forms>().Error;
            //Buscar Errores
            bool errorActual;
            errorActual = errorDescription != "" ? true : false;
            //Debug.Log(errorDescription);
            errors = errors || errorActual;
            errorsDescription.Add(errorDescription);

        }
        if (errors)
        {
            
            ErrorPanel.SetActive(true);
            ErrorPanel.transform.Find("Description").GetComponent<Text>().text = "";
            foreach (var error in errorsDescription)
            {
                ErrorPanel.transform.Find("Description").GetComponent<Text>().text += " " + error; 
            }


        }
        else
        {
            ErrorPanel.SetActive(false);
            submitButton.SetActive(false);
            finish = false;
            time = 0f;
            initial_velocity = float.Parse(InitialVelocity.GetComponentInChildren<InputField>().text);
            initial_launch_angle = float.Parse(InitialLaunchAngle.GetComponentInChildren<InputField>().text);
            float V_x = initial_velocity * Mathf.Cos(AngleToRadians(initial_launch_angle));
            float V_y = initial_velocity * Mathf.Sin(AngleToRadians(initial_launch_angle));
            Velocity = new Vector2(
                V_x,
                V_y);
        }        

    }
    void FinishSimulation ()
    {
        Debug.Log("Simulación terminada");
        Debug.Log(Displacement_x);
        Debug.Log(HorizonatalExpectedDisplacementByUser.GetComponentInChildren<InputField>().text);
        
        finish = true;
        
        FinishPanel.SetActive(true);
        bool win =Mathf.Abs( Displacement_x - float.Parse(HorizonatalExpectedDisplacementByUser.GetComponentInChildren<InputField>().text))< Displacement_x*0.05f;
        string FinalMessage = win ? "Felicitaciones \nAcertaste" : "Más suerte para la proxima";
        FinishPanel.transform.Find("title").GetComponent<Text>().text=FinalMessage;

        plot.Clear();
        XIndicator.Clear();

        /*string info = "Distancia recorrida por el circulo en x: " + Displacement_x.ToString() + "m\n" +
            "Distancia predicha por el usuario: " + HorizonatalExpectedDisplacementByUser.GetComponentInChildren<InputField>().text+"m";
        FinishPanel.transform.Find("Description").GetComponent<Text>().text = info;*/

        //Resetear entorno de simulación
        //circle.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //CartesianPlane.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //InitialVelocity.GetComponentInChildren<InputField>().text="";
        //InitialLaunchAngle.GetComponentInChildren<InputField>().text="";
        //HorizonatalExpectedDisplacementByUser.GetComponentInChildren<InputField>().text = "";

        //circle.GetComponent<Circle>().BorderScreen=false;
        //submitButton.SetActive(true);
    }

    void escale (int potencia)
    {
        Debug.Log(potencia);
    }
    

}
