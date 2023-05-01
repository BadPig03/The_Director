using System.Collections.Generic;

public class VmfResourcesContainer
{
    public int Id { get; set; }
    public string Classname { get; set; }
    public string Targetname { get; set; }
    public string Origin { get; set; }
    public string Model { get; set; }
    public List<string> Materials { get; set; }

    public VmfResourcesContainer(int id, string classname = "", string targetname = "", string origin = "", string model = "", List<string> materials = null)
    {
        Id = id;
        Classname = classname;
        Targetname = targetname;
        Origin = origin;
        Model = model;
        Materials = materials ?? new List<string>();
    }
}