using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using ViveSR.anipal.Eye;
public class HandllemyJobs : MonoBehaviour
{
    [SerializeField] private bool UseJobs;
    private static EyeData eyeData;
    private static EyeData_v2 EyeData2;
    private static VerboseData verboseData;
    private float pupilDiameterLeft, pupilDiameterRight;
    private Vector2 pupilPositionLeft, pupilPositionRight;
    private float eyeOpenLeft, eyeOpenRight;
    // Update is called once per frame
    void Update()
    {
        float time = Time.realtimeSinceStartup;
        if (UseJobs)
        {
            Debug.Log("Frame: " + eyeData.frame_sequence);
            JobHandle job = eyetestchekker();
            job.Complete();
        }
    }

    private JobHandle eyetestchekker()
    {
        eyetest awesomeJob = new eyetest();
        return awesomeJob.Schedule();

    }
}
