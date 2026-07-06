using UnityEngine;

public static class PhysicsEquations
{

    private const double CouloumbsConst = 8.9875517923e9;  // Probably need to change this later, so that objects don't get launched

    // Use Couloumbs Law to calculate the force between two electrically charged objects, assuming that dist is already squared
    public static float CalculatePointChargeForceSqDist (float q1, float q2, float sqdist)
    {
        return q1*q2/sqdist;
    }

        public static Vector3 CalculateAcceleration (Vector3 force, float mass)
    {
        return force/mass;
    }
}