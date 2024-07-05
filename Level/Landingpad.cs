using Godot;
using System;

public partial class Landingpad : CsgBox3D
{
	// File path variable with file export hint
	[Export(PropertyHint.File, "*.tscn")] 
	public new string FilePath { get; set; }
}
