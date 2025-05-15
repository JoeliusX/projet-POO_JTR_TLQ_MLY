
using System.Collections.Generic;

namespace Memory_WPF_OOP.Controllers
{
    internal class Game
    {
        public List<int> Cards { get; private set; }
        public int? chosenCart1 { get; private set; }
        public int? chosenCart2 { get; private set; }

        public void Choose(int index)
        {
            if (chosenCart1 == index || chosenCart2 == index)
            {
                chosenCart2 = null;
            }

            if (chosenCart1 == null)
            {
                chosenCart1 = index;
                return;
            }
            if (chosenCart2 == null)
            {
                chosenCart2 = index;
                CheckStatus()
            }

        }
    }
}
