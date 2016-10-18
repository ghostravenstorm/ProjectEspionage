using System;

public class BaseController {
	public float speed{ private set; get; }
	public float modifier{ private set; get; }

	public BaseController(float speed, float modifier){
		this.speed = speed;
		this.modifier = modifier;
	}
}