using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoParabolico : MonoBehaviour
    
{
    // V_o = Velocidad inicial
    // Teta_o = Angulo de velocidad inicial
    // X =  Distancia que el usuario espera que recorra el circulo

    //Datos necesarios para calcular el movimiento parabolico
    [SerializeField]
    GameObject inputTextInitialVelocity,
               inputTextInitialLaunchAngle, 
               inputTextHorizonatalExpectedDisplacementByUser;
    float initial_velocity, initial_launch_angle;
    const float gravity = 9.8f;
    Vector2 Velocity;
    float time = 0;

    //Datos necesarios para simular el movimiento parabolico
    [SerializeField]
    GameObject circulo;
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
    // Start is called before the first frame update
    void Start()
    {
        Circledisplacement = Vector2.zero;
         labels = new List<GameObject>()
    {
        inputTextInitialVelocity,
        inputTextInitialLaunchAngle,
        inputTextHorizonatalExpectedDisplacementByUser
    };
        errorsDescription = new List<string>();
        ErrorPanel.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {

        if (!finish)
        {
            time += Time.deltaTime*2;
            
            Circledisplacement = Displacement(Velocity, time);
            if (Circledisplacement.y > 0f)
            {
                circulo.GetComponent<RectTransform>().anchoredPosition = Circledisplacement;
                Displacement_x = Circledisplacement.x;
            }
            else
            {
                Debug.Log(Displacement_x);
                Debug.Log("Simulación terminada");
                finish = true;
                circulo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                submitButton.SetActive(true);
                FinishPanel.SetActive(true);


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
            initial_velocity = float.Parse(inputTextInitialVelocity.GetComponentInChildren<InputField>().text);
            initial_launch_angle = float.Parse(inputTextInitialLaunchAngle.GetComponentInChildren<InputField>().text);
            float V_x = initial_velocity * Mathf.Cos(AngleToRadians(initial_launch_angle));
            float V_y = initial_velocity * Mathf.Sin(AngleToRadians(initial_launch_angle));
            Velocity = new Vector2(
                V_x,
                V_y);
        }        

    }
}
