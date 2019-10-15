using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perzeptron
{
    public class PerNetWork
    {
        private List<List<float>> _weight;
        private const int GRID_SIZE = 34;

        public PerNetWork()
        {
            _weight = new List<List<float>>();

            for (int i = 0; i < GRID_SIZE; i++)
            {
                List<float> _row = new List<float>();
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    _row.Add(0.5f);
                }
                _weight.Add(_row);
            }
        }

        private int Sgnm(float num)
        {
            if (num >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int RunIteration(List<List<float>> grid)
        {
            float s1 = 0;
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    s1 += grid[i][j] * _weight[i][j];
                }
            }

            float t1 = 44;

            s1 -= t1;

            return Sgnm(s1);
        }

        public string PrintWeight()
        {
            string weightStr = "";

            for (int y = 0; y < _weight.Count; y++)
            {
                for (int x = 0; x < _weight[y].Count; x++)
                {
                    weightStr += _weight[y][x].ToString() + "  ";
                }

                weightStr += '\n';
            }

            return weightStr;
        }

        public void IncWeight(List<List<float>> grid)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        _weight[i][j] += 0.2f;
                    }
                }
            }
        }

        public void DecWeight(List<List<float>> grid)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        _weight[i][j] -= 0.2f;
                    }
                }
            }
        }
    }
}
