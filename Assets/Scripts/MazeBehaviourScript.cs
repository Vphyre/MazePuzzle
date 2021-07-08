using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBehaviourScript : MonoBehaviour
{
    //Prefabs that will make up the maze


    [Header("Horizontal Prefab")]
    public GameObject horizontalPiece;
    //Horizontal pieces counter, when it reaches 14 it stops creating new pieces
    private int horizontalCount = 0;
    //When it reaches 8 it stops creating new lines
    private int horizontalLineCount = 0;
    
    private GameObject lastHorizontalInstance;

    private GameObject firstHorLineInstance;

    private bool firstHFlag = false;
    
    [Header("Vertical Prefab")]
    public GameObject verticalPiece;
    //Veritcal pieces counter, when it reaches 8 it stops creating new pieces
    private int verticalCount = 0;
    //When it reaches 14 it stops creating new lines
    private int verticalLineCount = 0;

    private GameObject lastVerticalInstance;

    private GameObject firstVertLineInstance;

    private bool firstVFlag = false;


    //Path's Variables
    public GameObject[] path;
    public int sortCount = 0;

    public bool pathFlag = false;

    void Start()
    {
        
    }
  
    void Update()
    {
        BuildNewMaze();
    }

    void BuildNewMaze()
    {
        BuildVerticalLine();
        BuildHorizontalLine();
        if(!pathFlag)
        {
            BuildPaths();
        }
    }

    void BuildVerticalLine()
    {
        //Line Counter
        if(verticalLineCount<12)
        {
            //Pieces in line Count
            if(verticalCount<8)
            {
                //Verify if is the first piece in line
                if(verticalCount==0)
                {
                    if(!firstVFlag)
                    {
                        //Ensures that the first piece starts in the correct place at the begin
                        firstVertLineInstance = Instantiate(verticalPiece);
                        firstVertLineInstance.transform.position = new Vector2(0.639f,-0.335f);
                        lastVerticalInstance = firstVertLineInstance;
                        verticalCount++;
                        firstVFlag = true;
                    }
                    else
                    {
                        //After, the others first pices are instantiated to make others lines 
                        lastVerticalInstance = Instantiate(verticalPiece);
                        lastVerticalInstance.transform.position = new Vector2(1f+firstVertLineInstance.transform.position.x,-0.335f);
                        firstVertLineInstance = lastVerticalInstance;
                        verticalCount++; 
                        
                    }
                    
                }
                //Else, instances in line continues
                else
                {
                    GameObject AuxObj = Instantiate(verticalPiece);
                    AuxObj.transform.position = new Vector2(lastVerticalInstance.transform.position.x, lastVerticalInstance.transform.position.y - 1f);
                    lastVerticalInstance = AuxObj;
                    verticalCount++;
                }
                
            }
            //Stop creating new pieces in line
            else
            {
                //Next line
                verticalLineCount++;
                verticalCount = 0;
            }
        }
        
    }
    
    void BuildHorizontalLine()
    {
       //Line Counter
        if(horizontalLineCount<9)
        {
            //Pieces in line Count
            if(horizontalCount<12)
            {
                //Verify if is the first piece in line
                if(horizontalCount==0)
                {
                    if(!firstHFlag)
                    {
                        //Ensures that the first piece starts in the correct place at the begin
                        firstHorLineInstance = Instantiate(horizontalPiece);
                        firstHorLineInstance.transform.position = new Vector2(0f,0f);
                        lastHorizontalInstance = firstHorLineInstance;
                        horizontalCount++;
                        firstHFlag = true;
                    }
                    else
                    {
                        //After, the others first pices are instantiated to make others lines 
                        lastHorizontalInstance = Instantiate(horizontalPiece);
                        lastHorizontalInstance.transform.position = new Vector2(0,firstHorLineInstance.transform.position.y-1f);
                        firstHorLineInstance = lastHorizontalInstance;
                        horizontalCount++; 
                    }
                    
                }
                //Else, instances in line continues
                else
                {
                    GameObject AuxObj = Instantiate(horizontalPiece);
                    AuxObj.transform.position = new Vector2(lastHorizontalInstance.transform.position.x+1f, lastHorizontalInstance.transform.position.y);
                    lastHorizontalInstance = AuxObj;
                    horizontalCount++;
                }
            }
            //Stop creating new pieces in line
            else
            {
                //Next line
                horizontalLineCount++;
                horizontalCount = 0;
            }
        }    
    }
    //To calculate GameObj's Weight
    float gameObjectWeight(GameObject obj)
    {
        var pointOne = obj.transform.TransformPoint(0,0,0);
        var pointTwo = obj.transform.TransformPoint(1,1,0);

        var weight = pointTwo.x - pointOne.x;

        return weight;
    }

    //To calculate GameObj's Height
    float gameObjectHeight(GameObject obj)
    {
        var pointOne = obj.transform.TransformPoint(0,0,0);
        var pointTwo = obj.transform.TransformPoint(1,1,0);

        var height = pointTwo.y - pointOne.y;

        return height;
    }
    void BuildPaths()
    {
        if(sortCount<30)
        {
            path =  GameObject.FindGameObjectsWithTag("Maze");
            int sort = Random.Range(0,108);

            if(path[sort]!=null)
            {
                path[sort].gameObject.SetActive(false);
            }
            
            sortCount++;
        }
        else
        {
            pathFlag = true;
        }

    }
}
