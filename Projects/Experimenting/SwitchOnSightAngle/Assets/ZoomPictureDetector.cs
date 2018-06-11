using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ZoomPictureDetector {

    public bool checkForHitLocation(Vector2 vZp, Vector2 vSp, Vector3 vAngle)
    {
        updateVariables(vZp, vSp, vAngle);
        setBoundaries_mimicingMirrorRaster(v_zp, v_sp);
        return checkForHitRaster(v_zp, v_sp);
    }

    private void updateVariables(Vector2 vZp, Vector2 vSp, Vector3 vAngle)
    {
        v_zp = vZp;
        v_sp = vSp;
        v_angle = vAngle;
    }
    private void setBoundaries_mimicingMirrorRaster(Vector2 Vzp, Vector2 Vsp)
    {
        vRefLower.x = Vzp.x;
        vRefUpper.x = setUpperBoundary(vRefLower.x, Vzp.x, Vsp.x);
        vRefLower.y = Vzp.y;
        vRefUpper.y = setUpperBoundary(vRefLower.y, Vzp.y, Vsp.y);
    }
    private float setUpperBoundary(float low, float zp, float sp)
    {
        float up = low + sp;
        if (up > 359)
        {
            up = up - 360;
        }
        return up;
    }
    private bool checkForHitRaster(Vector2 Vzp, Vector2 Vsp)
    {
        rasterHit_X = CheckDetectionOfRaster(vRefUpper.x, vRefLower.x, v_angle.x, Vsp.x);
        rasterHit_Y = CheckDetectionOfRaster(vRefUpper.y, vRefLower.y, v_angle.y, Vsp.y);

        if (rasterHit_X && rasterHit_Y)
            return true; //index detector in list 
        return false;
    }
    private bool CheckDetectionOfRaster(float vRefUpper, float vRefLower, float vAngle, float spread) 
    {
        if (vRefUpper > vRefLower) //raster between 0 - 360 degrees boundaries
        {
            if ((vAngle < vRefUpper) && (vAngle > vRefLower))
                return true;
            else
            {
                return false;
            }
        }
        else //raster crossing 0 - 360 boundaries
        {
            if ((vAngle < vRefLower) && (vAngle > vRefUpper))
            {
                return false;
            }
            else
                return true;
        }
    }

    Vector2 v_zp = new Vector2();
    Vector2 v_sp = new Vector2();
    Vector3 v_angle = new Vector3();
    Vector2 vRefUpper = new Vector2(); //point to reference to upper boundary => Vzp + vSp
    Vector2 vRefLower = new Vector2(); //point to reference to lower boundary => Vzp
    bool rasterHit_X = false;
    bool rasterHit_Y = false;

}
