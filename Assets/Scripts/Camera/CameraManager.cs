using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject fadeToBlackCanvasObj;
    [SerializeField] Camera mainCam;
    [SerializeField] float modifiedOffsetCoeff, modifierOffsetLerpCoeff;
    [SerializeField] float lerpCoef;
    [SerializeField] Vector3 firstPosition, offsetY;


    [SerializeField] private float followAngle;


    Coroutine shakeCoroutine;

    //Follow
    [SerializeField] bool isFollowing;
    GameObject targetObj;
    public Vector3 offset;
    Vector3 modifiedOffsetDirection, modifiedOffset;


    void Awake()
    {
        isFollowing = false;
        firstPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isFollowing)
        {
            Following();
        }
    }

    public Camera GetMainCam()
    {
        return mainCam;
    }

    #region Fade To Black

    IEnumerator FadeToBlackCoroutine(float time)
    {
        float timer = 0;
        fadeToBlackCanvasObj.SetActive(true);
        CanvasGroup canvasGroup = fadeToBlackCanvasObj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        while (timer <= time)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / time);
            timer += 0.025f;
            yield return new WaitForSeconds(0.025f);
        }
        yield return null;
    }

    public void FadeToBlack(float time)
    {
        if (fadeToBlackCanvasObj != null)
        {
            StartCoroutine(FadeToBlackCoroutine(time));
        }
    }

    #endregion



    #region Camera Follow

    private void Following()
    {
        modifiedOffset = Vector3.Lerp(modifiedOffset, modifiedOffsetDirection * modifiedOffsetCoeff, modifierOffsetLerpCoeff * Time.fixedDeltaTime);

        Vector2 rotatedXZOffset = new Vector2(offset.x, offset.z).magnitude * new Vector2(Mathf.Cos(Mathf.Deg2Rad * followAngle), Mathf.Sin(Mathf.Deg2Rad * followAngle));
        //Debug.Log(followAngle);
        //        Debug.Log(new Vector2(offset.x, offset.z).ToString() + " " + new Vector2(Mathf.Cos(Mathf.Deg2Rad * followAngle), Mathf.Sin(Mathf.Deg2Rad * followAngle)).ToString() + " " + rotatedXZOffset.ToString());

        Vector3 desiredPosition = targetObj.transform.position + new Vector3(rotatedXZOffset.x, offset.y, rotatedXZOffset.y) + modifiedOffset + offsetY;

        // Vector3 lerpedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, lerpTime * Time.fixedDeltaTime);// for smooth lerp
        Vector3 lerpedPosition = Vector3.Slerp(transform.position, desiredPosition, lerpCoef * Time.deltaTime);// for smooth lerp

        //transform.position = lerpedPosition;
        //transform.position = desiredPosition;
        //Vector3 offsetHorizontal = new Vector3(offset.x, 0, offset.z);
        //Vector3 offsetVertical = new Vector3(0, offset.y, 0);
        //Vector3 orbitHorizontal = offsetHorizontal.magnitude*(-followedObjDirection.direction.normalized);
        //Vector3 orbitVertical = offsetVertical;
        //Vector3 orbitVector = orbitHorizontal + orbitVertical;

        //Vector3 finalPosition = Vector3.Lerp(transform.position, targetObj.transform.position + orbitVector, Time.deltaTime * 2);



        transform.position = desiredPosition;

        //transform.LookAt(targetObj.transform);

        //Debug.Log(targetObj.transform);
    }

    //public void RotateAroundFollowed(float degree)
    //{
    //    followAngle += degree;
    //}

    public void SetFollowAngle(float degree)
    {

        followAngle = degree;
    }

    public void OffsetForObstacles(Vector3 obsticleOffset)
    {

        offsetY = Vector3.Lerp(offsetY, obsticleOffset, 0.015f);

    }
    public void SetModifiedOffsetDirection(Vector3 direction)
    {
        modifiedOffsetDirection = direction.normalized;
    }

    public void StartFollow(GameObject obj)
    {
        isFollowing = true;
        targetObj = obj;

        offset = mainCam.transform.position - obj.transform.position;
        transform.position = obj.transform.position + offset;

        //followAngle = Vector2.SignedAngle(Vector2.right, new Vector2(offset.x, offset.z));

        firstPosition = transform.position;
        //transform.LookAt(obj.transform, Vector3.up);



    }


    //public void StartFollow(GameObject obj, Vector3 offset)
    //{
    //    isFollowing = true;
    //    targetObj = obj;
    //    this.offset = offset;
    //}



    public void StopFollow()
    {
        isFollowing = false;
    }

    public void StartRotate(GameObject obj, Vector3 finalPosition)
    {
        Vector3 lookRotation = finalPosition - transform.position;

        transform.rotation = Quaternion.LookRotation(lookRotation);

    }

    #endregion


    #region Camera Shake

    public void ShakeCamera(float duration = 1, float magnitude = 1, bool decreasingMagnitude = false)
    {
        shakeCoroutine = StartCoroutine(CameraShakeCoroutine(duration, magnitude, decreasingMagnitude));
    }

    public void StopCameraShake()
    {
        StopCoroutine(shakeCoroutine);
    }

    private IEnumerator CameraShakeCoroutine(float duration = 1, float magnitude = 1, bool decreasingMagnitude = false)
    {
        float timer = 0;

        while (timer < duration)
        {
            Vector2 randomShake = Random.insideUnitCircle * magnitude;

            if (decreasingMagnitude)
            {
                randomShake *= (1 - (timer / duration));
            }
            transform.Translate((Vector3)randomShake, Space.Self);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    #endregion

    public void ReturnToFirstPoint()
    {
        transform.position = firstPosition;
    }

}
