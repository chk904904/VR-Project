using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Text;
using Random=UnityEngine.Random;

public class OverallController : MonoBehaviour
{
	public bool gameStart; 
	public int currentScore; 
	public GameObject scoreBoard; 
	public GameObject hintSprite;

	private Dictionary<int, string> faceSignal = new Dictionary<int, string>();
	private Dictionary<int, string> leftNoodleSignal = new Dictionary<int, string>();
	private Dictionary<int, string> rightNoodleSignal = new Dictionary<int, string>();
	private Dictionary<int, string> leftBlocks = new Dictionary<int, string>(); 
	private Dictionary<int, string> middleBlocks = new Dictionary<int, string>(); 
	private Dictionary<int, string> rightBlocks = new Dictionary<int, string>(); 
	private List<int> keyFrames; 
	private int curFrame; 
	private int frameCtr;  
	private int curFrameIdx;
	private string curDir;


	private bool is_init = true;

    // Start is called before the first frame update
    void Start()
    {
        frameCtr = 0;  
        curFrameIdx = 0;
        //initialize left blocks
        leftBlocks[210] = "M";
        leftBlocks[330] = "M";
        leftBlocks[330] = "M";
        leftBlocks[330] = "M";
        leftBlocks[330] = "M";
        leftBlocks[330] = "M";
        leftBlocks[330] = "M";
        leftBlocks[330] = "M";
        leftBlocks[450] = "M";
        leftBlocks[380] = "M";
        leftBlocks[585] = "M";
        leftBlocks[1650] = "M";
        leftBlocks[1890] = "M";
        leftBlocks[2130] = "M";
        leftBlocks[2205] = "L";
        leftBlocks[2250] = "M";
        leftBlocks[2370] = "M";
        leftBlocks[2490] = "M";
        leftBlocks[2565] = "L";
        //initialize middle blocks
        middleBlocks[240] = "M";
        middleBlocks[360] = "M";
        middleBlocks[465] = "M";
        middleBlocks[570] = "M";
        middleBlocks[600] = "M";
        middleBlocks[1260] = "L";
        middleBlocks[1350] = "M";
        middleBlocks[1680] = "M";
        middleBlocks[1740] = "R";
        middleBlocks[1800] = "R";
        middleBlocks[1920] = "M";
        middleBlocks[2040] = "L";
        middleBlocks[1890] = "M";
        middleBlocks[2130] = "M";
        middleBlocks[2100] = "L";
        middleBlocks[2145] = "M";
        middleBlocks[2190] = "L";
        middleBlocks[2235] = "M";
        middleBlocks[2265] = "L";
        middleBlocks[2295] = "R";
        middleBlocks[2325] = "R";
        middleBlocks[2385] = "M";
        middleBlocks[2415] = "R";
        middleBlocks[2445] = "R";
        middleBlocks[2505] = "M";
        middleBlocks[2550] = "L";
        middleBlocks[2595] = "M";
        
        //initialize right blocks
        rightBlocks[1230] = "L";
        rightBlocks[1380] = "M";
        rightBlocks[1770] = "R";
        rightBlocks[1860] = "M";
        rightBlocks[1980] = "L";
        rightBlocks[2010] = "L";
        rightBlocks[2175] = "L";
        rightBlocks[2310] = "R";
        rightBlocks[2355] = "M";
        rightBlocks[2430] = "R";
        rightBlocks[2475] = "M";
        rightBlocks[2535] = "L";


        //initialize face signal
        faceSignal[0] = "M"; 
        faceSignal[120] = "M";
        faceSignal[240] = "M";
        faceSignal[360] = "M";
        faceSignal[480] = "M";
        faceSignal[600] = "M";
        faceSignal[720] = "L";
        faceSignal[840] = "M";
        faceSignal[960] = "L";
        faceSignal[1020] = "M";
        faceSignal[1080] = "R";
        faceSignal[1140] = "M";
        faceSignal[1200] = "L";
        faceSignal[1320] = "M";
        faceSignal[1440] = "L";
        faceSignal[1500] = "M";
        faceSignal[1560] = "R";
        faceSignal[1620] = "M";
        faceSignal[1680] = "M";
        faceSignal[1740] = "R";
        faceSignal[1800] = "R";
        faceSignal[1860] = "M";
        faceSignal[1920] = "M";
        faceSignal[1980] = "L";
        faceSignal[2040] = "L";
        faceSignal[2100] = "M";
        faceSignal[2160] = "M";
        faceSignal[2190] = "L";
        faceSignal[2250] = "M";
        faceSignal[2280] = "M";
        faceSignal[2310] = "R";
        faceSignal[2370] = "M";
        faceSignal[2400] = "M";
        faceSignal[2430] = "R";
        faceSignal[2490] = "M";
        faceSignal[2520] = "M";
        faceSignal[2550] = "L";
        faceSignal[2610] = "M";
        //initialize left noodle signal
        leftNoodleSignal[0] = "X";
        leftNoodleSignal[60] = "WS";
        leftNoodleSignal[90] = "W";
        leftNoodleSignal[120] = "WE";
        leftNoodleSignal[150] = "WS";
        //leftNoodleSignal[165] = "W";
        leftNoodleSignal[180] = "WE";
        leftNoodleSignal[210] = "X";
        leftNoodleSignal[240] = "X";
        leftNoodleSignal[300] = "WS";
        leftNoodleSignal[330] = "W";
        leftNoodleSignal[360] = "WE";
        leftNoodleSignal[480] = "X";
        leftNoodleSignal[540] = "WS";
        leftNoodleSignal[570] = "W";
        leftNoodleSignal[600] = "WE";
        leftNoodleSignal[720] = "X";
        leftNoodleSignal[840] = "X";
        leftNoodleSignal[960] = "X";
        leftNoodleSignal[1020] = "X";
        leftNoodleSignal[1080] = "X";
        leftNoodleSignal[1140] = "X";
        leftNoodleSignal[1200] = "x";
        leftNoodleSignal[1320] = "X";
        leftNoodleSignal[1440] = "U";
        leftNoodleSignal[1470] = "U";
        leftNoodleSignal[1500] = "X";
        leftNoodleSignal[1560] = "D";
        leftNoodleSignal[1590] = "D";
        leftNoodleSignal[1620] = "X";
        leftNoodleSignal[1680] = "X";
        leftNoodleSignal[1740] = "X";
        leftNoodleSignal[1800] = "X";
        leftNoodleSignal[1860] = "X";
        leftNoodleSignal[1920] = "X";
        leftNoodleSignal[1980] = "U";
        leftNoodleSignal[2040] = "X";
        leftNoodleSignal[2100] = "D";
        leftNoodleSignal[2160] = "U";
        leftNoodleSignal[2220] = "D";
        leftNoodleSignal[2280] = "X";
        leftNoodleSignal[2340] = "X";
        leftNoodleSignal[2400] = "X";
        leftNoodleSignal[2460] = "X";
        leftNoodleSignal[2520] = "U";
        leftNoodleSignal[2580] = "D";
        //initialize right noodle signal
        rightNoodleSignal[0] = "X";
        rightNoodleSignal[60] = "X";
        rightNoodleSignal[90] = "X";
        rightNoodleSignal[120] = "X";
        rightNoodleSignal[150] = "X";
        rightNoodleSignal[165] = "WS";
        rightNoodleSignal[180] = "W";
        rightNoodleSignal[210] = "WE";
        rightNoodleSignal[240] = "X";
        rightNoodleSignal[300] = "X";
        rightNoodleSignal[330] = "X";
        //????
        rightNoodleSignal[360] = "X";
        rightNoodleSignal[480] = "WS";
        rightNoodleSignal[540] = "W";
        rightNoodleSignal[570] = "WE";
        rightNoodleSignal[600] = "WE";
        rightNoodleSignal[720] = "X";
        //???
        rightNoodleSignal[840] = "X";
        rightNoodleSignal[960] = "X";
        rightNoodleSignal[1020] = "X";
        rightNoodleSignal[1080] = "X";
        rightNoodleSignal[1140] = "X";
        rightNoodleSignal[1200] = "x";
        rightNoodleSignal[1320] = "X";
        rightNoodleSignal[1440] = "X";
        rightNoodleSignal[1470] = "X";
        rightNoodleSignal[1500] = "X";
        rightNoodleSignal[1560] = "U";
        rightNoodleSignal[1590] = "U";
        rightNoodleSignal[1620] = "X";
        rightNoodleSignal[1680] = "D";
        rightNoodleSignal[1740] = "U";
        rightNoodleSignal[1800] = "X";
        rightNoodleSignal[1860] = "X";
        rightNoodleSignal[1920] = "D";
        rightNoodleSignal[1980] = "X";
        rightNoodleSignal[2040] = "X";
        rightNoodleSignal[2100] = "X";
        rightNoodleSignal[2160] = "X";
        rightNoodleSignal[2220] = "X";
        rightNoodleSignal[2280] = "U";
        rightNoodleSignal[2340] = "D";
        rightNoodleSignal[2400] = "U";
        rightNoodleSignal[2460] = "D";
        rightNoodleSignal[2520] = "X";
        rightNoodleSignal[2580] = "X";
        //initialize frame list
        keyFrames = new List<int>(){0,60,90,120,150,165,180,210,240,270,300,330,360,450,465,480,540,570,585,600,720,840,960,
        1020,1080,1140,1200,1230,1260,1320,1350,1380,1440,1470,1500,1560,1590,1620,1650,1680,1740,1770,1800,1860,1890,1920,1980,2010,2040,2100,2130,2145,2160,
        2175,2190,2205,2220,2235,2250,2265,2280,2295,2310,2325,2340,2355,2370,2385,2400,2415,2430,2445,2460,2475,2490,2505,2520,2535,2550,2580,2595,2610
        };
        curFrame = keyFrames[0];
        curDir = "M";

    }

