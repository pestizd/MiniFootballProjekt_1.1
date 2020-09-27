//Skripta za drzanje boundarya
struct Boundary
{
    //Skupiti up down left right u jedno
    public float Up, Down, Left, Right;

    public Boundary(float up, float down, float left, float right)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
    }
}