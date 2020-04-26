namespace BusinessLogic.src.models
{
    public class Dog : Pet
    {
        public string name { get; set; }
        public string address { get; set; }
        public string owner { get; set; }
        public string breed { get; set; }
        public string breeder { get; set; }
        public bool outdoor { get; set; }
        public bool attack { get; set; }
        public bool service { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
    }
}
