#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

using System.Collections;

[ExecuteInEditMode]
public class Snap : MonoBehaviour {
	
	public float cell_size = 1.0f; // For equal widht, height, and depth
	private float x, y, z;
	
	
	void Start() 
	{
		x = 0.0f;
		y = 0.0f;
		z = 0.0f;		
		
	}
	
	void Update() 
	{
		// Don't want this in play mode.
		if (EditorApplication.isPlaying ) 
			return;
		
		// Snap the object to grid
		x = Mathf.Round(transform.position.x / cell_size) * cell_size;
		y = Mathf.Round(transform.position.y / cell_size) * cell_size;
		z = Mathf.Round(transform.position.z / cell_size) * cell_size;
		transform.position = new Vector3(x, y, z);
		
	}
}
# endif
