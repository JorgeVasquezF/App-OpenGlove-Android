using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSharpTest.OpenGloveAPI_C_Sharp_HL;

public class SwipeScript : MonoBehaviour {

	Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
	float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

	[SerializeField]
	float throwForceInXandY = 1f; // to control throw force in X and Y directions

	[SerializeField]
	float throwForceInZ = 50f; // to control throw force in Z direction

	Rigidbody rb;
    public static string Url = "ws://127.0.0.1:7070";

    public OpenGlove leftHand = new OpenGlove("Left Hand", "OpenGloveDER", "leftHand", Url);

    List<int> actuatorRegions = new List<int> { 0, 1, 2, 3, 4 };
    List<int> actuatorPositivePins = new List<int> { 11, 10, 9, 3, 6 };
    List<int> actuatorNegativePins = new List<int> { 12, 15, 16, 2, 8 };
    void Start()
	{
		rb = GetComponent<Rigidbody> ();

        Debug.Log("INICIAR");
        leftHand.ConnectToWebSocketServer();
        Debug.Log("CONNECT SERVER");
        leftHand.ConnectToBluetoothDevice();
        Debug.Log("CONNECT BLUETOOTH");
        leftHand.GetOpenGloveArduinoVersionSoftware();
        leftHand.AddActuators(actuatorRegions, actuatorPositivePins.GetRange(0, 5), actuatorNegativePins.GetRange(0, 5));
        Debug.Log("ADD ACTUATORS");
        leftHand.SetThreshold(0);
        leftHand.SetIMUStatus(true);
        leftHand.Start();
        
    }

	// Update is called once per frame
	void Update () {

		// if you touch the screen
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {

           
            

            

            
            // getting touch position and marking time when you touch the screen
            touchTimeStart = Time.time;
			startPos = Input.GetTouch (0).position;
            int actuator = 4;


            List<int> actuators = new List<int> { actuator };
            Debug.Log(actuatorRegions);
            List<string> intensitiesOn = new List<string> { "255", "255", "255", "255", "255" };
            List<string> intensitiesOff = new List<string> { "0" };
            leftHand.ActivateActuators(actuators, intensitiesOn);
        }

		// if you release your finger
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
            

          


            // marking time when you release it
            touchTimeFinish = Time.time;

			// calculate swipe time interval 
			timeInterval = touchTimeFinish - touchTimeStart;

			// getting release finger position
			endPos = Input.GetTouch (0).position;

			// calculating swipe direction in 2D space
			direction = startPos - endPos;

			// add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
			rb.isKinematic = false;
			rb.AddForce (- direction.x * throwForceInXandY, - direction.y * throwForceInXandY, throwForceInZ / timeInterval);
            int actuator = 3;


            List<int> actuators = new List<int> { actuator };

            List<string> intensitiesOn = new List<string> { "255", "255", "255", "255", "255" };
            List<string> intensitiesOff = new List<string> { "0" };
            leftHand.ActivateActuators(actuatorRegions, intensitiesOn);
            // Destroy ball in 4 seconds
            Destroy (gameObject, 3f);

		}
			
	}
}
