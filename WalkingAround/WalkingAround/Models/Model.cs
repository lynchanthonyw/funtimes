namespace WalkingAround.Models
{
    public class Model : VisualContainer
    {
        public Model(int key, VisualContainer parent = null)
            : this(key.ToString(), parent)
        {
        }

        public Model(string key, VisualContainer parent = null)
            : base("Model", key, parent)
        {
        }
    }
}