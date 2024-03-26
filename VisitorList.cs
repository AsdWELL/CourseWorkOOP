namespace CourseWork
{  
    public class VisitorList : JsonSerializableList<Visitor>
    {      
        public VisitorList() : base("./visitors.json") { }
    }
}
