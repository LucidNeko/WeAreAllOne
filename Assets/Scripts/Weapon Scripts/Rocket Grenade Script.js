var radius : float = 5.0; //provides a radius at which the explosive will effect rigidbodies
var power : float = 10.0; //provides explosive power
var explosiveLift : float = 1.0; //determines how the explosion reacts. A higher value means rigidbodies will fly upward
var explosiveDelay : float = 5.0; //adds a delay in seconds to our explosive object
var explosionPrefab : Transform; //Put your particle system prefab in this slot
var explosionSound : AudioClip; //Audio clip for the explosion
 
function Update ()
 
{
 
Fire();
 
}
 
function Fire () {
 
yield WaitForSeconds(explosiveDelay); //provides a delay for our explosion to enable our grenades to be shot out
 
Instantiate(explosionPrefab, transform.position, transform.rotation); //instantiates our particle system prefab to play when the grenade explodes
 
var grenadeOrigin : Vector3 = transform.position;
 
var colliders : Collider[] = Physics.OverlapSphere (grenadeOrigin, radius); //this is saying that if any collider within the radius of our object will feel the explosion
 
for(var hit : Collider in colliders){  //for loop that says if we hit any colliders, then do the following below
 
if (hit.GetComponent.<Rigidbody>()){
 
hit.GetComponent.<Rigidbody>().AddExplosionForce(power, grenadeOrigin, radius, explosiveLift); //if we hit any rigidbodies then add force based off our power, the position of the explosion object
 
AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1); //plays our explosion sound clip
 
Destroy(gameObject); //the radius and finally the explosive lift. Afterwards destroy the game object.
 
}
 
}
 
}