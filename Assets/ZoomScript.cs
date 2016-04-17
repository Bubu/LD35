using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ZoomScript : MonoBehaviour {
	private Camera m_camera;
	private int minZoom = 300;
	private int maxZoom = 3500;
	private int pan_speed = 10;
	private Slider m_slider;
	private Vector3 lastPosition;
	private float mouseSensitivity = 5.0f;

	// Use this for initialization
	void Start () {
		m_camera = GetComponent<Camera>();
		m_slider = GameObject.Find("ZoomSlider").GetComponent<Slider>();
		//m_camera.orthographicSize = (1-m_slider.value) * minZoom + (m_slider.value) * maxZoom;
	}
	
	// Update is called once per frame
	void Update () {
		float sroll_wheel = Input.GetAxis("Mouse ScrollWheel");
		float hor = Input.GetAxisRaw("Horizontal");
		float vert = Input.GetAxisRaw("Vertical");
		if (sroll_wheel != 0f){
			m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize + sroll_wheel, minZoom, maxZoom);
			//m_slider.value = Mathf.Clamp(m_camera.orthographicSize + sroll_wheel, minZoom, maxZoom)/maxZoom;
		}
		if(hor != 0f || vert != 0f){
			transform.Translate(-hor * pan_speed,-vert * pan_speed,0f);
		}

		if (Input.GetMouseButtonDown(0))
		{
			lastPosition = Input.mousePosition;
		}

		if (Input.GetMouseButton(0) && !(EventSystem.current.currentSelectedGameObject == m_slider.gameObject))
		{
			Vector3 delta = Input.mousePosition - lastPosition;
			transform.Translate(-delta.x * mouseSensitivity, -delta.y * mouseSensitivity, 0);
			lastPosition = Input.mousePosition;
		}
	}

	public void zoomTo(Vector2 coords, float size){
		m_camera.orthographicSize = size/2-3;
		transform.position = new Vector3(size/2,size/2,transform.position.z);
		maxZoom = (int)(size/2)-3;
	}

	public void setZoom(){
		m_camera.orthographicSize = (1-m_slider.value) * minZoom + (m_slider.value) * maxZoom;
	}
}
