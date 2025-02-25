using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckCamera_Freetest : MonoBehaviour
{

    public Camera cameraToCheck; // Camera 오브젝트의 레퍼런스를 받을 변수
    public GameObject Player; // OVRPlayerContoroller
    public GameObject Cam; // 카메라 프리팹
    public GameObject RightHand; // 카메라 프리팹'
                                 // public GameObject Camera_light;

    FingerPrintObject fingerprintobject;

    public bool first_check = false; //분말법을 하기 전에 사진을 찍었는지 확인
    public bool second_check = false;//분말법을 한 후에 사진을 찍었는지 확인

    public GameObject HandTrigger;
    npcText failed;
    public bool first_Failed = false;
    private bool first_Failed_check = false; //분말 법 전 사진 x시의 경고 메시지를 한 번만 띄우기 위함


    public GameObject tape;
    FingerPrintTape fingerprinttape; //테이프가 생성되는 스크립트
    private bool second_Failed = false;


    public GameObject other;

    public GameObject Near;
    public GameObject Near2;
    public GameObject Near3;
    public GameObject Near4;
    public GameObject Near5;

    private int number = 1;
    void Start()
    {
        fingerprintobject = GetComponent<FingerPrintObject>();

        failed = HandTrigger.GetComponent<npcText>();

        fingerprinttape = tape.GetComponent<FingerPrintTape>(); //테이프에 있는 컴포넌트 가져오기
    }



    private float MaxDistance = 0.4f; //레이캐스트 거리(카메라  촬영 거리라고 생각해도 됨)
    void Update()
    {
        
        if (fingerprintobject.isVisible == true && first_check != true && first_Failed == false)
        {
            first_Failed = true;
            //0424 분말법 전에 사진을 찍지 않았을 경우
            //failed.FailedFirstCamera();

        }
        // 드러난 지문을 촬영하지 않고 테이프를 붙였을 때를 위함
        
            //if (fingerprintobject.isVisible == true && fingerprinttape.onTape == true && second_check == false && second_Failed == false)
            //{
              //  Debug.Log("2번째 카메라 실패 in CheckCamera");
                //second_Failed = true;
                //0424 분말법 전에 사진을 찍지 않았을 경우
                //failed.FailedSecondCamera();
                //fingerprinttape.onTape = false;
            //}


        if (OVRInput.GetDown(OVRInput.Button.One)) //A버튼을 누를 경우
        {
            Debug.Log("카메라 점수 체크 시작");

            // Cube 오브젝트가 Camera에 의해 보이는지 확인
            if (cameraToCheck != null)
            {

                RaycastHit hit; //레이캐스트와 부딪히는 것
                Vector3 rayDirection = cameraToCheck.transform.position - transform.position;
                // 카메라와 원하는 객체와의 거리
                // Cube와 Camera 사이에 다른 객체가 있는지 Raycast를 통해 확인
                //if (Physics.Raycast(transform.position, rayDirection, out hit, MaxDistance))

                ///처음 시도 시에는 거리 체크 x
                if (fingerprintobject.isVisible == false)
                {
                    if (Physics.Raycast(transform.position, rayDirection, out hit))
                    //카메라와 객체 사이에 무언가 부딪힐 경우z
                    {
                        //인식하고자 하는 객체와 카메라, 플레이어 오브젝트가 가리는 것은 제외
                        if (hit.collider.gameObject != cameraToCheck.gameObject && hit.collider.gameObject != gameObject && hit.collider.gameObject != Player && hit.collider.gameObject != gameObject && hit.collider.gameObject != Cam && hit.collider.gameObject != RightHand && hit.collider.gameObject != other && hit.collider.gameObject != Near
                            && hit.collider.gameObject != Near2 && hit.collider.gameObject != Near3 && hit.collider.gameObject != Near4 && hit.collider.gameObject != Near5)
                        {
                            // 다른 객체로 가려져 있으면 "False" 출력
                            string hiddenObjectName = hit.collider.gameObject.name;
                            Debug.Log(gameObject.name+" 다른 객체로 가려져 있다." + hiddenObjectName);
                            return;
                        }
                    }
                    else
                    {
                        Debug.Log("거리부족");
                        return;
                    }
                }

                // 2번째 시도에서는 근접에서 촬영하므로 거리 체크 O
                if (fingerprintobject.isVisible == true)
                {
                    if (Physics.Raycast(transform.position, rayDirection, out hit))
                    //카메라와 객체 사이에 무언가 부딪힐 경우z
                    {
                        //인식하고자 하는 객체와 카메라, 플레이어 오브젝트가 가리는 것은 제외
                        if (hit.collider.gameObject != cameraToCheck.gameObject && hit.collider.gameObject != gameObject && hit.collider.gameObject != Player && hit.collider.gameObject != gameObject && hit.collider.gameObject != Cam && hit.collider.gameObject != RightHand && hit.collider.gameObject != other && hit.collider.gameObject != Near
                            && hit.collider.gameObject != Near2 && hit.collider.gameObject != Near3 && hit.collider.gameObject != Near4 && hit.collider.gameObject != Near5)
                        {
                            // 다른 객체로 가려져 있으면 "False" 출력
                            string hiddenObjectName = hit.collider.gameObject.name;
                            Debug.Log(gameObject.name + " 다른 객체로 가려져 있다." + hiddenObjectName);
                            return;
                        }
                    }
                    else
                    {
                        Debug.Log("거리부족");
                        return;
                    }
                }

                Vector3 viewportPoint = cameraToCheck.WorldToViewportPoint(transform.position);

                if (viewportPoint.x > 0.1 && viewportPoint.x < 0.9 &&
                     viewportPoint.y > 0.1 && viewportPoint.y < 0.9 && viewportPoint.z > 0)
                {


                    //0402자 수정 사진을 정상적으로 찍었는지 체크하는 예외처리
                    if (fingerprintobject.isVisible == false && first_check != true) // 분말법을 하기 전 사진을 찍었을 경우
                    {
                        first_check = true;
                        Debug.Log("분말법을 하기 전 사진을 찍었다.");
                    }

                    if (fingerprintobject.isVisible == true && second_check != true && second_Failed == false) // 분말법을 한 후 사진을 찍었을 경우
                    {
                        second_check = true;
                        Debug.Log("분말법을 한 후 사진을 찍었다.");
                        other.SetActive(false);
                    }

                    Debug.Log("True");
                }
                else
                {
                    Debug.Log(gameObject.name+" 객체가 카메라 안에 없다.");

                }
            }
        }
    }
}
