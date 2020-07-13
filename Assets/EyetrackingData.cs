using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Tobii.G2OM;
//using Tobii.XR;
using ViveSR.anipal.Eye;

    


public class EyetrackingData : MonoBehaviour
{




    private static EyeData eyeData;
    private static EyeData_v2 EyeData2;
    private static VerboseData verboseData;
    private float pupilDiameterLeft, pupilDiameterRight;
    private Vector2 pupilPositionLeft, pupilPositionRight;
    private float eyeOpenLeft, eyeOpenRight;
   

    void Update()
    {
        
        Debug.Log(System.DateTime.Now.TimeOfDay.TotalMilliseconds);
        Debug.Log("Time millisecond"+System.DateTime.Now.Millisecond);
        SRanipal_Eye_API.GetEyeData(ref eyeData);
        SRanipal_Eye.GetVerboseData(out verboseData);
        //pupil diameter    
        pupilDiameterLeft = eyeData.verbose_data.left.pupil_diameter_mm;
        pupilDiameterRight = eyeData.verbose_data.right.pupil_diameter_mm;
        // pupil positions    
        pupilPositionLeft = eyeData.verbose_data.left.pupil_position_in_sensor_area;
        pupilPositionRight = eyeData.verbose_data.right.pupil_position_in_sensor_area;
        // eye open  
        eyeOpenLeft = eyeData.verbose_data.left.eye_openness;
        eyeOpenRight = eyeData.verbose_data.right.eye_openness;


        Debug.Log("pupilDiameterLeft :" + pupilDiameterLeft);
        Debug.Log("pupilDiameterRight :" + pupilDiameterRight);
        Debug.Log("pupilPositionLeft :" + pupilPositionLeft);
        Debug.Log("pupilPositionRight :" + pupilPositionRight);
        Debug.Log("eyeOpenLeft :" + eyeOpenLeft);
        Debug.Log(" eyeOpenRight:" + eyeOpenRight);

        //Eye version 2

        SRanipal_Eye_API.GetEyeData(ref eyeData);
        SRanipal_Eye.GetVerboseData(out verboseData);
        //pupil diameter    
        pupilDiameterLeft = eyeData.verbose_data.left.pupil_diameter_mm;
        pupilDiameterRight = eyeData.verbose_data.right.pupil_diameter_mm;
        // pupil positions    
        pupilPositionLeft = eyeData.verbose_data.left.pupil_position_in_sensor_area;
        pupilPositionRight = eyeData.verbose_data.right.pupil_position_in_sensor_area;
        // eye open  
        eyeOpenLeft = eyeData.verbose_data.left.eye_openness;
        eyeOpenRight = eyeData.verbose_data.right.eye_openness;

        Debug.Log("Time Stamp :" + eyeData.timestamp);
        Debug.Log("Frame: " + eyeData.frame_sequence);
        Debug.Log("Gaze Direction left Normalized : " + eyeData.verbose_data.left.gaze_direction_normalized);
        Debug.Log("eye_openness left: " + eyeData.verbose_data.left.eye_openness);
        Debug.Log("Gaze Direction Right Normalized : " + eyeData.verbose_data.right.gaze_direction_normalized);
        Debug.Log("eye_openness Right: " + eyeData.verbose_data.right.eye_openness);
        Debug.Log("pupilDiameterLeft :" + pupilDiameterLeft);
        Debug.Log("pupilDiameterRight :" + pupilDiameterRight);
        Debug.Log("pupilPositionLeft :" + pupilPositionLeft);
        Debug.Log("pupilPositionRight :" + pupilPositionRight);
        Debug.Log("eyeOpenLeft :" + eyeOpenLeft);   
        Debug.Log(" eyeOpenRight:" + eyeOpenRight);
    }




   

    /*
 EyeData data;
 Vector3 GazeOriginCombinedLocal, GazeDirectionCombinedLocal;
 Vector3 GazeOriginCombinedLocalleft, GazeDirectionCombinedLocalleft;
 Vector3 GazeOriginCombinedLocalRight, GazeDirectionCombinedLocalRight;

 // Start is called before the first frame update
 void Start()
 {
      data = new EyeData();
      bool me= SRanipal_Eye_API.IsViveProEye();
         //Calibaration is working
     //SRanipal_Eye_API.LaunchEyeCalibration(System.IntPtr.Zero);
 }

 // Update is called once per frame
 void Update()
 {// Tobi data taker 
  //   Debug.Log("Local : " + TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local).GazeRay.Direction);
  //   Debug.Log("World : "+TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).GazeRay.Direction);
  //
  //   Debug.Log("LocalTime : " + TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local).Timestamp);
  //   Debug.Log("WorldTime : " + TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).Timestamp);
  //
  //
  //   Debug.Log("LocalDistance : " + TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local).ConvergenceDistance);
  //   Debug.Log("WorldDistance : " + TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).ConvergenceDistance);

    SRanipal_Eye_API.GetEyeData(ref data);

     // Vive SRanipal Data Taker
     SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, data);
     SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocalleft, out GazeDirectionCombinedLocalleft, data);
     SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocalRight, out GazeDirectionCombinedLocalRight, data);
     Debug.Log("Out data Combined " + GazeOriginCombinedLocal);
     Debug.Log("Out data left " + GazeOriginCombinedLocalleft );
     Debug.Log("Out data Right " + GazeOriginCombinedLocalRight);
     Debug.Log("data :"+ data);
     Debug.Log("dataeye combined:" + GazeIndex.COMBINE);
     Debug.Log("data tracking improvements :" + data.verbose_data.tracking_improvements);//further check 
     Debug.Log("data Frame Squence :" + data.frame_sequence);
     Debug.Log("data Timestamp :" + data.timestamp);
     Debug.Log("data no user :" + data.no_user);
     //left
     float openness;
     //Debug.Log("data left eye gaze:" + data.verbose_data.left.gaze_direction_normalized);
     //Debug.Log("data left eye openness:" + data.verbose_data.left.eye_openness);
     //Debug.Log("data left eye origin:" + data.verbose_data.left.gaze_origin_mm);
     //Debug.Log("data left eye pupil diameter:" + data.verbose_data.left.pupil_diameter_mm);
     //Debug.Log("data left eye validata bitmask:" + data.verbose_data.left.eye_data_validata_bit_mask);
     SRanipal_Eye.GetEyeOpenness(EyeIndex.LEFT, out openness);
     Debug.Log("data left eye gaze:" + SRanipal_Eye.GetVerboseData(out data.verbose_data,data));
     Debug.Log("data left eye openness:" + openness);
     Debug.Log("data left eye origin:" + data.verbose_data.left.gaze_origin_mm);
     Debug.Log("data left eye pupil diameter:" + data.verbose_data.left.pupil_diameter_mm);
     Debug.Log("data left eye validata bitmask:" + data.verbose_data.left.eye_data_validata_bit_mask);


     //Right
     Debug.Log("data right eye gaze:" + data.verbose_data.right.gaze_direction_normalized);
     Debug.Log("data right eye openness:" + data.verbose_data.right.eye_openness);
     Debug.Log("data right eye origin:" + data.verbose_data.right.gaze_origin_mm);
     Debug.Log("data right eye pupil diameter:" + data.verbose_data.right.pupil_diameter_mm);
     Debug.Log("data right eye validata bitmask:" + data.verbose_data.right.eye_data_validata_bit_mask);
 

}*/
}


