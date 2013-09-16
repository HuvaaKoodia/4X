using UnityEngine;
using System.Collections;

public class FactionAI {
	
	FactionData faction;
	AIMemoryData mem;
	WorldData world;
	
	public FactionAI(FactionData fac, WorldData world){
		faction=fac;
		mem=new AIMemoryData(fac);
		this.world=world;
		
		//known nodes DEV.IMP use scanners
		foreach(var n in world.Nodes){
			var mn=new NodeMemory(n);
			mem.KnownNodes.Add(n,mn);
			if (n.HasColony()&&n.Colony==fac.CapitalColony)
				mn.Known=true;
		}
	}
	
	//DEV:TEMP
	public void Update ()
	{
		//buying ships
		foreach(var c in faction.Colonies){
			//while(c.Energy>faction.ShipCost){
			//	c.BuildShip();
			//}
			if (c.BuildItems.Count==0)
				c.BuildShip();
		}
		//order ships around
		foreach(var s in faction.Ships){
			if (!s.Moving){
				//colonize
				if (s.Orbit!=null&&!s.Orbit.HasColony()){
					s.ColonizePlanet(true);
				}
				
				//move to colonize
				if (!s.ColonizingPlanet){
					foreach (var n in mem.KnownNodes){
						if (!n.Value.Known&&!n.Value.colonization_target){
							s.setMovement(n.Value.Node);
							n.Value.colonization_target=true;
							break;
						}
					}
				}
			}
		}
	}
}
