using UnityEngine;
using System.Collections;

public class Sound
{

    public float getEngineSoundPitch(int[] gearRatio, float carSpeed)
    {
        float enginePitch;
        if (carSpeed >= 0)
        {
            float gearMinValue = 0.00F;
            float gearMaxValue = 0.00F;
            int i = 0;
            for (i = 0; i < gearRatio.Length; i++)
            {
                if (gearRatio[i] > carSpeed)
                {
                    break;
                }
            }

            if (i == 0)
            {
                gearMinValue = 0;
                gearMaxValue = gearRatio[i];
            }
            else
            {
                gearMinValue = gearRatio[i - 1];
                gearMaxValue = gearRatio[i];
            }
            enginePitch = ((carSpeed - gearMinValue) / (gearMaxValue - gearMinValue)) + 1;
        }
        else
        {
            enginePitch = Mathf.Abs(carSpeed / 100) + 1;
        }

        return enginePitch;
    }

}
