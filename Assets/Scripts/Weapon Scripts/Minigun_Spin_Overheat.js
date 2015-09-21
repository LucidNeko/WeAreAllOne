#pragma strict

var shootMinigun : AudioClip;
var spinMinigun : AudioClip;

var crosshairHeat : Texture2D;
var overheatText : Texture2D;
var position : Rect;
static var OriginalOn = true;

var heat: float = 0;
var heatPer: float = 1;
var maxHeat: float = 100;
var Overheat: boolean = false;

var smoke: ParticleSystem;

var speed: float = 0;
var maxSpeed: float = 1000;

var projectile : Rigidbody;
var clone = projectile;
var bspeed = -20;

function Start() {
	position = Rect((Screen.width - crosshairHeat.width) / 2, (Screen.height - 
	crosshairHeat.height) /2, crosshairHeat.width, crosshairHeat.height);
	position = Rect((Screen.width - overheatText.width) / 2, (Screen.height - 
	overheatText.height) /2, overheatText.width, overheatText.height);
	smoke = GetComponent.<ParticleSystem>();
}

function Update () {
	transform.Rotate(Vector3.forward, speed * Time.deltaTime);
	Spin ();
	Heat ();
	if (Overheat) {
		Cooldown();
		}
		else {
			if (speed == maxSpeed) {
				Fire ();
				
				}
			else {
				GetComponent.<AudioSource>().Play();
		}
	}
}

function Spin () {
	if (Input.GetMouseButton(0)){
		speed += 10;
		}
		else {
		speed -= 10;
		heat -= heatPer;
	}
	if (speed > maxSpeed){
		speed = maxSpeed;
	}
	if (speed <= 0) {
		speed = 0;
	}
}

function Heat () {
		if (heat > maxHeat) {
		heat = maxHeat;
		}
		if (heat < 0) {
		heat = 0;
		Overheat = false;
		}
	if (heat == maxHeat) {
		Overheat = true;
		}
}

function Cooldown () {
	heat -= 0.5;
	smoke.emissionRate = heat;
	GetComponent.<AudioSource>().Stop();
}

function Fire () {
	clone = Instantiate(projectile, transform.position, transform.rotation);
	clone.velocity = transform.TransformDirection( Vector3 (0, 0, bspeed));
	Destroy (clone.gameObject, 3);
	heat += heatPer;
}

function OnGUI()
 {
     if(OriginalOn == true)
     {
		GUI.DrawTexture(position, crosshairHeat);
		if (Overheat == true) {
         	GUI.DrawTexture(position, overheatText);
         }
     }
 }