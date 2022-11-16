namespace rabota
{
    public class ipdz<T> where T : class
    {
        private List<T> nuget = new List<T>();

        public ipdz() { }
        public void Add(T argument)
        {
            nuget.Add(argument);
        }
        public void Remove(T argument)
        {
            nuget.Remove(argument);
        }
        public void Change(T id, T arg)
        {
            var index = nuget.IndexOf(id);
            nuget[index] = arg;
        }
        public List<T> Get()
        {
            return nuget;
        }
    }
}