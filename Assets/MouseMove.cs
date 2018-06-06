using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {
    Vector3 downPoint;
    Vector3 dir;
    public LayerMask LayerMask;
    public float AngleToGo;
    public float speed;
    public float distMin;
    public float rotSpeed = 1.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            // print("左ボタンが押されている");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, LayerMask))
            {
                //Debug.Log (hit.point);

                Vector3 targetPositon = hit.point;
                // 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
                if (transform.position.y != hit.point.y)
                {
                    targetPositon = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                }
                Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

                Vector3 targetDir = targetPositon - transform.position;
                float angle = Vector3.Angle(targetDir, transform.forward);
                //Debug.Log(angle);

                if ( angle < AngleToGo)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
                }

                if ( Vector3.Distance( targetPositon, gameObject.transform.position) < distMin)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
        }
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
