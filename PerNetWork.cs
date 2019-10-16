using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perzeptron
{
    public class PerNetWork
    {
        // Class Field
        private List<List<float>> _weight;

        // Class Constant
        private const int GRID_SIZE = 34;
        private const float EDUCATION_SPEED = 0.2f;

        // Constructor
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

        // Get Set
        public int GetGridSize()
        {
            return GRID_SIZE;
        }

        // Internal Function
        private int ThresholdFunc(float s1)
        {
            return MathFunc.Sgnm(s1);
        }

        private float ActiveFunc(float inputSignal, int i, int j)
        {
            return inputSignal * _weight[i][j]; ;
        }

        // Interface Function
        public int RunIteration(List<List<float>> grid)
        {
            float s1 = 0;
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    s1 += ActiveFunc(grid[i][j], i, j);
                }
            }

            float t1 = 44;

            s1 -= t1;

            return ThresholdFunc(s1);
        }

        public void IncWeight(List<List<float>> grid)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        _weight[i][j] += EDUCATION_SPEED;
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
                        _weight[i][j] -= EDUCATION_SPEED;
                    }
                }
            }
        }

        public override string ToString()
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
    }
}
