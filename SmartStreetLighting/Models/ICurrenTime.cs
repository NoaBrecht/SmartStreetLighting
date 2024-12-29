using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStreetLighting.Models
{
    public interface ICurrenTime
    {
        int GetCurrentHour();
        int getCurrentMonth();
        bool IsWinterSeason();
    }
}
