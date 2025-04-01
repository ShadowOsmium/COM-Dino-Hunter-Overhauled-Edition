using BehaviorTree;
using UnityEngine;

public class MainCpp : MonoBehaviour
{
	public Geometry g1;

	public Geometry g2;

	private void Start()
	{
		CompositeNode compositeNode = new CompositeNode_Sequence();
		compositeNode.AddChild(new lgTypeCheckNode(GeometryType.Cube));
		compositeNode.AddChild(new doWaitNode(5f));
		compositeNode.AddChild(new doRandomPointNode());
		compositeNode.AddChild(new doMoveNode());
		CompositeNode compositeNode2 = new CompositeNode_Sequence();
		compositeNode2.AddChild(new lgTypeCheckNode(GeometryType.Sphere));
		compositeNode2.AddChild(new doWaitNode(0.5f));
		compositeNode2.AddChild(new doRandomPointNode());
		compositeNode2.AddChild(new doMoveNode());
		CompositeNode compositeNode3 = new CompositeNode_Selector();
		compositeNode3.AddChild(compositeNode);
		compositeNode3.AddChild(compositeNode2);
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		gameObject.GetComponent<Renderer>().material.color = Color.green;
		g1 = gameObject.AddComponent<Geometry>();
		g1.Type = GeometryType.Cube;
		g1.SetAI(compositeNode3);
		gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		g2 = gameObject.AddComponent<Geometry>();
		g2.Type = GeometryType.Sphere;
		g2.SetAI(compositeNode3);
	}

	private void Update()
	{
	}
}
