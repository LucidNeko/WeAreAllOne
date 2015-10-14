using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;

public class menuScript : MonoBehaviour 
{
	public Canvas controlMenu;
	public Canvas quitMenu;
	public Button startText;
	public Button controls;
	public Button exitText;

	public Image playButton;
	public Image controlButton;
	public Image exitButton;
	public Image logo;
	
	void Start ()
		
	{
		quitMenu = quitMenu.GetComponent<Canvas>();
		startText = startText.GetComponent<Button> ();
		controls = controls.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		controlMenu.enabled = false;
		quitMenu.enabled = false;


		
	}
	
	public void ExitPress() //this function will be used on our Exit button
		
	{
		quitMenu.enabled = true; //enable the Quit menu when we click the Exit button
		startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
		controls.enabled = false;
		exitText.enabled = false;
		
	}
	
	public void ControlsPress()
	
	{
		controlMenu.enabled = true;
		startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
		controls.enabled = false;
		exitText.enabled = false;

		Color color = playButton.color;
		color.a = 0;
		playButton.color = color;
		Color colorA = controlButton.color;
		colorA.a = 0;
		controlButton.color = colorA;
		Color colorB = exitButton.color;
		colorB.a = 0;
		exitButton.color = colorB;
		Color colorC = logo.color;
		colorC.a = 0;
		logo.color = colorC;
	}

	public void NoPress() //this function will be used for our "NO" button in our Quit Menu
		
	{
		controlMenu.enabled = false;
		quitMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
		startText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
		controls.enabled = true;
		exitText.enabled = true;

		Color color = playButton.color;
		color.a = 1;
		playButton.color = color;
		Color colorA = controlButton.color;
		colorA.a = 1;
		controlButton.color = colorA;
		Color colorB = exitButton.color;
		colorB.a = 1;
		exitButton.color = colorB;
		Color colorC = logo.color;
		colorC.a = 1;
		logo.color = colorC;
	}
	
	public void StartLevel () //this function will be used on our Play button
		
	{
		Application.LoadLevel(1); //this will load our first level from our build settings. "1" is the second scene in our game
		
	}
	
	public void ExitGame () //This function will be used on our "Yes" button in our Quit menu
		
	{
		Application.Quit(); //this will quit our game. Note this will only work after building the game
		
	}
	
}