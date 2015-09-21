#pragma strict
var weapon01 : GameObject;
var weapon02 : GameObject;
function Update () {
	if (Input.GetKeyDown(KeyCode.Q))
	{
	SwapWeapons();
	}
}
function SwapWeapons() {
	if (weapon01.activeInHierarchy){
		weapon01.SetActive(false);
		weapon02.SetActive(true);
	}
	else{
		weapon01.SetActive(true);
		weapon02.SetActive(false);	
	}
}