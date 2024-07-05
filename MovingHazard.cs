using Godot;
using System;

public partial class MovingHazard : AnimatableBody3D
{
	[Export] private Vector3 _destination;
	[Export] private float _duration = 1f;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Current GlobalPosition: ", GlobalPosition);
		GD.Print("Destination: ", _destination);
		GD.Print("Duration: ", _duration);
		
		Tween tween = CreateTween();
		tween.SetLoops();
		tween.SetTrans(Tween.TransitionType.Sine);
		tween.TweenProperty(this, "global_position", GlobalPosition + _destination, _duration);
		tween.TweenProperty(this, "global_position", GlobalPosition, _duration);
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
