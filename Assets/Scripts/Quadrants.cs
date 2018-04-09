using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadrants : MonoBehaviour {

    public SteamVR_TrackedObject ob;
    private SteamVR_Controller.Device input;
    public Material balloon1;
    public Material balloon2;
    public Material balloon3;
    public Material balloon4;
    public ParticleSystem part;
    
    private GameObject obj;
    private bool laser;

	// Use this for initialization
	void Start () {
        laser = false;
        input = SteamVR_Controller.Input((int)ob.index);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (input.GetPressDown(Valve.VR.EVRButtonId.k_EButton_A)) {
            obj = ObjectPooler.current.getPooledObject();
            growBalloon(obj, 1);
        }
        else if (input.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)){
            obj = ObjectPooler.current.getPooledObject();
            growBalloon(obj, 2);
        }
        else if (input.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu)){
            obj = ObjectPooler.current.getPooledObject();
            growBalloon(obj, 3);
        }
        else if (input.GetPressDown(1ul<<8)){
            obj = ObjectPooler.current.getPooledObject();
            growBalloon(obj, 4);
        }
        else if (input.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)) {
            part.transform.position = gameObject.transform.position;
            part.transform.rotation = gameObject.transform.rotation;
            part.transform.forward = gameObject.transform.forward;
            part.Clear();
            part.Stop();
            part.Play();
        }
        else if (input.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
            laser = true;
        }
        if (input.GetPress(Valve.VR.EVRButtonId.k_EButton_A)) {
            growBalloon(obj, 1);
        }
        else if (input.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip)) {
            growBalloon(obj, 2);
        }
        else if (input.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu)) {
            growBalloon(obj, 3);
        }
        else if (input.GetPress(1ul << 8)) {
            growBalloon(obj, 4);
        }
        if (input.GetPressUp(Valve.VR.EVRButtonId.k_EButton_A)) {
            releaseBalloon(obj);
        }
        else if (input.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip)) {
            releaseBalloon(obj);
        }
        else if (input.GetPressUp(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu)) {
            releaseBalloon(obj);
        }
        else if (input.GetPressUp(1ul << 8)) {
            releaseBalloon(obj);
        }
        if (input.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
            laser = false;
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, transform.position);
        }
        if (laser)
        {
            RaycastHit collide;
            bool hit = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out collide, 100);
            if (hit) {
                if (collide.collider.tag.Equals("projectile")) {
                    collide.collider.gameObject.GetComponent<DestroyBalloon>().RemoveBalloon();
                }
            }
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.forward * 2f);
        }
    }

    void growBalloon(GameObject obj, int i) {
        float growRate = 0.01f;
        if (obj == null) {
            return;
        }
        obj.SetActive(true);
        switch (i) {
            case 1:
                obj.GetComponent<Renderer>().material = balloon1;
                break;
            case 2:
                obj.GetComponent<Renderer>().material = balloon2;
                break;
            case 3:
                obj.GetComponent<Renderer>().material = balloon3;
                break;
            case 4:
                obj.GetComponent<Renderer>().material = balloon4;
                break;
        }
        if (obj.transform.localScale.x > 2) {
            releaseBalloon(obj);
        }
        else {
            Vector3 scale = obj.transform.localScale;
            scale.x += growRate;
            scale.y += growRate;
            scale.z += growRate;
            obj.transform.localScale = scale;
            obj.transform.position = transform.position + transform.forward * 0.1f;
        }
    }

    void releaseBalloon(GameObject obj) {
        obj.GetComponent<ConstantForce>().enabled = true;
    }
}
