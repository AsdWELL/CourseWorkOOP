namespace CourseWork
{
    public class ExhibitList : JsonSerializableList<Exhibit>
    {
        public ExhibitList() : base("./exhibits.json") { }
    }
}