    // Update is called once per frame
    void Update()
    {
        	// if(faceSignal.ContainsKey(frameCtr)){
        	// 	//if is face
        	// 	moveFace(faceSignal[frameCtr]);
        	// 	curDir = faceSignal[frameCtr];
        		
        	// }
        	// if(leftNoodleSignal.ContainsKey(frameCtr)){
        	// 	//if is left noodle
        	// 	moveLeftNoodle(leftNoodleSignal[frameCtr]);
        	
        	// }
        	// if(rightNoodleSignal.ContainsKey(frameCtr)){
        	// 	//if is right noodle
        	// 	moveRightNoodle(rightNoodleSignal[frameCtr]);
        	
        	// }

        	if(leftBlocks.ContainsKey(frameCtr)){
        		spawnLeftBlock(leftBlocks[frameCtr]);
        	}
        	if(middleBlocks.ContainsKey(frameCtr)){
        		spawnMiddleBlock(middleBlocks[frameCtr]);
        	}
        	if(rightBlocks.ContainsKey(frameCtr)){
        		spawnRightBlock(rightBlocks[frameCtr]);
        	}
        	frameCtr++;
    }

    public void updateScore(int deltaValue){
    	currentScore += deltaValue; 
    	//TODO: change the score board
    }

    void moveLeftNoodle(string signal){
    	RodController r_c = GameObject.Find("Left_pivot").GetComponent<RodController>();
    	if(signal == "X"){
    		r_c.l_start_over = true;
    		r_c.l_hold = true;
    		r_c.l_horizontal_rot = false; 
    		r_c.l_horizontal_rot_back = false; 
    		r_c.l_is_horizontal = false;
    		r_c.l_up = false;
    		r_c.l_down = false;
    	}
    	else if(signal == "WS"){
    		r_c.l_start_over = true;
    		r_c.l_hold = false;
    		r_c.l_horizontal_rot = true; 
    		r_c.l_horizontal_rot_back = false; 
    		r_c.l_is_horizontal = false;
    		r_c.l_up = false;
    		r_c.l_down = false;
    	}
    	else if(signal == "WE"){
    		r_c.l_start_over = true;
    		r_c.l_hold = false;
    		r_c.l_horizontal_rot = false; 
    		r_c.l_horizontal_rot_back = true; 
    		r_c.l_is_horizontal = false;
    		r_c.l_up = false;
    		r_c.l_down = false;
    	}
    	else if(signal == "W"){
    		r_c.l_start_over = true;
    		r_c.l_hold = false;
    		r_c.l_horizontal_rot = false; 
    		r_c.l_horizontal_rot_back = false;
    		r_c.l_is_horizontal = true;
    		r_c.l_up = false;
    		r_c.l_down = false;
    	}
    	else if(signal == "U"){
    		r_c.l_start_over = true;
    		r_c.l_hold = false;
    		r_c.l_horizontal_rot = false; 
    		r_c.l_horizontal_rot_back = false;
    		r_c.l_is_horizontal = false;
    		r_c.l_up = true;
    		r_c.l_down = false;

    	}
    	else if(signal == "D"){
    		r_c.l_start_over = true;
    		r_c.l_hold = false;
    		r_c.l_horizontal_rot = false; 
    		r_c.l_horizontal_rot_back = false;
    		r_c.l_is_horizontal = false;
    		r_c.l_up = false;
    		r_c.l_down = true;
    	}
    }

