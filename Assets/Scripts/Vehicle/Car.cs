using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Vehicles/Car")]
public class Car : ScriptableObject
{
    public float MotorForce, BreakForce, MaxSteerAngle;
    public float Speed;
    public float Health;
    public float Cost;
}
