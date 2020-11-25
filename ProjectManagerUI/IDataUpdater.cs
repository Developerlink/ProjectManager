using ProjectManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerUI
{
    public interface IDataUpdater
    {
        void UpdateProjectList(Project project);
    }
}
