using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Forms : MonoBehaviour
{
    public string Error { get => _error ; }
    private string _error="";

    GameObject label;

    

    private void Start()
    {

        label = gameObject;
        string labelName = label.name;
        InputField inputField = gameObject.GetComponentInChildren<InputField>();
        
        switch (labelName)
        {
            case "initial Velocity":
                inputField.onValidateInput += delegate (string input, int charIndex, char addedChar)
                {
                    
                    //input = input + addedChar;
                   
                    Debug.Log(input);
                    return PositiveDecimalValidator(input, addedChar);
                };


                break;
            case "Initial Launch Angle":
                inputField.onValidateInput += delegate (string input, int charIndex, char addedChar)
                {
                    //input = input + addedChar;
                    return PositiveDecimalValidator(input, addedChar);
                };


                break;
            case "HorizonatalExpectedDisplacementByUser":
                inputField.onValidateInput += delegate (string input, int charIndex, char addedChar)
                {
                    //input = input + addedChar;
                    return  PositiveDecimalValidator(input, addedChar);
                };


                break;
        }
    }

    private char PositiveDecimalValidator(string input,char charToValidate)
    {
        input = input + charToValidate;

        if (!Regex.IsMatch(input, @"^([1-9]\d*(\,)\d*|0?(\,)\d*[1-9]\d*|[1-9]\d*)$")) //"(^[|0-9][0-9]*)(\,*)[0-9]*$"))
        {
            
            charToValidate = '\0';
            
            
        }
        return charToValidate;
    }


    void InitialVelocityError(float velocity)
    {
        if (velocity < 0 )
        {
            _error = "La velocidad debe ser positiva";
            

            
        }
        else if (velocity > 100000)
        {
            _error = "La velocidad maxima permitida es de 100000";
        }
        else 
        {
            _error = "";
        }
        
    }
    void initialLaunchAngleError(float Angle)
    {
        if (Angle < 0 || Angle >90 )
        {
            _error = "El angulo debe estar entre 0 y 90";
            

            
        }
        else
        {
            _error = "";
        }
        //ErrorRender();
    }
    void HorizonatalExpectedDisplacementByUserError (float displacement)
    {
        if (displacement < 0)
        {
            _error = "La distancia debe ser positiva";
            

            
        }
        else
        {
            _error = "";
        }
        //ErrorRender();
    }

    public void EmptyInputChecker()
    {
        string value = gameObject.GetComponentInChildren<InputField>().text;
        _error = value == "" ? "Input vacio" : _error; 
    }
    public  void ErrorChecker()
    {
        
        float inputNumberValue;
        string value = gameObject.GetComponentInChildren<InputField>().text;
        try
        {
            
            inputNumberValue = float.Parse(value);
            string labelName = label.name;

            switch (labelName)                
            {
                case "initial Velocity":
                    InitialVelocityError(inputNumberValue);
                    
                    break;
                case "Initial Launch Angle":
                    initialLaunchAngleError(inputNumberValue);
                    
                    break;
                case "HorizonatalExpectedDisplacementByUser":
                    HorizonatalExpectedDisplacementByUserError(inputNumberValue);
                    
                    break;                
            }
            

        }
        catch (System.FormatException )
        {
           
            
            
        }
        finally
        {
            //Debug.Log(_error);
            //Debug.Log("vamos a renderizar el mensaje de error");
            ErrorRender();
        }
    }
    public void ErrorRender()
    {
        
        label.transform.Find("Error").GetComponent<Text>().text = _error;
        
    }
}
