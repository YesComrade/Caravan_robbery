using UnityEngine;
using System.Collections;
using path_finder;

public class Map : MonoBehaviour {
	public static int[,] game_map = new int [52,26];
	private bool ready = false;
	public int num_of_city = 12;
	private int tree_num=5;
	private int count_helper =1;
	public Transform land;
	public Transform land_1;
	public Transform main_city;
	public Transform city;
	public Transform road;
	// Use this for initialization
	void Start () 
	{
		for (int i=0;i<52;i++)
			for (int j=0;j<26;j++)
		{
			//int rand = Random.Range(1,3);
			//if (rand == 1)
			game_map[i,j] = -1;
			//else game_map[i,j] = 1;
		}
		border_make();
		for (int i=0;i<tree_num;i++) 
//		{
//			int tree_x = Random.Range(1,50);
//			int tree_y = Random.Range(1,24);
//			for (int q=0;q<51;q++)
//			{
//				for (int c=0;c<25;c++)
//				{
//					if((q>=tree_x-20+count_helper&&q<=tree_x+20+count_helper)&&(c>=tree_y-10+count_helper
//					                                                                    &&c<=tree_y+10+count_helper))
//						game_map[q,c] = -1;
//					count_helper = Random.Range(-15,15);
//				}
//			}
//		}
		game_map[26,13] = -2;
		for (int i=0;i<num_of_city-1;i++)
		{
			int random1 = Random.Range(1,51);
			int random2 = Random.Range(1,24);
			//if (city_check(random1,random2))
			game_map[random1,random2] = -2;
			//else i--;
		}
		PathFinder pf = new PathFinder(game_map,52,26);
		pf.buildPaths();
		pf.printFinalField();
		pf.give_me_my_field(game_map);
		Vector3 p = new Vector3(0,0,-1);
		for (int i=0;i<52;i++)
		{
			if (i==0)
				p.x = -10;
			for (int j=0;j<26;j++)
		  {
				if (j==0)
					p.y = 0;
				switch (game_map[i,j])
				{
				case -1:Instantiate(land,p,Quaternion.identity);break;
				case 1:Instantiate(land_1,p,Quaternion.identity);break;
				case 3:Instantiate(main_city,p,Quaternion.identity);break;
				case -2:Instantiate(city,p,Quaternion.identity);break;
				case -7:Instantiate(road,p,Quaternion.identity);break;
				};
				p.y -= 1.26f;
		  }
			p.x += 1.26f;
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
	bool city_check(int a,int b)
	{
		for (int k=-1;k<=1;k++)
		{
			if (game_map[a+k,b]==4||game_map[a,b+k]==4)
				return false;
		}
		return true;
	}
	void border_make()
	{
		for (int i=0;i<52;i++)
			game_map[i,0]=-8;
		for(int i=0;i<26;i++)
			game_map[0,i]=-8;
		for (int i=0;i<52;i++)
			game_map[i,25]=-8;
		for (int i=0;i<26;i++)
			game_map[51,i]=-8;
	}
}
