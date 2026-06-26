public static class ElectricForceCalculator
{

    private const double CouloumbsConst = 8.9875517923e9;  // Probably need to change this later, so that objects don't get launched

    // Use Couloumbs Law to calculate the force between two electrically charged objects, assuming that dist is already squared
    public static float CalculatePointChargeForceSqDist (float q1, float q2, float dist)
    {
        //return CouloumbsConst * (q1*q2/(dist*dist));

        // Normally dist would be squared, but this is already done when computing the distance
        return q1*q2/dist;
    }
}