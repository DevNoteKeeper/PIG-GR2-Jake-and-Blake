using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    public Transform Jack;
    public Transform Blake;

    private LineRenderer lineRenderer;

    public Color lineColor = Color.green;
    public float maxLineLength = 15f;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = lineColor;
    }

    void Update()
    {
        // Jack 또는 Blake이 null이면 메서드 실행 중단
        if (Jack == null || Blake == null)
        {
            return;
        }

        // 선의 시작점을 Jack의 위치로 설정
        lineRenderer.SetPosition(0, Jack.position);

        // 캐릭터 간의 거리 계산
        float distance = Vector3.Distance(Jack.position, Blake.position);

        // 캐릭터 간의 거리가 최대 길이보다 크면 선을 최대 길이로 설정
        if (distance > maxLineLength)
        {
            // 선을 최대 길이로 유지하면서 Jack과 Blake을 이동시킵니다.
            Vector3 direction = (Blake.position - Jack.position).normalized;
            Vector3 newBlakePosition = Jack.position + direction * maxLineLength;
            Blake.position = newBlakePosition;

            // 선 업데이트
            lineRenderer.SetPosition(1, newBlakePosition);
        }
        else
        {
            // 캐릭터의 위치에 따라 선 업데이트
            lineRenderer.SetPosition(1, Blake.position);
        }
    }
}
