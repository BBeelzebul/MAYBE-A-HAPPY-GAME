using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{

    public float cronometro;
    public float grade;

    public int rutine;

    public Quaternion angle;


    void Update()
    {
        ComportamientoHumanos();
    }


    public void ComportamientoHumanos()
    {

        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 4)
        {
            rutine = Random.Range(0, 2);
            cronometro = 0;
        }
        switch (rutine)
        {
            case 0:
                break;

            case 1:
                grade = Random.Range(0, 360);
                angle = Quaternion.Euler(0, grade, 0);
                rutine++;
                break;

            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                break;

        }

    }
}
