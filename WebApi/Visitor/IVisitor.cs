namespace WebApi.Visitor
{
    public interface IVisitor
    {
        void Visit(IVisitor visitor);
    }
}
