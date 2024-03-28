using static CourseWork.Visitor;

namespace CourseWork
{      
    public class VisitorList : JsonSerializableList<Visitor>
    {      
        public VisitorList() : base("./visitors.json") { }

        public VisitorList FindVisitorsByFieldValue(VisitorFields field, string value)
        {
            return [.. FindAll(v => v.IsFieldEqulsValue(field, value))];
        }
    }
}
