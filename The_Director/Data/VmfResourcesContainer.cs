public class VmfResourcesContainer
{
    public int Id { get; set; }
    public string Classname { get; set; }
    public string Targetname { get; set; }
    public string Origin { get; set; }
    public bool IsPointEntity { get; set; }

    public VmfResourcesContainer(int id, string classname, string targetname, string origin, bool isPointEntity)
    {
        Id = id;
        Classname = classname;
        Targetname = targetname;
        Origin = origin;
        IsPointEntity = isPointEntity;
    }
}