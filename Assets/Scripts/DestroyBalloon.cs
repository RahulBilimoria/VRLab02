using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBalloon : MonoBehaviour {

    private void Update()
    {
        if (gameObject.transform.position.y > 7)
        {
            RemoveBalloon();
        }
    }

    // Update is called once per frame
    public void RemoveBalloon () {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<ConstantForce>().enabled = false;
        Vector3 scale = gameObject.transform.localScale;
        scale.x = 0.05f;
        scale.y = 0.05f;
        scale.z = 0.05f;
        gameObject.transform.localScale = scale;
        gameObject.SetActive(false);
    }
}
