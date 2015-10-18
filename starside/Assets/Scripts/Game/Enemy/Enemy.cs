using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private int x;
    private int y;
    private int[] pos; 

    public void setPos(int x, int y)
    {
        pos[0] = x;
        pos[1] = y; 
    } 

    public int getX() { return x; }
    public int getY() { return y; }
    public void setX(int x) { this.x = x; }
    public void setY(int y) { this.y = y; }
}
