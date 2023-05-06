using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using The_Director.Utils;

public class VmfExtractor
{
    public virtual List<VmfResourcesContainer> VmfResourcesContainer { get; set; }
    public virtual string SavePath { get; set; }
    public virtual MdlExtractor MdlExtractor { get; set; }
    public virtual VmtReader VmtReader { get; set; }
    public virtual PcfReader PcfReader { get; set; }
    public virtual BackgroundWorker Worker { get; set; }

    protected int containerCount = 0;
    public void StartProcess()
    {
        Worker.ReportProgress(0);

        foreach (VmfResourcesContainer vmfResource in VmfResourcesContainer)
        {
            if (vmfResource.Classname == "worldspawn")
            {
                foreach (string extension in Globals.SkyboxExtensionList)
                {
                    Functions.CopyVtfMaterialFiles(VmtReader.HandleAVmt("materials\\skybox\\" + vmfResource.Model + extension + ".vmt"), SavePath);
                }
                Functions.CopyVtfMaterialFiles(vmfResource.Materials, SavePath);
                continue;
            }

            if (vmfResource.Classname != "info_particle_system" && vmfResource.Model != string.Empty && !Globals.OfficialModelPaths.Contains(vmfResource.Model.Replace("/", "\\").ToLowerInvariant()))
            {
                foreach (string path in MdlExtractor.HandleAMdl(vmfResource.Model))
                {
                    Functions.CopyVtfMaterialFiles(VmtReader.HandleAVmt(path), SavePath);
                }

                if (vmfResource.Model.EndsWith(".mdl"))
                {
                    Functions.CopyMdlModelFiles(vmfResource, SavePath);
                }
                else if (vmfResource.Model.EndsWith(".vmt"))
                {
                    Functions.CopyVtfMaterialFiles(VmtReader.HandleAVmt("materials\\" + vmfResource.Model), SavePath);
                }
            }

            if (vmfResource.Classname == "info_particle_system")
            {
                foreach (string file in Globals.CustomParticlePaths)
                {
                    string result = PcfReader.ContainsEffectInFile(vmfResource.Model, file);
                    if (result != string.Empty)
                    {
                        string pcfName = "particles\\" + result + ".pcf";
                        foreach (string path in PcfReader.HandleAPcf(pcfName))
                        {
                            Functions.CopyVtfMaterialFiles(VmtReader.HandleAVmt("materials\\" + path), SavePath);
                        }
                        break;
                    }
                }
            }

            if (vmfResource.Materials.Count > 0)
            {
                foreach (string material in vmfResource.Materials)
                {
                    if (!Globals.OfficialMaterialsPaths.Contains(material.Replace("/", "\\").ToLowerInvariant()))
                    {
                        Functions.CopyVtfMaterialFiles(VmtReader.HandleAVmt(material), SavePath);
                    }
                }
            }
            containerCount++;
            Worker.ReportProgress(100 * containerCount / VmfResourcesContainer.Count);
        }
        Worker.ReportProgress(100);
    }
}