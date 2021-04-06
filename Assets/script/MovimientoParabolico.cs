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




    bool finish;
    // Start is called before the first frame update
    void Start()
    {
        Circledisplacement = Vector2.zero;
        finish = false;

        initial_velocity = float.Parse(inputTextInitialVelocity.GetComponent<InputField>().text);
        initial_launch_angle = float.Parse(inputTextInitialLaunchAngle.GetComponent<InputField>().text);
        float V_x = initial_velocity * Mathf.Sin(AngleToRadians(initial_launch_angle));
        float V_y = initial_velocity * Mathf.Cos(AngleToRadians(initial_launch_angle));
        Velocity = new Vector2(
            V_x,
            V_y);
        Debug.Log(Velocity);
    }

    // Update is called once per frame
    void Update()
    {

        if (!finish)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            Debug.Log(circulo.GetComponent<RectTransform>().anchoredPosition);
            Debug.Log(Displacement(Velocity, time));
            Circledisplacement = Displacement(Velocity, time);
            if (Circledisplacement.y > 0f)
            {
                circulo.GetComponent<RectTransform>().anchoredPosition = Circledisplacement;
            }
            else
            {
                Debug.Log("Simulación terminada");
                finish = true;
            }
        }
         



    }
    float AngleToRadians(float Angle)
    {
        return (Angle * Mathf.PI)/180;
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
}