    void moveRightNoodle(string signal){
    	RodController r_c = GameObject.Find("Left_pivot").GetComponent<RodController>();
    	if(signal == "X"){
    		r_c.r_start_over = true;
    		r_c.r_hold = true;
    		r_c.r_horizontal_rot = false; 
    		r_c.r_horizontal_rot_back = false; 
    		r_c.r_is_horizontal = false;
    		r_c.r_up = false;
    		r_c.r_down = false;
    	}
    	else if(signal == "WS"){
    		r_c.r_start_over = true;
    		r_c.r_hold = false;
    		r_c.r_horizontal_rot = true; 
    		r_c.r_horizontal_rot_back = false; 
    		r_c.r_is_horizontal = false;
    		r_c.r_up = false;
    		r_c.r_down = false;
    	}
    	else if(signal == "WE"){
    		r_c.r_start_over = true;
    		r_c.r_hold = false;
    		r_c.r_horizontal_rot = false; 
    		r_c.r_horizontal_rot_back = true; 
    		r_c.r_is_horizontal = false;
    		r_c.r_up = false;
    		r_c.r_down = false;
    	}
    	else if(signal == "W"){
    		r_c.r_start_over = true;
    		r_c.r_hold = false;
    		r_c.r_horizontal_rot = false; 
    		r_c.r_horizontal_rot_back = false;
    		r_c.r_is_horizontal = true;
    		r_c.r_up = false;
    		r_c.r_down = false;
    	}
    	else if(signal == "U"){
    		r_c.r_start_over = true;
    		r_c.r_hold = false;
    		r_c.r_horizontal_rot = false; 
    		r_c.r_horizontal_rot_back = false;
    		r_c.r_is_horizontal = false;
    		r_c.r_up = true;
    		r_c.r_down = false;

    	}
    	else if(signal == "D"){
    		r_c.r_start_over = true;
    		r_c.r_hold = false;
    		r_c.r_horizontal_rot = false; 
    		r_c.r_horizontal_rot_back = false;
    		r_c.r_is_horizontal = false;
    		r_c.r_up = false;
    		r_c.r_down = true;
    	}
    }

