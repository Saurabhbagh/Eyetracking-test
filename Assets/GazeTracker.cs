﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;
using UnityEngine.Assertions;
using System.Runtime.InteropServices;

public class GazeTracker : MonoBehaviour
{
    public int LengthOfRay = 25;
    [SerializeField] private LineRenderer GazeRayRenderer;
    private static EyeData_v2 eyeData = new EyeData_v2();
    private bool eye_callback_registered = false;
    int count = 0;
    private void Start()
    {
        if (!SRanipal_Eye_Framework.Instance.EnableEye)
        {
            enabled = false;
            return;
        }
        Assert.IsNotNull(GazeRayRenderer);
    }

    private void Update()
    {
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
            SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

        if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
        {
            SRanipal_Eye_v2.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = true;
        }
        else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
        {
            SRanipal_Eye_v2.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }

        Vector3 GazeOriginCombinedLocal, GazeDirectionCombinedLocal;

        if (eye_callback_registered)
        {
            if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
            else return;
        }
        else
        {
            if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
            else return;
        }

        Vector3 GazeDirectionCombined = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocal);
        Vector3 newpos = Camera.main.transform.position + GazeDirectionCombined * LengthOfRay;
        RaycastHit hit;
        Vector3 origin = Camera.main.transform.position - Camera.main.transform.up * 0.05f;
        if(Physics.Raycast(origin, GazeDirectionCombinedLocal,  out hit))
        {
            if(hit.collider==null)
            {
                Debug.LogError("Error");
            }
            else
            {

                if (hit.transform.gameObject != null)
                {
                    Debug.LogError("Game Object Name" + hit.transform.gameObject.name);
                    GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    quad.gameObject.name = "New Quad :::" + count.ToString();
                    quad.transform.position = hit.transform.position;
                    count++;
                }

            }
           
        }

        
        
       

        //Vector3 GazeDirectionCombined = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocal);////
        GazeRayRenderer.SetPosition(0, Camera.main.transform.position - Camera.main.transform.up * 0.05f);
        GazeRayRenderer.SetPosition(1, Camera.main.transform.position + GazeDirectionCombined * LengthOfRay);
    }
    private void Release()
    {
        if (eye_callback_registered == true)
        {
            SRanipal_Eye_v2.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }
    }
    private static void EyeCallback(ref EyeData_v2 eye_data)
    {
        eyeData = eye_data;
    }
}
