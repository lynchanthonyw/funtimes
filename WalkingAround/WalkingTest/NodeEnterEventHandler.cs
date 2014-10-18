namespace WalkingTest
{
    public delegate void NodeEventHandler(Node sender, NodeEventType e);

    public enum NodeEventType
    {
        Enter,
        Exit,
        Click
    }
}