    void moveFace(string signal){
    	FaceController f_c = GameObject.Find("Face").GetComponent<FaceController>();
    	if(signal == "L"){
    		f_c.face_right = false; 
    		f_c.face_middle = false; 
    		f_c.face_left = true; 
    	}
    	else if(signal == "M"){
    		f_c.face_right = false; 
    		f_c.face_middle = true; 
    		f_c.face_left = false; 
    	}
    	else if(signal == "R"){
    		f_c.face_right = true; 
    		f_c.face_middle = false; 
    		f_c.face_left = false; 
    	}
    }

    void spawnLeftBlock(string typeIndex){
    	int index = 0;
    	if(typeIndex == "L"){
    		index = 1;
    	}
    	else if(typeIndex == "M"){
    		index = 0;
    	}
    	else if(typeIndex == "R"){
    		index = 2;
    	}
    	BlockSpawner b_s = GameObject.Find("Spawner").GetComponent<BlockSpawner>();
    	b_s.spawn_left(index);
    }

    void spawnRightBlock(string typeIndex){
    	int index = 0;
    	if(typeIndex == "L"){
    		index = 1;
    	}
    	else if(typeIndex == "M"){
    		index = 0;
    	}
    	else if(typeIndex == "R"){
    		index = 2;
    	}
    	BlockSpawner b_s = GameObject.Find("Spawner").GetComponent<BlockSpawner>();
    	b_s.spawn_right(index);

    }

    void spawnMiddleBlock(string typeIndex){
    	int index = 0;
    	if(typeIndex == "L"){
    		index = 1;
    	}
    	else if(typeIndex == "M"){
    		index = 0;
    	}
    	else if(typeIndex == "R"){
    		index = 2;
    	}
    	BlockSpawner b_s = GameObject.Find("Spawner").GetComponent<BlockSpawner>();
    	b_s.spawn_middle(index);
    }


}
