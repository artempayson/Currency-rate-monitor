using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
namespace GUI
{
    public class GraphViewModel
    {
        public string Title { get; set; }
        public IEnumerable<DataPoint> Points { get; set; }
        public GraphViewModel()
        {
            {
                Title = "First prototype";
                Points = new List<DataPoint>
                              {
                                  new DataPoint(0, 4),
                                  new DataPoint(10, 13),
                                  new DataPoint(20, 15),
                                  new DataPoint(30, 16),
                                  new DataPoint(40, 12),
                                  new DataPoint(50, -12)
                              };
            }
        }


    }
}


